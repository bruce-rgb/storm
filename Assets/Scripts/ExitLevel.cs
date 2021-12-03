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
            //stop gameplay music
            fade.SetActive(true);
            canvas.SetActive(false);
            canvasGameOver.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
