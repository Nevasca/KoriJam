using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerFly playerFly;
    private PlayerInteraction playerInteraction;

    private bool init;
    private Vector2 movement;
    private Vector2 look;
    private bool holdingJump;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerFly = GetComponent<PlayerFly>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        init = true;
    }

    private void Update()
    {
        if (!CanReceiveInput())
            return;

        playerMovement.SetMovement(movement);
        playerFly.SetFlying(holdingJump);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        movement = value.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        look = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (!CanReceiveInput())
            return;

        if (value.started)
            playerMovement.Jump();

        holdingJump = value.performed;
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if(!CanReceiveInput())
            return;
        
        if(value.started)
            playerInteraction.Interact();
    }

    private bool CanReceiveInput()
    {
        return init;
    }
}
