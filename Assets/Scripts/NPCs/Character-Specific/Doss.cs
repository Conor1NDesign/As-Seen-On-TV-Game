using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Doss : MonoBehaviour
{
    public int headTurnSpeed = 3;

    public Transform neckJoint;

    public GameObject dossIdlePos;
    private GameObject playerCamera;

    [HideInInspector]
    public bool lookAtPlayer = false;

    public string playerName = "Speeve";

    private bool firstConversation = true;


    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");

        //playerName = GameObject.Find("PlayerNameHolder").GetComponent<PlayerNameHolder>().playerName;
    }

    public void LookAtPlayer()
    {
        if (!lookAtPlayer)
            lookAtPlayer = true;
        else
            lookAtPlayer = false;
    }

    public void GetConversationVariables()
    {
        ConversationManager.Instance.SetBool("firstConversation", firstConversation);
    }

    public void SetConversationVariables()
    {
        firstConversation = ConversationManager.Instance.GetBool("firstConversation");
    }

    private void Update()
    {
        if (!lookAtPlayer)
        {
            var lookPos = dossIdlePos.transform.position - neckJoint.position;
            var rotation = Quaternion.LookRotation(lookPos);
            neckJoint.transform.rotation = Quaternion.Slerp(neckJoint.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }
        else
        {
            var lookPos = playerCamera.transform.position - neckJoint.position;
            var rotation = Quaternion.LookRotation(lookPos);
            neckJoint.transform.rotation = Quaternion.Slerp(neckJoint.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }
    }

    public void OverrideDialogueText(int dialogueInt)
    {
        if (dialogueInt == 0)
            ConversationManager.Instance.DialogueText.text = "Ah, " + playerName + "... I could sense it was you.";
    }
}
