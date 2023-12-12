using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour
{
    private float timer;
    [SerializeField] private CreepMove creepMove;
    [SerializeField] private float spawnTime;
    [SerializeField] private Path path;

    // Start is called before the first frame update
    void Start()
    {

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
        CreepMove newCreepMove = Instantiate(creepMove, transform.position, Quaternion.identity);
        newCreepMove.Path = path;
        timer = spawnTime;
    }
}
