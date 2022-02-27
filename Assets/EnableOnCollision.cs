using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnCollision : MonoBehaviour
{
    public GameObject lights;

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            lights.SetActive(true);
        }

    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            lights.SetActive(false);
        }
    }
}
