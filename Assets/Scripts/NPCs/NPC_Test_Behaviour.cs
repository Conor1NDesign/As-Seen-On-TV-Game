using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC_Test_Behaviour : MonoBehaviour
{
    //This NPCConversation can be named anything, it's like a bool and such
    public NPCConversation Conversation;
    public bool isInRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //isInRange = true;
        ConversationManager.Instance.StartConversation(Conversation);
    }

    //void OnTriggerExit(Collider other)
    //{
       // isInRange = false;
  //  }

    // Update is called once per frame
    void Update()
    {
    //if (isInRange == true)
        //{
            //ConversationManager.Instance.StartConversation(Conversation);
       // }

    }
}
