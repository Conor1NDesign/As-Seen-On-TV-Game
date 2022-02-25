using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybind_FadeIn : MonoBehaviour
{
    public Image keybindImage;
    [Range(0.01f, 0.5f)]
    public float lerpSpeed = 0.01f;

    public GameObject button;

    private bool fadeIn = false;

    private void Update()
    {
        if (fadeIn)
        {
            keybindImage.color = new Color(255, 255, 255, Mathf.Lerp(keybindImage.color.a, 1, lerpSpeed));
        }

        if (!fadeIn)
        {
            keybindImage.color = new Color(255, 255, 255, Mathf.Lerp(keybindImage.color.a, 0, lerpSpeed));
        }
    }

    public void KeybindsFadeIn()
    {
        fadeIn = true;
        button.SetActive(true);

        Invoke("AllowCursor", 1);
    }

    public void KeybindsFadeOut()
    {
        fadeIn = false;
        button.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameObject.FindGameObjectWithTag("Player").GetComponent<CCFirstPerson_Controller>().canMove = true;

        Invoke("DeactivateMe", 12);
    }

    public void DeactivateMe()
    {
        gameObject.SetActive(false);
    }

    public void AllowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameObject.FindGameObjectWithTag("Player").GetComponent<CCFirstPerson_Controller>().canMove = false;
    }
}
