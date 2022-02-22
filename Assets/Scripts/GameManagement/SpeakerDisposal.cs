using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerDisposal : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Speaker")
        {
            other.GetComponent<SpeakerManager>().OnSpeakerDestroy();
        }
    }
}
