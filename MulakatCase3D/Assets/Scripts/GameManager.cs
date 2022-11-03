using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
     
    [Header("Audio")]
    [SerializeField] private UnityEvent<AudioClip> ScoreSound;

    [Header("Score Information")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private int score;
    [SerializeField] private UnityEvent<int> ScoreChanged;

    void Awake()
    {


        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        score++;
        ScoreChanged.Invoke(score);
        
    }

    public void PlayScoreSound(AudioClip clip)
    {
        Debug.Log("Audio is played");
        ScoreSound.Invoke(clip);
    }

     

    public bool CheckHighScore()
    {
        if(score >= PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
             
            return true;
        }
        return false;

    }
}
