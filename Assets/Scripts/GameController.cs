using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController singleton { get; private set; }

    public Tower _Tower { get; set; }



    //Временное
    public float SpawnInterval;
    public float WaveInterval;
    public int EnemyPerWave;
    //Временное



    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }


    public void StartGame(int level)
    {
        SceneManager.LoadScene(level);
    }
}
