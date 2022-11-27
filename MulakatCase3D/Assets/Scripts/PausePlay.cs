using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePlay : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject pauseButton;

    public void Pause()
    {
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        Time.timeScale = 0;


    }
    public void Play()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }
}
