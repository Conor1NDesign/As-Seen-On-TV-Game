using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequence : MonoBehaviour
{
    public GameObject player;
    public GameObject endingStandPos;
    public GameObject playerCamera;
    public GameObject endingCamera;
    public GameObject audienceLookPos;
    public GameObject gameplayUI;
    public GameObject quitButton;
    private bool controlPlayerCamera = false;
    private int headTurnSpeed = 3;
    private bool credits = false;

    public List<TMPro.TextMeshProUGUI> creditsNames;
    public List<TMPro.TextMeshProUGUI> creditsJobs;

    public List<GameObject> npcsToHide;
    public List<GameObject> npcsToSpawn;
    public List<GameObject> endingSpeakers;

    private void Update()
    {
        if (controlPlayerCamera)
        {
            var lookPos = audienceLookPos.transform.position - playerCamera.transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, headTurnSpeed * Time.deltaTime);

            player.transform.position = Vector3.MoveTowards(player.transform.position, endingStandPos.transform.position, 3 * Time.deltaTime);
        }

        if (credits)
        {
            foreach (var item in creditsNames)
            {
                item.color = new Color(255, 255, 255, Mathf.Lerp(item.color.a, 1, 0.001f));
            }

            foreach (var item in creditsJobs)
            {
                item.color = new Color(190, 190, 190, Mathf.Lerp(item.color.a, .75f, 0.001f));
            }
        }
    }

    public void MoveNPCs()
    {
        foreach (var item in npcsToHide)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in npcsToSpawn)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void OnEndingTrigger()
    {
        controlPlayerCamera = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<CCFirstPerson_Controller>().canMove = false;

        foreach (var item in endingSpeakers)
        {
            item.GetComponent<SpeakerManager>().PlayLaughTrack();
        }

        Invoke("SwapCameras", 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnEndingTrigger();
        }
    }

    public void SwapCameras()
    {
        DisableMainUI();
        playerCamera.SetActive(false);
        controlPlayerCamera = false;

        endingCamera.SetActive(true);

        Invoke("CreditsFadeIn", 4);
    }

    public void DisableMainUI()
    {
        gameplayUI.SetActive(false);
    }

    public void CreditsFadeIn()
    {
        credits = true;
        Invoke("QuitButtonAppears", 6);
    }

    public void QuitButtonAppears()
    {
        quitButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
