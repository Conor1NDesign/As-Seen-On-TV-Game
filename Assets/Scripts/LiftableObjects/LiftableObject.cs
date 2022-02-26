using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftableObject : MonoBehaviour
{
    [Tooltip("This is the max speed that an object will move towards the target position while being held.")]
    public float carrySpeed;

    [Tooltip("The weight of the lerp applied to the object. A lower value will make the object appear to be heavier using MATH")]
    [Range(0.01f, 0.5f)]
    public float lerpValue = 0.035f;

    [Tooltip("The strength of the force applied to an object when it is released by the Player")]
    [Range(1, 10)]
    public int throwStrength = 2;

    
    public GameObject targetPosition;

    public bool beingHeld = false;

    public bool wallMounted = false;

    private Vector3 storedVelocity;
    private Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (wallMounted)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition != null)
        {
            MoveTowardsTarget();
        }
    }

    public void OnPickup(GameObject target)
    {
        //rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        targetPosition = target;
        beingHeld = true;
        //rb.useGravity = false;
    }

    public void OnRelease()
    {
        targetPosition = null;
        beingHeld = false;
        rb.velocity = storedVelocity * throwStrength;
        rb.useGravity = true;
    }

    public void ThrowObject(Vector3 cameraForward)
    {
        targetPosition = null;
        beingHeld = false;
        rb.velocity = (storedVelocity + (cameraForward * throwStrength)) * throwStrength;
    }

    public void MoveTowardsTarget()
    {
        rb.velocity = Vector3.zero;
        float xVelocity = Mathf.Lerp(transform.position.x, targetPosition.transform.position.x, lerpValue);
        float yVelocity = Mathf.Lerp(transform.position.y, targetPosition.transform.position.y, lerpValue);
        float zVelocity = Mathf.Lerp(transform.position.z, targetPosition.transform.position.z, lerpValue);

        Vector3 moveVelocity = new Vector3(xVelocity, yVelocity, zVelocity);
        storedVelocity = targetPosition.transform.position - transform.position;


        transform.position = Vector3.MoveTowards(transform.position, moveVelocity, carrySpeed * Time.deltaTime);
    }
}
