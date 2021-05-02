using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour, IActivable
{
    public enum PlataformState { Moving, Waiting, Disabled}

    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private PlataformState currentState;

    private Rigidbody rb;
    private int indexPoint = 0;
    private float timerWait = 0f;
    private Vector3 distanceToDestination;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case PlataformState.Moving:
                Move();
                break;
            case PlataformState.Waiting:
                Wait();
                break;
        }
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        rb.MovePosition(Vector3.MoveTowards(transform.position, points[indexPoint].position, step));

        distanceToDestination = points[indexPoint].position - transform.position;
        if(distanceToDestination.sqrMagnitude < 0.001f)
        {
            currentState = PlataformState.Waiting;
            indexPoint = (indexPoint + 1) % points.Length;
        }
    }

    private void Wait()
    {
        timerWait += Time.deltaTime;

        if(timerWait >= waitTime)
        {
            currentState = PlataformState.Moving;
            timerWait = 0f;
        }
    }

    public void ToggleActive()
    {
        currentState = currentState == PlataformState.Disabled ? PlataformState.Moving : PlataformState.Disabled;
    }

    public bool IsActive()
    {
        return currentState != PlataformState.Disabled;
    }
}