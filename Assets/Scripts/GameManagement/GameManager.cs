using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class GameManager : MonoBehaviour
{
    [Header("Objective Variables")]
    public int speakersRemaining;
    public bool allSpeakersDestroyed = false;

    public TMPro.TextMeshProUGUI speakerUINumber;

    public NPCConversation endingConversation;

    public GameObject endingWall;

    public void Awake()
    {
        speakersRemaining = GameObject.FindGameObjectsWithTag("Speaker").Length;
        speakerUINumber.text = speakersRemaining.ToString();

        foreach (var item in GameObject.FindGameObjectsWithTag("Speaker"))
        {
            item.GetComponent<SpeakerManager>().AssignGameManager(gameObject.GetComponent<GameManager>());
        }
    }

    public void SpeakerDestroyed()
    {
        speakersRemaining--;
        //Do other stuff here, UI elements or whatever...
        speakerUINumber.text = speakersRemaining.ToString();
        if (speakersRemaining <= 0)
        {
            EndingTime();
        }
    }

    public void EndingTime()
    {
        endingWall.SetActive(false);
        ConversationManager.Instance.EndConversation();
        gameObject.GetComponent<EndingSequence>().MoveNPCs();
        ConversationManager.Instance.StartConversation(endingConversation);
    }
}
