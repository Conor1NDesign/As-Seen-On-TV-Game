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

    public GameObject targetPosition;

    public bool beingHeld = false;

    private Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        targetPosition = target;
        beingHeld = true;
        rb.useGravity = false;
    }

    public void OnRelease()
    {
        targetPosition = null;
        beingHeld = false;
        rb.useGravity = true;
    }

    public void MoveTowardsTarget()
    {
        float xVelocity = Mathf.Lerp(transform.position.x, targetPosition.transform.position.x, lerpValue);
        float yVelocity = Mathf.Lerp(transform.position.y, targetPosition.transform.position.y, lerpValue);
        float zVelocity = Mathf.Lerp(transform.position.z, targetPosition.transform.position.z, lerpValue);

        Vector3 moveVelocity = new Vector3(xVelocity, yVelocity, zVelocity);

        transform.position = Vector3.MoveTowards(transform.position, moveVelocity, carrySpeed * Time.deltaTime);
    }
}
