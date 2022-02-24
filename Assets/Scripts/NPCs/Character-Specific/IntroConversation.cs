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

    public GameObject meldon;
    private bool fadeIn;
    private bool fadeOut;

    private void Awake()
    {
        Invoke("WakeUp", 0.5f);
        meldon = GameObject.Find("Meldon");
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
}
