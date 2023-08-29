using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// When gameobject is enabled, this script waits a certian ammount of time, then does something.
/// </summary>
public class LoadingDelay : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private UnityEvent onComplete;

    private void OnEnable()
    {
        StartCoroutine(loadDelay());
    }
    IEnumerator loadDelay()
    {
        yield return new WaitForSeconds(waitTime);
        onComplete.Invoke();
    }
}
