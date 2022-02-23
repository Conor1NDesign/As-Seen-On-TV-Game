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

    public Transform neckJoint;
    public Transform lookIdle;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        if (playerCamera == null)
        {
            Debug.LogError("Tim can't find the player's camera what have you done?");
        }
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
        if (!playerLookAtCamera)
        {
            playerLookAtCamera = true;

            playerCamera.GetComponent<CameraSight>().ActivateCameraSight();
        }
        else
            playerLookAtCamera = false;
    }

    private void Update()
    {
        //Just what the heck is Tim looking at?
        if (timLooksAt == TimLooksAt.Idle)
        {
            neckJoint.transform.rotation = Quaternion.RotateTowards(neckJoint.transform.rotation, lookIdle.rotation, headTurnSpeed * Time.deltaTime);
        }
        else if (timLooksAt == TimLooksAt.Player)
        {
            neckJoint.transform.rotation = Quaternion.RotateTowards(neckJoint.transform.rotation, playerCamera.transform.rotation, headTurnSpeed * Time.deltaTime);
        }
        else if (timLooksAt == TimLooksAt.Camera)
        {
            neckJoint.transform.rotation = Quaternion.RotateTowards(neckJoint.transform.rotation, cameraOnWall.transform.rotation, headTurnSpeed * Time.deltaTime);
        }

        if (playerLookAtCamera)
        {
            playerCamera.transform.rotation = Quaternion.RotateTowards(playerCamera.transform.rotation, cameraOnWall.transform.rotation, headTurnSpeed * Time.deltaTime);
        }
        else
        {
            playerCamera.transform.rotation = Quaternion.RotateTowards(playerCamera.transform.rotation, neckJoint.rotation, headTurnSpeed * Time.deltaTime);
        }
    }
}
