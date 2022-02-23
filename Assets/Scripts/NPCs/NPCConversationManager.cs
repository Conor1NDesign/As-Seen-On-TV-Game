using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCConversationManager : MonoBehaviour
{
    public NPCConversation conversationToStart;
    public CameraGrab playerCamera;

    private void Awake()
    {
        if (conversationToStart == null)
        {
            Debug.LogError("Hey " + gameObject + " doesn't have an NPCConversation attached to them!");
        }
    }

    public void StartConversation()
    {
        if (ConversationManager.Instance.IsConversationActive == true)
        {
            return;
        }
        else
        {
            ConversationManager.Instance.StartConversation(conversationToStart);
        }
    }


    public void EndConversation()
    {
        if (ConversationManager.Instance.IsConversationActive == true)
        {
            ConversationManager.Instance.EndConversation();
        }
        else
            return;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ConversationManager.Instance.IsConversationActive);
    }
}
