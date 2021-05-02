using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerZone : MonoBehaviour
{
    [SerializeField] private GameObject cameraToActive;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            cameraToActive.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            cameraToActive.SetActive(false);
    }
}