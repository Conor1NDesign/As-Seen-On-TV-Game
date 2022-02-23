using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class TimLooksAtCamera : MonoBehaviour
{
    public enum TimLooksAt
    {
        Camera,
        Player,
        Idle
    };

    public TimLooksAt timLooksAt = TimLooksAt.Idle;

    public float headTurnSpeed = 20;

    public GameObject cameraOnWall;

    private GameObject playerCamera;

    private int timCameraCount = 0;
    private int timPrankCount = 0;

    private bool playerLookAtCamera;
    private bool playerCameraControl;

    public Transform neckJoint;
    public Transform lookIdle;

    private bool firstConversation = true;
    private bool cameraSightUnlocked = false;
    private int timLooksAtCamera = 0;
    private int timPrankCounter = 0;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        if (playerCamera == null)
        {
            Debug.LogError("Tim can't find the player's camera what have you done?");
        }
    }

    public void GetConversationVariables()
    {
        ConversationManager.Instance.SetBool("firstConversation", firstConversation);
        ConversationManager.Instance.SetBool("cameraSightUnlocked", cameraSightUnlocked);
        ConversationManager.Instance.SetInt("timPrankCounter", timPrankCounter);
        ConversationManager.Instance.SetInt("timLooksAtCamera", timLooksAtCamera);
    }

    public void SetConversationVariables()
    {
        firstConversation = ConversationManager.Instance.GetBool("firstConversation");
        cameraSightUnlocked = ConversationManager.Instance.GetBool("cameraSightUnlocked");
        timPrankCounter = ConversationManager.Instance.GetInt("timPrankCounter");
        timLooksAtCamera = ConversationManager.Instance.GetInt("timLooksAtCamera");
    }

    public void LookAtCamera()
    {
        //Increments the 'jimLooksAtCamera' integer in the conversation tracker by 1
        timCameraCount++;
        ConversationManager.Instance.SetInt("timLooksAtCamera", timCameraCount);

        timLooksAt = TimLooksAt.Camera;
    }

    public void LookAtPlayer()
    {
        timLooksAt = TimLooksAt.Player;
    }

    public void LookAtIdle()
    {
        timLooksAt = TimLooksAt.Idle;
    }

    public void TimPrankCounter()
    {
        if (timPrankCount < 6)
            timPrankCount++;
        else if (timPrankCount >= 6)
            timPrankCount = 0;

        ConversationManager.Instance.SetInt("timPrankCounter", timPrankCount);
    }

    public void PlayerLookAtCamera()
    {
        playerCameraControl = true;
        if (!playerLookAtCamera)
        {
            playerLookAtCamera = true;

            playerCamera.GetComponent<CameraSight>().ActivateCameraSight();
        }
        else
            playerLookAtCamera = false;
    }

    public void RestorePlayerCameraControl()
    {
        playerCameraControl = false;
    }

    private void Update()
    {
        //Just what the heck is Tim looking at?
        if (timLooksAt == TimLooksAt.Idle)
        {
            var lookPos = lookIdle.position - neckJoint.position;
            var rotation = Quaternion.LookRotation(lookPos);
            neckJoint.transform.rotation = Quaternion.Slerp(neckJoint.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }
        else if (timLooksAt == TimLooksAt.Player)
        {
            var lookPos = playerCamera.transform.position - neckJoint.position;
            var rotation = Quaternion.LookRotation(lookPos);
            neckJoint.transform.rotation = Quaternion.Slerp(neckJoint.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }
        else if (timLooksAt == TimLooksAt.Camera)
        {
            var lookPos = cameraOnWall.transform.position - neckJoint.position;
            var rotation = Quaternion.LookRotation(lookPos);
            neckJoint.transform.rotation = Quaternion.Slerp(neckJoint.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }

        if (playerLookAtCamera && playerCameraControl)
        {
            var lookPos = cameraOnWall.transform.position - playerCamera.transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, headTurnSpeed * Time.deltaTime);
            //playerCameraControl = false;
        }
        else if (!playerLookAtCamera && playerCameraControl)
        {
            var lookPos = neckJoint.position - playerCamera.transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, headTurnSpeed * Time.deltaTime);
            //playerCameraControl = false;
        }
    }
}
