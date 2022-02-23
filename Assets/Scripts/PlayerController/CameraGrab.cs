using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CameraGrab : MonoBehaviour
{
    [Header("Grab Range")]
    [Tooltip("The distance (in units) that the Player can grab objects from.")]
    [Range(5f, 10f)]
    public float grabRange = 3f;
    
    public RaycastHit castHit;

    public GameObject targetPosition;

    private bool holdingObject = false;
    private GameObject graspedObject;
    [HideInInspector]
    public GameObject conversationNPC;

    public CCFirstPerson_Controller characterController;


    private void Awake()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CCFirstPerson_Controller>();
    }

    private void Update()
    {
        if (holdingObject)
        {
            Debug.DrawRay(transform.position, transform.forward * castHit.distance, Color.yellow);
        }
    }

    public void OnGrabAction(CallbackContext context)
    {
        if (context.started)
        {
            //Mask 7 should be 'LiftableObject' and so the cast should only activate when touching a LiftableObject
            int mask = 1 << 7;
            if (Physics.Raycast(transform.position, transform.forward, out castHit, grabRange, mask) && graspedObject == null)
            {
                holdingObject = true;

                //Moves the grab targetPosition object to the raycast hit point
                targetPosition.transform.position = castHit.point;

                //Assigns the Liftable Object to the relevant variable
                graspedObject = castHit.collider.gameObject;

                graspedObject.GetComponent<LiftableObject>().OnPickup(targetPosition);
            }
        }
        else if (context.canceled && graspedObject != null)
        {
            holdingObject = false;

            graspedObject.GetComponent<LiftableObject>().OnRelease();

            graspedObject = null;
        }    
    }

    public void OnInteractAction(CallbackContext context)
    {
        int mask = 1 << 7;
        int npcMask = 1 << 8;
        if (context.started && Physics.Raycast(transform.position, transform.forward, out castHit, grabRange, mask))
        {
            GameObject hitObject = castHit.collider.gameObject;
            if (hitObject.tag == "Speaker")
            {
                hitObject.GetComponent<SpeakerManager>().PlayLaughTrack();
            }
        }
        else if (context.started && Physics.Raycast(transform.position, transform.forward, out castHit, grabRange, npcMask))
        {
            GameObject hitObject = castHit.collider.gameObject;
            if (conversationNPC == null)
            {
                conversationNPC = hitObject;
                hitObject.GetComponent<NPCConversationManager>().StartConversation();
            }
            else
                return;
        }
        else if (context.started && holdingObject == true)
        {
            //Do the throw thing here
            //graspedObject.GetComponent<LiftableObject>().ThrowObject(transform.forward);
        }
    }
}
