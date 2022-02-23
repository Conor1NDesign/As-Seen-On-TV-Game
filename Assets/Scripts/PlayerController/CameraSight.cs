using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSight : MonoBehaviour
{
    public CCFirstPerson_Controller playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CCFirstPerson_Controller>();
    }

    public void ActivateCameraSight()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("HiddenCamera"))
        {
            playerController.cameraSightActive = true;
            gameObject.layer = 0;
        }
    }
}
