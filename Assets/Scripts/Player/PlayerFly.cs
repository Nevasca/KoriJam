using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour, IWindInteractable
{
    private CharacterController characterController;
    private PlayerMovement playerMovement;

    private bool isFlying;
    private bool insideWind;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isFlying && characterController.isGrounded)
            StopFlying();
    }

    public void SetFlying(bool flying)
    {
        if (!flying)
            return;

        if (flying && isFlying)
            return;

        if (flying && !CanFly())
            return;

        isFlying = true;
        Vector3 velocity = playerMovement.Velocity;        
        velocity.y = insideWind ? velocity.y * 0.1f : 0f;

        playerMovement.Velocity = velocity;
        playerMovement.Gravity = PlayerMovement.BASE_GRAVITY / 10f;        
    }

    private void StopFlying()
    {
        playerMovement.ResetGravity();
        playerMovement.Velocity = Vector3.zero;
        isFlying = false;
    }

    private bool CanFly()
    {
        return playerMovement.Velocity.y < 0f || insideWind;
    }

    #region Wind
    public void AddWindForce(Vector3 force)
    {
        if (!isFlying)
            return;

        playerMovement.Velocity += force;
    }

    public Vector3 GetVelocity()
    {
        return playerMovement.Velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        if (!isFlying)
            return;

        playerMovement.Velocity = velocity;
    }

    public void SetInsideWind(bool value)
    {
        insideWind = value;
    }
    #endregion
}