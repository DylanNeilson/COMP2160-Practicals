using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    // Singleton
    static private Layers instance;
    static public Layers Instance 
    {
        get 
        {
            if (instance == null)
            {
                Debug.LogError("There is no Layers instance in the scene.");
            }
            return instance;
        }
    }

    [SerializeField] private LayerMask player;
    public LayerMask Player
    {
        get
        {
            return player;
        }
    }
    [SerializeField] private LayerMask terrain;
    public LayerMask Terrain
    {
        get
        {
            return terrain;
        }
    }
    [SerializeField] private LayerMask missile;
    public LayerMask Missile
    {
        get
        {
            return missile;
        }
    }
    [SerializeField] private LayerMask radar;
    public LayerMask Radar
    {
        get
        {
            return radar;
        }
    }
    [SerializeField] private LayerMask bullet;
    public LayerMask Bullet
    {
        get
        {
            return bullet;
        }
    }
    [SerializeField] private LayerMask bomb;
    public LayerMask Bomb
    {
        get
        {
            return bomb;
        }
    }
    [SerializeField] private LayerMask checkpoint;
    public LayerMask Checkpoint
    {
        get
        {
            return checkpoint;
        }
    }
    
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
}
