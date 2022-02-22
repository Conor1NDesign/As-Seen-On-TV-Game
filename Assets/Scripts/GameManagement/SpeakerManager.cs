using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerManager : MonoBehaviour
{
    public GameManager gameManager;

    public List<AudioClip> laughTracks;

    private AudioSource audioSource;
    private AudioClip laughToPlay;


    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void AssignGameManager(GameManager target)
    {
        gameManager = target;
    }

    public void PlayLaughTrack()
    {
        int trackListNumber = Random.Range(0, laughTracks.Count);
        laughToPlay = laughTracks[trackListNumber];
        audioSource.clip = laughToPlay;
        audioSource.Play();
    }
}
