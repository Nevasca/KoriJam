using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null) 
        {
            controller.enabled = false;
            controller.transform.position = destination.position;
            controller.enabled = true;
        }
    }
}
