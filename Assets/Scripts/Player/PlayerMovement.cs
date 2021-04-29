using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float baseGravity = -9.81f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 5f;

    private CharacterController characterController;
    private Vector3 desiredMovement;
    private Vector3 velocity = Vector3.zero;
    private float defaultStepOffset;
    private float gravity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        defaultStepOffset = characterController.stepOffset;
        gravity = baseGravity;        
    }

    public void Move(Vector3 movement)
    {
        if (characterController.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        characterController.stepOffset = characterController.isGrounded ? defaultStepOffset : 0.01f;

        desiredMovement = movement * movementSpeed * Time.deltaTime;
        characterController.Move(desiredMovement);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(!characterController.isGrounded)
            return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Fly(bool fly)
    {
        if (velocity.y >= 0f)
            return;

        gravity = fly ? baseGravity / 10f : baseGravity;
    }
}