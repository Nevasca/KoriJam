using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDevice : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] activables;
    [SerializeField] private bool disableAfterUse = true;
    [SerializeField] private GameObject focusCamera;
    [SerializeField] private float focusTime;
    [SerializeField] private GameObject buttonDisplay;

    public void Interact()
    {
        if(!enabled)
            return;            

        foreach(var g in activables)
            g.GetComponent<IActivable>().ToggleActive();

        if(focusCamera != null)
            StartCoroutine(FocusActivable());

        if(disableAfterUse)
        {
            GetComponent<Collider>().enabled = false;
            buttonDisplay.SetActive(false);    
            enabled = false;
        }
    }

    public void OnPlayerEnter()
    {
        if(!enabled)
            return;

        buttonDisplay.SetActive(true);
    }

    public void OnPlayerExit()
    {
        if(!enabled)
            return;

        buttonDisplay.SetActive(false);
    }

    IEnumerator FocusActivable()
    {
        focusCamera.SetActive(true);
        yield return new WaitForSeconds(focusTime);
        focusCamera.SetActive(false);
    }
}