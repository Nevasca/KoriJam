using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataformTrigger : MonoBehaviour
    
    // <3 //
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Oi " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
