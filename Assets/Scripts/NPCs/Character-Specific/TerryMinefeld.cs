using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TerryMinefeld : MonoBehaviour
{
    public int headTurnSpeed = 3;

    public int terryStandUpCounter = 0;

    public Transform neckJoint;

    public GameObject terryIdlePos;
    private GameObject playerCamera;

    public bool lookAtPlayer = false;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    private void Update()
    {
        if (!lookAtPlayer)
        {
            var lookPos = terryIdlePos.transform.position - neckJoint.position;
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

    public void LookAtPlayer()
    {
        if (!lookAtPlayer)
            lookAtPlayer = true;
        else
            lookAtPlayer = false;
    }


    public void GetConversationVariables()
    {
        ConversationManager.Instance.SetInt("terryStandUpCounter", terryStandUpCounter);
    }

    public void SetConversationVariables()
    {
        terryStandUpCounter = ConversationManager.Instance.GetInt("terryStandUpCounter");
    }

    public void TerryStandUpCounterIncrease()
    {
        terryStandUpCounter++;
        ConversationManager.Instance.SetInt("terryStandUpCounter", terryStandUpCounter);
    }
}
