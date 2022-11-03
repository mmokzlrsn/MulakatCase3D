using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour, IScore
{

    [SerializeField] private AudioClip highScoreSound;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"High Score: {PlayerPrefs.GetInt("HighScore")}";
    }

    public void SetText(int score)
    {

        if (GameManager.Instance.CheckHighScore())
        {
            GetComponent<TextMeshProUGUI>().text = $"High Score: {PlayerPrefs.GetInt("HighScore")}";
            GameManager.Instance.PlayScoreSound(highScoreSound);
        }
    }


}
