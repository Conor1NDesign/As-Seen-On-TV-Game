using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TutorialSpeakers : MonoBehaviour
{
    public List<GameObject> tutorialSpeakers;

    private int speakersRemaining;

    public int headTurnSpeed = 3;
    private bool playerLookWindow = false;

    private GameObject playerCamera;
    public GameObject meldonTutorial;
    public GameObject meldonReal;
    public GameObject tutorialWindow;
    public GameObject tutorialUI;
    public GameObject speakersUI;

    public NPCConversation conversationToStart;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");

        foreach (var item in tutorialSpeakers)
        {
            item.GetComponent<SpeakerManager>().forTutorial = true;
        }

        speakersRemaining = tutorialSpeakers.Count;
    }

    private void Update()
    {
        if (playerLookWindow)
        {
            var lookPos = tutorialWindow.transform.position - playerCamera.transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }
    }

    public void SpeakerDestroyed()
    {
        speakersRemaining--;

        if (speakersRemaining <= 0)
        {
            ConversationManager.Instance.StartConversation(conversationToStart);
        }
    }

    public void LookOutWindow()
    {
        playerLookWindow = true;
    }

    public void EndTutorial()
    {
        RemoveTutorialMeldon();
        playerLookWindow = false;
        speakersUI.SetActive(true);
    }

    public void RemoveTutorialMeldon()
    {
        meldonTutorial.SetActive(false);
        meldonReal.SetActive(true);
    }
}
