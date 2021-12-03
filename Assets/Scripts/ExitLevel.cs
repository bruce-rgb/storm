using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public int scene;

    public GameObject fade;
    public GameObject canvas;

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

    public void ChangeLevel()
    {
        SceneManager.LoadScene(scene);
    }
}
