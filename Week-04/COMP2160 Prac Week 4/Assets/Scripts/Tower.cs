using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float attackStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        // Debug.Log("Trigger entered by: " + other.gameObject.name);
        CreepHealth creep = other.GetComponent<CreepHealth>();
        creep?.TakeDamage(attackStrength * Time.deltaTime);

        // Debug.Log("Creep took damage. Remaining health: " + creep.health);
    }
}
