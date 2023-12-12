using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turningRate;
    private Vector3 direction = new Vector3(0, 0, 1);
    private Vector3 targetLocalPos;

    [SerializeField] private Transform target;
    [SerializeField] private float minimumDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Check if car is greater than the minimum distance from target
        if (distance > minimumDistance)
        {
            // Move the car forward in its local coordinates
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }

        targetLocalPos = transform.InverseTransformPoint(target.position);

        // Check if target is to the left or right based on its x position
        if (targetLocalPos.x <= 0)
        {
            // Turn left
            transform.Rotate(Vector3.up * -turningRate * Time.deltaTime, Space.Self);
        }
        else
        {
            // Turn right
            transform.Rotate(Vector3.up * turningRate * Time.deltaTime, Space.Self);
        }
    }
}
 