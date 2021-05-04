using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject tutorialDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            tutorialDisplay.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            tutorialDisplay.SetActive(false);
        }
    }
}
