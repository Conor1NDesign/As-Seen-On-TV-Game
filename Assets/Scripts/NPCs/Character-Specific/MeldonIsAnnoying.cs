using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeldonIsAnnoying : MonoBehaviour
{
    public int headTurnSpeed = 3;

    public Transform neckJoint;

    public GameObject meldonIdlePos;
    private GameObject playerCamera;

    [HideInInspector]
    public bool playerLookAtMeldon;

    [HideInInspector]
    public bool lookAtPlayer = false;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    public void LookAtMeldon()
    {
        if (playerLookAtMeldon)
            playerLookAtMeldon = false;
        else
            playerLookAtMeldon = true;
    }

    public void LookAtPlayer()
    {
        if (!lookAtPlayer)
            lookAtPlayer = true;
        else
            lookAtPlayer = false;
    }

    private void Update()
    {
        if (playerLookAtMeldon)
        {
            var lookPos = neckJoint.position - playerCamera.transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, rotation, headTurnSpeed * Time.deltaTime);
        }

        if (!lookAtPlayer)
        {
            var lookPos = meldonIdlePos.transform.position - neckJoint.position;
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
