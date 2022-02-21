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

    private int layerMask = 6;

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

                /* The Old Way
                holdingObject = true;
                Debug.DrawRay(transform.position, transform.forward * castHit.distance, Color.yellow);

                //Assigns the liftable object to the graspedObject variable
                graspedObject = castHit.collider.gameObject;
                graspedObject.transform.parent = gameObject.transform;

                //Disable gravity and make the object Kinematic while it is in the player's grasp
                graspedObject.GetComponent<Rigidbody>().useGravity = false;
                graspedObject.GetComponent<Rigidbody>().isKinematic = true;
                */
            }
        }
        else if (context.canceled && graspedObject != null)
        {
            holdingObject = false;

            graspedObject.GetComponent<LiftableObject>().OnRelease();

            graspedObject = null;



            /* The Old Way
            //The player drops the item, gravity is turned back on and it is no longer kinematic.
            graspedObject.GetComponent<Rigidbody>().useGravity = true;
            graspedObject.GetComponent<Rigidbody>().isKinematic = false;

            //Un-parents the graspedObject and sets the variable to null to allow the player to pick up a new object
            graspedObject.transform.parent = null;
            graspedObject = null;

            holdingObject = false;
            */
        }    
    }

    /* Conor, do this:
     * Make the RaycastHit.point move a 'targetPosition' empty Game Object to it's position
     * Plug that target position into the Liftable object's own script with a GetComponent
     * Have the Liftable object continuously move towards the targetPosition while it's being held
     * instead of it being Kinematic.
     * 
     * Doing this, you should be able to keep gravity on the object, and still have it remain
     * in the 'held' position. Perhaps if the held position gets too far away from the object
     * then the grip breaks as well?
     * 
     * This move-towards idea could also give you the pseudo-"throwing" you want with the
     * physics?
     */
}
