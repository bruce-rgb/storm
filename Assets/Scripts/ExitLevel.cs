using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public int scene;

    public GameObject fade;
    public GameObject canvas;
    public GameObject canvasGameOver;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioController.obj.PlayExitLevel();
            fade.SetActive(true);
            canvas.SetActive(false);
            Invoke("ChangeLevel", 1.2f);
        }
    }

    private void Update()
    {
        //Desplegar GameOver
        if (Player.obj.lives == 0)
        {
            Debug.Log("GameOver");
            //stop gameplay music
            UIManager.obj.gamePause = true;
            fade.SetActive(true);
            canvas.SetActive(false);
            canvasGameOver.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(1);
        }
        else SceneManager.LoadScene(0);
    }

    //Cambia nivel cuando se toca la puerta
    public void ChangeLevel()
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
