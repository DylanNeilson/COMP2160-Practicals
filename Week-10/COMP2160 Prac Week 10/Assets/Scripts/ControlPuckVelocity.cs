using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class ControlPuckVelocity : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputAction movePuck;

    private Rigidbody rigidbody;

    private float moveSpeed = 10f;

    void Awake()
    {
        playerActions = new PlayerActions();
        movePuck = playerActions.PuckControl.MovePuck;
    }

    void OnEnable()
    {
        movePuck.Enable();
    }

    void OnDisable()
    {
        movePuck.Disable();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
    }

    void FixedUpdate()
    {
        // ERROR: You should not move a non-kinematic Rigidbody using Transform
        // Vector3 position = mousePosition();
        // transform.position = position;

        // Move the paddle using rigidbody.velocity
        Vector3 targetPosition = mousePosition();
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        rigidbody.velocity = moveDirection * moveSpeed;
    }

    private Vector3 mousePosition()
    {
        // use raycasting to turn mouse position into position on the board
        Plane plane = new Plane(Vector3.up, transform.position);
        Vector2 mouse = movePuck.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        float contact;
        plane.Raycast(ray, out contact);
        return ray.GetPoint(contact);
    }
}
