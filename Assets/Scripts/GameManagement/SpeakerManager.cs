using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerManager : MonoBehaviour
{
    public GameManager gameManager;

    public List<AudioClip> laughTracks;

    public bool beingDestroyed = false;

    private AudioSource audioSource;
    private AudioClip laughToPlay;


    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (beingDestroyed && audioSource.isPlaying)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, 0, 0.001f);
        }
    }

    public void AssignGameManager(GameManager target)
    {
        gameManager = target;
    }

    public void PlayLaughTrack()
    {
        if (!audioSource.isPlaying)
        {
            int trackListNumber = Random.Range(0, laughTracks.Count);
            laughToPlay = laughTracks[trackListNumber];
            audioSource.clip = laughToPlay;
            audioSource.Play();
        }
    }

    public void OnSpeakerDestroy()
    {
        if (!beingDestroyed)
        {
            //Plays a final laugh track before exploding
            audioSource.Stop();
            int trackListNumber = Random.Range(0, laughTracks.Count);
            laughToPlay = laughTracks[trackListNumber];
            audioSource.clip = laughToPlay;
            audioSource.Play();
            //PlayLaughTrack();
            beingDestroyed = true;

            Invoke("EndMySuffering", 6);
        }
    }

    public void EndMySuffering()
    {
        gameManager.SpeakerDestroyed();
        //If you want to instantiate an explosion effect or something when the speaker is destroyed, start here...
        Destroy(gameObject);
    }
}
