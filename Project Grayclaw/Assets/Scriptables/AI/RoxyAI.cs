using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// Controls Roxanne Wolf's behavior in the scene
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class RoxyAI : MonoBehaviour
{
    //scriptable object that contains all the information about a lavel
    private MapData levelData;
    public State currentState;
    [SerializeField]
    //Has one value, "Velocity" that corresponds to a blend tree. At Velocity = 0 is a full stand still animation. At Velocity = 1 is a full walk animation.
    private Animator animator;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    // Assuming the following additional states are defined:
    public enum State
    {
        idling,
        chasing,
        disrupting
    }

    [SerializeField]
    private float roamDelay = 5f; // Time Roxy spends in a location before moving again
    [SerializeField]
    private float disruptDelay = 5f; // Time Roxy spends at an endpoint before breaking it
    [Tooltip("Value between 0 and 1 signifying likleyness to break an endpoint.")]
    [SerializeField]
    [Range(0f, 1f)]
    private float destructiveness = 0.1f;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private float chaseDistance = 20f; // Distance from which Roxy starts chasing the player
    [SerializeField]
    private float viewAngle;
    [SerializeField]
    private string systemToTarget = "Untagged"; // Tag of the system endpoints to disrupt
    private float nextActionTime = 0f;
    private Vector3 lastKnownPlayerPosition;
    private physicalEndpoint targetedEndpoint;

    private Vector2 velocity;
    private Vector2 smoothDeltaPostion;

    private void Awake()
    {
        levelData = FindAnyObjectByType<map>().data;
        if (levelData == null)
        {
            Debug.LogError("No map singleton in level.");
        }
        currentState = RoxyAI.State.idling;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("No defined animator for Roxanne Wolf");
            }
        }
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent == null)
            {
                Debug.LogError("No defined NavMeshAgent for Roxanne Wolf");
            }
        }
        animator.applyRootMotion = true;
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = true;
        //Here so idling script will initially work
        navMeshAgent.destination = transform.position;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.idling:
                Idle();
                break;
            case State.chasing:
                Chase();
                break;
            case State.disrupting:
                Disrupt();
                break;
        }
        SyncronizeAnimatorAndAgent();
    }
    private void OnAnimatorMove()
    {
        Vector3 rootPosition = animator.rootPosition;
        rootPosition.y = navMeshAgent.nextPosition.y;//keep height syncronized
        transform.position = rootPosition;//could do rotation here to
        navMeshAgent.nextPosition = rootPosition;
    }
    private void SyncronizeAnimatorAndAgent()
    {
        Vector3 worldDeltaPostiton = navMeshAgent.nextPosition - transform.position;
        worldDeltaPostiton.y = 0; // modify if jumping

        float dx = Vector3.Dot(transform.right, worldDeltaPostiton);
        float dy = Vector3.Dot(transform.forward, worldDeltaPostiton);
        Vector2 deltaPostion = new Vector2(dx, dy);
        //For varible framerate
        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        smoothDeltaPostion = Vector2.Lerp(smoothDeltaPostion, deltaPostion, smooth);

        velocity = smoothDeltaPostion / Time.deltaTime;
        //slow  down
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            velocity = Vector2.Lerp(Vector2.zero, velocity, navMeshAgent.remainingDistance/navMeshAgent.stoppingDistance);
        }
        //from idle to moving
        bool shouldMove = velocity.magnitude > 0.5f && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;


        animator.SetBool("Move", shouldMove);
        animator.SetFloat("Velocity", velocity.magnitude); // for one dimensional

        float deltaMagnitude = worldDeltaPostiton.magnitude;
        if(deltaMagnitude > navMeshAgent.radius / 2f)
        {
            transform.position = Vector3.Lerp(animator.rootPosition, navMeshAgent.nextPosition, smooth);
        }
    }
    private void Idle()
    {
        // Check if Roxy has reached her destination or if it's time to choose a new destination
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending && nextActionTime == -1)
        {
            nextActionTime = Time.time + roamDelay + Random.Range(-2f, 2f);
        }
        if(nextActionTime != -1 && Time.time >= nextActionTime)
        {
            // Pick a random room and set the destination OR decide to break an endpoint
            float random = Random.Range(0f,1f);
            if(random <= destructiveness)
            {
                currentState = State.disrupting;
            }
            else
            {
                if (levelData.rooms.Count > 0)
                {
                    Room targetRoom = levelData.rooms[Random.Range(0, levelData.rooms.Count)];
                    navMeshAgent.SetDestination(targetRoom.transform.position);
                }
                nextActionTime = -1;
            }
        }

        // Check for player visibility to potentially switch to chasing
        if (CheckForPlayer() != null)
        {
            currentState = State.chasing;
        }
    }

    private void Chase()
    {
        Collider playerCollider = CheckForPlayer();
        //If player detected
        if (playerCollider != null)
        {
            lastKnownPlayerPosition = playerCollider.transform.position; //update player position
            navMeshAgent.SetDestination(lastKnownPlayerPosition);
        }
        //if has arrived at last know location, but isn't there
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Recheck for player visibility here could potentially be optimized away or integrated differently.
            Debug.Log(playerCollider != null ? "found you" : "I'll find you eventually");
            if (playerCollider == null)
            {
                currentState = State.idling;
            }
        }
    }

    private Collider CheckForPlayer()
    {
        // Check if player in range
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, chaseDistance, playerLayer);
        Collider player = null;
        foreach (var playerCollider in playersInRange)
        {
            // Find the exact player controller object in the player layer
            if (playerCollider.GetComponent<CharacterController>() != null)
            {
                player = playerCollider;
            }
        }

        if (player != null)
        {
            // Check line of sight
            RaycastHit hit;
            // So ray hits center of collider
            //Vector3 playerHeightOffset = new Vector3(0, player.GetComponent<CharacterController>().height / 2, 0);
            Vector3 playerCenter = player.GetComponent<CharacterController>().center + player.transform.position;
            Vector3 directionToPlayer = (playerCenter - transform.position).normalized;
            Debug.DrawLine(transform.position, transform.position + directionToPlayer * chaseDistance, Color.red);

            // Use LayerMask to combine player and obstacle layers
            int combinedLayers = playerLayer.value | obstacleLayer.value;

            // Cast ray to check for line of sight considering both player and obstacles
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, chaseDistance, combinedLayers))
            {
                // Check if the hit object is actually the player
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    return player;
                }
            }
        }
        return null;
    }
    private void Disrupt()
    {
        if(targetedEndpoint == null)
        {
            // Pick a random vulnerable system endpoint and move to disrupt
            if (levelData.endpoints.ContainsKey(systemToTarget) && levelData.endpoints[systemToTarget].Count > 0)
            {
                int index = Random.Range(0, levelData.endpoints[systemToTarget].Count);
                physicalEndpoint targetEndpoint = levelData.endpoints[systemToTarget][index];
                // Additional check: Verify the endpoint's current state is indeed vulnerable
                if (targetEndpoint.endpoint.state == EndpointState.Vulnerable)
                {
                    targetedEndpoint = targetEndpoint;
                    Debug.Log("I'm going to destroy: " + targetedEndpoint.gameObject.name);
                    nextActionTime = -1; // en route
                    navMeshAgent.SetDestination(targetEndpoint.transform.position);
                }
                else
                {
                    // No vulnerable targets found, reset targetedEndpoint and return to idling
                    targetedEndpoint = null;
                    currentState = State.idling;
                    return;
                }
            }
            else
            {
                // No vulnerable targets found, reset targetedEndpoint and return to idling
                targetedEndpoint = null;
                currentState = State.idling;
                return;
            }
        }
        // Check if Roxy has reached her destination
        if (targetedEndpoint != null && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending && nextActionTime == -1)
        {
            //Start timer
            Debug.Log("You better stop me.");
            nextActionTime = Time.time + disruptDelay + Random.Range(-2f, 2f);
        }
        //wait until ready to break, then go back to idling
        if (nextActionTime != -1 && Time.time >= nextActionTime)
        {
            targetedEndpoint.breakEndpoint();
            targetedEndpoint = null;
            currentState = State.idling;
        }
        // Check for player visibility to potentially switch to chasing
        if (CheckForPlayer() != null)
        {
            currentState = State.chasing;
        }
    }
}