using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Objective Variables")]
    public int speakersRemaining;
    public bool allSpeakersDestroyed = false;

    public void Awake()
    {
        speakersRemaining = GameObject.FindGameObjectsWithTag("Speaker").Length;

        foreach (var item in GameObject.FindGameObjectsWithTag("Speaker"))
        {
            item.GetComponent<SpeakerManager>().AssignGameManager(gameObject.GetComponent<GameManager>());
        }
    }

    public void SpeakerDestroyed()
    {
        speakersRemaining--;
        //Do other stuff here, UI elements or whatever...
    }
}
