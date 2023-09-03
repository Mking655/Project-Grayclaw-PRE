using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PingCountdown : MonoBehaviour
{
    [SerializeField]
    private RoxyAI roxyAI;
    private float countdownTime;
    //How long ping UI will stay after ping.
    [SerializeField]
    private float decayTime;
    [SerializeField]
    private GameObject notification;
    [SerializeField]
    private AudioClip pingingSound;
    private void OnEnable()
    {
        StartCoroutine(StartPing());
    }
    IEnumerator StartPing()
    {
        notification.SetActive(false);
        countdownTime = roxyAI.getPingTime();
        gameObject.GetComponent<AudioSource>().PlayOneShot(pingingSound);
        yield return new WaitForSeconds(countdownTime);
        yield return null;
        //if ping was successful and Roxy is in attack mode, notify player.
        if (roxyAI.getAttacking()) 
        {
            notifyPlayer();
        }
        yield return new WaitForSeconds(decayTime);
        gameObject.SetActive(false);
    }
    void notifyPlayer()
    {
        notification.SetActive(true);
    }
}
