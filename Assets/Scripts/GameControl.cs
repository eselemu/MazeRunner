using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl GM;
    public int lives;
    public int maxLives;

    void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        maxLives = 3;
        lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void GoToMaze()
    {
        SceneManager.LoadScene("Maze");
    }

    public static void GoToMiniGame()
    {
        SceneManager.LoadScene("MiniGame");
    }

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
