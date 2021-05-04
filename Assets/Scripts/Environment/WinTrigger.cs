using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private Transition transition;
    [SerializeField] private GameObject winText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))        
        {
            other.GetComponent<PlayerController>().EnableInputs(false);
            other.GetComponent<PlayerMovement>().enabled = false;
            other.GetComponent<PlayerFly>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;

            transition.FadeIn(1f, ShowWinText);
        }
    }

    private void ShowWinText()
    {
        winText.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //winText.GetComponent<TextMeshProUGUI>().enabled = true;
    }
}