using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public const float BASE_GRAVITY = -40f;

    [SerializeField] private Transform characterMesh;
    [SerializeField] private Animator characterAnimator;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;

    [Header("Look")]
    [SerializeField] private Transform followTransform;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector2 movement;
    private Vector3 desiredMovement;
    private Vector3 velocity = Vector3.zero;
    private Vector3 characterOffsetRotation;
    private AudioSource playerAudio;

    public float Gravity { get { return Physics.gravity.y; } set { Physics.gravity = new Vector3(0f, value, 0f); } }
    public Vector3 Velocity { get { return rb.velocity; } set { rb.velocity = value; } }
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Gravity = BASE_GRAVITY;
        cameraTransform = Camera.main.transform;
        characterOffsetRotation = characterMesh.rotation.eulerAngles;
        characterAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    public void SetMovement(Vector2 movement)
    {
        this.movement = movement;
    }

    public void SetLook(Vector2 look)
    {

    }

    public void Move()
    {
        desiredMovement = cameraTransform.forward * movement.y + cameraTransform.right * movement.x;
        desiredMovement *= movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + desiredMovement);

        characterMesh.transform.eulerAngles = Quaternion.LookRotation(cameraTransform.forward).eulerAngles + characterOffsetRotation;
        characterAnimator.SetBool("isRunning", movement.sqrMagnitude > 0f);

        // float step = movementSpeed * Time.deltaTime;
        // desiredMovement = cameraTransform.forward * movement.y + cameraTransform.right * movement.x;
        // rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + desiredMovement, step));

        followTransform.localEulerAngles = new Vector3(followTransform.localEulerAngles.x, 0f, 0f);
    }

    private void CheckGround()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
    }

    public void Jump()
    {
        if (!IsGrounded)
            return;

        velocity = rb.velocity;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * BASE_GRAVITY);
        rb.velocity = velocity;
        playerAudio.pitch = Random.Range(0.8f, 1.1f);
        playerAudio.Play();
    }

    public void ResetGravity()
    {
        Gravity = BASE_GRAVITY;
    }
}