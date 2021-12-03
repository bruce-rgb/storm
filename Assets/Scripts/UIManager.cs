using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager obj;

    public Text livesLbl;
    public Text scoreLbl;
    public bool gamePause;
    public Transform uiPanel;

    private void Awake()
    {
        obj = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gamePause = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLives()
    {
        if (Player.obj.lives == 0)
        {
            livesLbl.text = "0";
        }
        livesLbl.text = "" + Player.obj.lives;
    }

    public void UpdateScore()
    {
        scoreLbl.text = "" + Game.obj.score;
    }

    public void StartGame()
    {
        AudioController.obj.PlayMainMenu();
        gamePause = true;
        uiPanel.gameObject.SetActive(true);
    }

    //Play button
    public void HideInitPanel()
    {
        AudioController.obj.PlayClickButton();
        gamePause = false;
        uiPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
