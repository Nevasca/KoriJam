using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable interactableOnRange;

    public void Interact()
    {
        interactableOnRange?.Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if(interactable != null)
        {
            interactableOnRange = interactable;
            interactableOnRange.OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if(interactable != null)
        {
            interactable.OnPlayerExit();
            interactableOnRange = null;
        }
    }
}