using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class DebugScreen : MonoBehaviour
{
    public InputActionProperty restartAction;

    private void Start()
    {
        restartAction.action.Enable();
        restartAction.action.started += RestartScene;
    }

    //[ContextMenu("Restart")]
    private void RestartScene(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        restartAction.action.started -= RestartScene;
        restartAction.action.Disable();
    }
}
