using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;

public class IntroConversation : MonoBehaviour
{
    [Range(0.01f, 0.5f)]
    public float lerpSpeed = 0.035f;
    public Image fadeImage;

    public int headTurnSpeed = 3;

    public GameObject meldon;
    private GameObject playerCamera;

    private bool fadeIn;
    private bool fadeOut;

    private bool playerLookSpeaker;

    public string playerName = "Speeve";

    public GameObject tutorialSpeaker;

    private void Awake()
    {
        Invoke("WakeUp", 0.5f);
        meldon = GameObject.Find("TutorialMeldon");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");

        //playerName = GameObject.Find("PlayerNameHolder").GetComponent<PlayerNameHolder>().playerName;
    }

    private void Update()
    {
        if (fadeIn)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(fadeImage.color.a, 0, lerpSpeed));
        }

        if (fadeOut)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(fadeImage.color.a, 1, lerpSpeed));
        }

        if (playerLookSpeaker)
        {
            var lookPos = tutorialSpeaker.transform.position - playerCamera.transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
        Invoke("StopFade", 4);
    }

    public void FadeOut()
    {
        fadeOut = true;
        Invoke("StopFade", 4);
    }

    public void StopFade()
    {
        if (fadeIn)
        {
            fadeImage.color = new Color(0, 0, 0, 0);
        }
        if (fadeOut)
        {
            fadeImage.color = new Color(0, 0, 0, 1);
        }
        fadeIn = false;
        fadeOut = false;
    }

    public void OverrideDialogueText(int dialogueInt)
    {
        if (dialogueInt == 0)
            ConversationManager.Instance.DialogueText.text = playerName + "...";
        else if (dialogueInt == 1)
            ConversationManager.Instance.DialogueText.text = playerName + "?";
    }

    public void LookAtMeldon()
    {
        meldon.GetComponent<MeldonIsAnnoying>().LookAtMeldon();
    }

    public void WakeUp()
    {
        gameObject.GetComponent<NPCConversationManager>().StartConversation();
    }

    public void MeldonLookAtPlayer()
    {
        meldon.GetComponent<MeldonIsAnnoying>().LookAtPlayer();
    }

    public void PlayerLookAtSpeaker()
    {
        if (playerLookSpeaker)
        {
            playerLookSpeaker = false;
        }
        else
            playerLookSpeaker = true;
    }
}
