using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IActivable
{
    [SerializeField] private int priority;

    public int Priority { get {return priority; }}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            CheckpointManager.Instance.SetCheckPoint(this);
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public void ToggleActive()
    {
        gameObject.SetActive(true);
    }
}