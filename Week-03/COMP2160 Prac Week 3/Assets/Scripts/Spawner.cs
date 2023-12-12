using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Obstacle obstacle;
    [SerializeField] private float timeDelay;
    private float timer;
    [SerializeField] private float minStateTimerVal;
    [SerializeField] private float maxStateTimerVal;

    // Start is called before the first frame update
    void Start()
    {
        // timer = timeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        // Obstacle newObstacle = Instantiate(obstacle);
        // timer = timeDelay;

        Obstacle newObstacle = Instantiate(obstacle, transform.position, Quaternion.identity);
        timer = Random.Range(minStateTimerVal, maxStateTimerVal);
    }
}
