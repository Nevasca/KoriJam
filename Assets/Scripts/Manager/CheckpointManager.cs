using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private Checkpoint lastCheckpoint;
    private Transform player;

    public static CheckpointManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetPlayerReference();
    }

    public void RestartFromLastCheckpoint()
    {
        if(player == null)
            SetPlayerReference();
        
        if(lastCheckpoint != null)
        {
            player.position = lastCheckpoint.transform.position;
            player.rotation = lastCheckpoint.transform.rotation;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SetCheckPoint(Checkpoint checkpoint)
    {        
        if(checkpoint == null)
            lastCheckpoint = null;
        else if(lastCheckpoint == null || 
                checkpoint.Priority > lastCheckpoint.Priority)
            lastCheckpoint = checkpoint;
    }

    private void SetPlayerReference()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}