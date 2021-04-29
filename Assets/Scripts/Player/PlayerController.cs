using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private bool init;
    private Vector3 movement;
    private bool holdingJump;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        init = true;
    }

    private void Update()
    {
        if (!CanReceiveInput())
            return;

        playerMovement.Move(movement);
        playerMovement.Fly(holdingJump);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0f, input.y);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (!CanReceiveInput())
            return;

        if (value.started)
            playerMovement.Jump();

        holdingJump = value.performed;
    }

    private bool CanReceiveInput()
    {
        return init;
    }
}
