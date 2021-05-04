using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transition transition;

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
        
        transition.FadeIn(0.1f, PlacePlayer);
    }

    private void PlacePlayer()
    {
        if(lastCheckpoint != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            
            rb.isKinematic = true;
            player.position = lastCheckpoint.transform.position;
            player.rotation = lastCheckpoint.transform.rotation;
            rb.isKinematic = false;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        transition.FadeOut(1f, null, 0.5f);
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