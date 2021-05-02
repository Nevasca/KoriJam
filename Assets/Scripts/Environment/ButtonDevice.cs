using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDevice : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] activables;
    [SerializeField] private bool disableAfterUse = true;
    [SerializeField] private GameObject focusCamera;
    [SerializeField] private float focusTime;

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
            enabled = false;
        }
    }

    public void OnPlayerEnter()
    {
        if(!enabled)
            return;
    }

    public void OnPlayerExit()
    {
        if(!enabled)
            return;
    }

    IEnumerator FocusActivable()
    {
        focusCamera.SetActive(true);
        yield return new WaitForSeconds(focusTime);
        focusCamera.SetActive(false);
    }
}