using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Swarley : MonoBehaviour
{
    public int headTurnSpeed = 3;

    public Transform neckJoint;

    public GameObject swarleyIdlePos;
    private GameObject playerCamera;

    [HideInInspector]
    public bool lookAtPlayer = false;

    private bool firstConversation = true;
    private bool leftHanging = false;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
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
        ConversationManager.Instance.SetBool("leftHanging", leftHanging);
    }

    public void SetConversationVariables()
    {
        firstConversation = ConversationManager.Instance.GetBool("firstConversation");
        leftHanging = ConversationManager.Instance.GetBool("leftHanging");
    }

    public void RaiseHand()
    {
        if (leftHanging)
        {
            //Lower hand.
        }
        else
        {
            //Raise hand.
        }
    }

    private void Update()
    {
        if (!lookAtPlayer)
        {
            var lookPos = swarleyIdlePos.transform.position - neckJoint.position;
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
}
