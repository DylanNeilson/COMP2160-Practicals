using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    static private GameManager instance;
    static public GameManager Instance 
    {
        get 
        {
            if (instance == null)
            {
                Debug.LogError("There is no GameManager instance in the scene.");
            }
            return instance;
        }
    }

    private int score = 0;
    public int Score 
    {
        get
        {
            return score;
        }
    }
    [SerializeField] private int scorePerMissile = 10;
    [SerializeField] private int scorePerRadar = 50;
    [SerializeField] private int scorePerPower = 500;

    [SerializeField] private int numLives = 3;
    private int livesRemaining;
    public int LivesRemaining
    {
        get
        {
            return livesRemaining;
        }
    }

    [SerializeField] private Transform startingPosition;
    private Transform lastCheckpoint;

    private Player player;
    private Backdrop backdrop;

    void Awake()
    {
        if (instance != null) 
        {
            // destroy duplicates
            Destroy(gameObject);            
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }        
    }

    public void Start()
    {
        score = 0;
        livesRemaining = numLives;
        lastCheckpoint = startingPosition;

        // find the player and backdrop
        player = FindObjectOfType<Player>();
        backdrop = FindObjectOfType<Backdrop>();
    }


    public void ScoreMissile()
    {
        score += scorePerMissile;
    }

    public void ScoreRadar()
    {
        score += scorePerRadar;
    }

    public void ScorePower()
    {
        score += scorePerPower;
        GameOver(true); // WIN
    }

    public void Die()
    {
        livesRemaining--;
        if (livesRemaining == 0)
        {
            GameOver(false);    // LOSE
        }
        else 
        {
            backdrop.RewindTo(lastCheckpoint.position.x);
            player.transform.position = lastCheckpoint.position;
            player.Revive();
        }
    }

    public void Checkpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    public void GameOver(bool win)
    {
        backdrop.Speed = 0;
        UIManager.Instance.ShowGameOver(win);
    }

}
