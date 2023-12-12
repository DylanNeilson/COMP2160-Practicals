using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI streakText;
    [SerializeField] private TextMeshProUGUI hitCheck;

    private string scoreTextString = "Score: {0}";
    private string streakTextString = "Streak: {0}";
    private string hitText = "Hit!";
    private string missText = "Miss!";

    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        gm.DoHit += Hit;
        gm.DoMiss += Miss;
        UpdateScore();
    }

    void Hit()
    {
        hitCheck.text = hitText;
        UpdateScore();
    }

    void Miss()
    {
        hitCheck.text = missText;
        UpdateScore();
    }

    void UpdateScore()
    {
        int updatedScore = gm.CurrentScore;
        scoreText.text = string.Format(scoreTextString, updatedScore);
        int updatedStreak = gm.Streak;
        streakText.text = string.Format(streakTextString, updatedStreak);
    }
}
