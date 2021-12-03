using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreGive = 100;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioController.obj.PlayCoin();
            Game.obj.AddScore(scoreGive);
            gameObject.SetActive(false);
        }
    }
}
