using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float gravity = 10;
    [SerializeField] private Vector3 velocity;

    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }

    public Vector3 Velocity
    {
        get 
        {
            return velocity;
        }
        set 
        {
            velocity = value;
        }
    }
}
