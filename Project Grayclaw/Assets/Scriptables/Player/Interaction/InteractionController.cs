using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private List<InteractionPoint> points;
    [Tooltip("The location where the player interaction controller currently is. Will change throughout game as player interacts with diffrent points.")]
    [SerializeField]
    private InteractionPoint activePoint;

    private void Awake()
    {
        //injection of self into all points
        foreach(InteractionPoint point in points) 
        {
            point.interactionController = this;
        }
        gameObject.transform.rotation = activePoint.transform.rotation;
        gameObject.transform.position = activePoint.transform.position;
    }
    public void changeActivePoint(InteractionPoint point)
    {
        activePoint = point;
        gameObject.transform.rotation = activePoint.transform.rotation;
        gameObject.transform.position = activePoint.transform.position;
    }
}
