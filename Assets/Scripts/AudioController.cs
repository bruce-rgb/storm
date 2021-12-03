using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController obj;

    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip mainMenuSound;
    public AudioClip addLiveSound;
    public AudioClip playerDamageSound;
    public AudioClip killEnemySound;
    public AudioClip gameOverhSound;
    public AudioClip coinSound;
    public AudioClip runnigSound;
    public AudioClip clickButtonSound;
    public AudioClip exitLevelSound;

    private AudioSource audioSource;
    private void Awake()
    {
        obj = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayExitLevel()
    {
        PlaySound(exitLevelSound);
    }

    public void PlayJump()
    {
        PlaySound(jumpSound);
    }

    public void PlayClickButton()
    {
        PlaySound(clickButtonSound);
    }

    public void PlayHit()
    {
        PlaySound(hitSound);
    }

    public void PlayAddLive()
    {
        PlaySound(addLiveSound);
    }

    public void PlayPlayerDamage()
    {
        PlaySound(playerDamageSound);
    }
    

    internal void PlayMainMenu()
    {
        PlaySound(mainMenuSound);
    }

    internal void PlayRunning()
    {
        PlaySound(runnigSound);
    }

    internal void PlayCoin()
    {
        PlaySound(coinSound);
    }

    internal void PlayGameOver()
    {
        PlaySound(gameOverhSound);
    }

    internal void PlayKillEnemy()
    {
        PlaySound(killEnemySound);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
