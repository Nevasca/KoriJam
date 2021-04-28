using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private bool init;
    private Vector3 movement;

    private void Awake()
    {
        
    }

    private void SetReferences()
    {

    }

    private void FixedUpdate()
    {
        playerMovement.Move(movement);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        movement = new Vector3(input.x, 0f, input.y);
    }

    private bool CanReceiveInput()
    {
        return playerMovement != null;
    }
}
