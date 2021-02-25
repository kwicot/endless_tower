using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMainMenuScene : MonoBehaviour
{
    public Transform pnTimer;
    private void Start()
    {
        GameController.singleton.canvasMainMenuScene = this;
    }
    
    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        GameController.singleton.state = State.MainMenu;
        Time.timeScale = 1;
    }
}
