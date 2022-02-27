using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenCamera : MonoBehaviour
{
    public List<GameObject> linkedSpeakers;

    public void PlayLaughTracks()
    {
        foreach (var item in linkedSpeakers)
        {
            item.GetComponent<SpeakerManager>().PlayLaughTrack();
        }
    }
}
