using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    // Variables to keep track of destroyed items
    private int missilesDestroyed = 0;
    private int radarDestroyed = 0;
    private int powerCellsDestroyed = 0;

    // Variables to keep track of total destroyed items
    private int totalMissilesDestroyed = 0;
    private int totalRadarDestroyed = 0;
    private int totalPowerCellsDestroyed = 0;

    [SerializeField] private int numLives = 3;
    [SerializeField] private float delayAfterDeath = 2;

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

    private LogFile log;

    private PlayerMove player;
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
        }
    }

    void Start()
    {
        score = 0;
        livesRemaining = numLives;
        lastCheckpoint = startingPosition;

        // find the player and backdrop
        player = FindObjectOfType<PlayerMove>();
        backdrop = FindObjectOfType<Backdrop>();

        // find the logfile script
        log = gameObject.GetComponent<LogFile>();
    }

    void Update()
    {
        if (log != null)
        {
            // write the time and the players x and y positions to the file
            log.WriteLine(Time.time,
                 player.transform.position.x,
                 player.transform.position.y);
        }
    }

    public void ScoreMissile()
    {
        score += scorePerMissile;
        missilesDestroyed++; // Increment count of destroyed missiles
    }

    public void ScoreRadar()
    {
        score += scorePerRadar;
        radarDestroyed++; // Increment count of destroyed radar items
    }

    public void ScorePower()
    {
        score += scorePerPower;
        powerCellsDestroyed++; // Increment count of destroyed power cells
        GameOver(true); // WIN
    }

    public void Die()
    {
        // Store the time since the start of the game
        float timeSinceStart = Time.time;

        // Store the position of the player's ship (X, Y)
        Vector2 playerPosition = player.transform.position;

        // Use LayerMask.LayerToName() to retrieve the layer's name.
        string causeOfDeath = LayerMask.LayerToName(player.gameObject.layer);

        // Print data
        Debug.Log("Player Death Time: " + timeSinceStart + " Position: " + playerPosition + " Cause: " + causeOfDeath);

        StartCoroutine(DieTimer());
    }

    private IEnumerator DieTimer()
    {
        livesRemaining--;
        yield return new WaitForSeconds(delayAfterDeath);

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
        // Store checkpoint name
        string checkpointName = checkpoint.name;

        // Store the time at the checkpoint
        float timeAtCheckpoint = Time.time;

        // Store player's score at the checkpoint
        int scoreAtCheckpoint = score;

        // Print data
        Debug.Log("Checkpoint Name: " + checkpointName + " Time: " + timeAtCheckpoint + " Score: " + scoreAtCheckpoint);

        // Log the number of missiles, radar, and power cells destroyed at the checkpoint
        Debug.Log("Missiles Destroyed at Checkpoint: " + missilesDestroyed);
        Debug.Log("Radar Items Destroyed at Checkpoint: " + radarDestroyed);
        Debug.Log("Power Cells Destroyed at Checkpoint: " + powerCellsDestroyed);

        // Update the last checkpoint
        lastCheckpoint = checkpoint;

        // Store the total number of missiles, radar, and power cells destroyed
        totalMissilesDestroyed += missilesDestroyed;
        totalRadarDestroyed += radarDestroyed;
        totalPowerCellsDestroyed += powerCellsDestroyed;

        // Reset the destroyed item counts for the checkpoint
        missilesDestroyed = 0;
        radarDestroyed = 0;
        powerCellsDestroyed = 0;
    }

    public void GameOver(bool win)
    {
        backdrop.Speed = 0;

        // Log the total number of missiles, radar, and power cells destroyed
        Debug.Log("Missiles Destroyed at End of Game: " + totalMissilesDestroyed);
        Debug.Log("Radar Items Destroyed at End of Game: " + totalRadarDestroyed);
        Debug.Log("Power Cells Destroyed at End of Game: " + totalPowerCellsDestroyed);

        UIManager.Instance.ShowGameOver(win);
    }

}