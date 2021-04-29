using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float baseGravity = -9.81f;

    [Header("Look")]
    [SerializeField] private Transform followTransform;
    [SerializeField] private float rotationPower = 3f;
    [SerializeField] private float rotationLerp = 0.5f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 5f;

    private CharacterController characterController;

    private Vector3 desiredMovement;
    private Quaternion nextRotation;

    private Vector3 velocity = Vector3.zero;
    private float defaultStepOffset;
    private float gravity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        defaultStepOffset = characterController.stepOffset;
        gravity = baseGravity;
    }

    public void Move(Vector2 movement)
    {
        if (characterController.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        characterController.stepOffset = characterController.isGrounded ? defaultStepOffset : 0.01f;

        desiredMovement = transform.forward * movement.y + transform.right * movement.x;
        characterController.Move(desiredMovement * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Look(Vector2 look)
    {
        //Horizontal follow rotation
        followTransform.rotation *= Quaternion.AngleAxis(look.x * rotationPower, Vector3.up);

        //Vertical follow rotation
        followTransform.rotation *= Quaternion.AngleAxis(-look.y * rotationPower, Vector3.right);

        Vector3 angles = followTransform.localEulerAngles;
        angles.z = 0f;

        float angle = followTransform.localEulerAngles.x;
        if (angle > 180f && angle < 350f)
            angles.x = 350f;
        else if (angle < 180 && angle > 40f)
            angles.x = 40f;

        followTransform.localEulerAngles = angles;
        nextRotation = Quaternion.Lerp(followTransform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        //Character rotation
        transform.rotation = Quaternion.Euler(0f, followTransform.rotation.eulerAngles.y, 0f);

        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0f, 0f);
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