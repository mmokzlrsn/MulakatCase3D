using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour , IScore
{
    [SerializeField] private AudioClip scoreSound;
    public void SetText(int score)
    {
        GetComponent<TextMeshProUGUI>().text = $"Score: {score}";
        GameManager.Instance.PlayScoreSound(scoreSound);
    }

    
}
