using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchKick : MonoBehaviour

{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;
    AudioClip lastClip;

    void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.tag == "Fighter")
        {
            audioSource.PlayOneShot(RandomClip());
        }
    }
 
    AudioClip RandomClip()
    {
        AudioClip newClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        while (newClip == lastClip)
        {
            newClip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        }
        lastClip = newClip;
        return newClip;
    }
}