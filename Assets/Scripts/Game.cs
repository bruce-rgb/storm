using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game obj;

    public int mxLives = 3;

    public bool gamePaused = false;
    public int score = 0;

    private void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }

    public void AddScore(int scoreGive)
    {
        score += scoreGive;
        UIManager.obj.UpdateScore();
    }

    public void GameOver()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Regarga escena para GameOver
    }

    void OnDestroy()
    {
        obj = this;
    }

}
