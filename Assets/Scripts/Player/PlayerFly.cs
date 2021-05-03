using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour, IWindInteractable
{
    //[SerializeField] private float airResistance = 0.2f;

    private PlayerMovement playerMovement;
    private Animator playerAnimator;

    private bool isFlying;
    private bool insideWind;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFlying && playerMovement.IsGrounded)
            StopFlying();
        else if (isFlying)
            Fly();
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
        playerAnimator.speed = 0.3f;
    }

    private void Fly()
    {
        //Aplies air resistence
        //if(!insideWind && playerMovement.Velocity.sqrMagnitude > 0.1f)
        //    playerMovement.Velocity -= playerMovement.Velocity * airResistance * Time.deltaTime;
    }

    private void StopFlying()
    {
        playerMovement.ResetGravity();
        playerMovement.Velocity = Vector3.zero;
        isFlying = false;
        playerAnimator.speed = 1f;
    }

    private bool CanFly()
    {
        //return playerMovement.Velocity.y < 0f || insideWind;
        return playerMovement.Velocity.y < 0f;
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