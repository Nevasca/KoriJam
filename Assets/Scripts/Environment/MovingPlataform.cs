using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour
{
    public enum PlataformState { Moving, Waiting, Disabled}

    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private PlataformState currentState;

    private int indexPoint = 0;
    private float timerWait = 0f;

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
        //transform.position = Vector3.MoveTowards(transform.position, points[indexPoint].position, step);
        GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, points[indexPoint].position, step));

        if(Vector3.Distance(transform.position, points[indexPoint].position) < 0.001f)
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
}