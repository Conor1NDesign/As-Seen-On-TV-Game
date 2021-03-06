using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CameraSight : MonoBehaviour
{
    public CCFirstPerson_Controller playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CCFirstPerson_Controller>();

        foreach (var item in GameObject.FindGameObjectsWithTag("HiddenCamera"))
        {
            item.GetComponent<Collider>().enabled = false;
        }
    }

    public void ActivateCameraSight()
    {
        playerController.cameraSightActive = true;

        ConversationManager.Instance.SetBool("cameraSightUnlocked", true);

        foreach (var item in GameObject.FindGameObjectsWithTag("HiddenCamera"))
        {
            item.gameObject.layer = 7;
            item.GetComponent<Collider>().enabled = true;
        }
    }
}
