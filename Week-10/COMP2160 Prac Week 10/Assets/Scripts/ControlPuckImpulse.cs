using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class ControlPuckImpulse : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputAction movePuck;
    private InputAction mouseClick;
    private bool isMousePressed = false;

    private Rigidbody rigidbody;

    private float impulseForce = 5f;

    void Awake()
    {
        playerActions = new PlayerActions();
        movePuck = playerActions.PuckControl.MovePuck;
        mouseClick = playerActions.PuckControl.MouseClick;
    }

    void OnEnable()
    {
        movePuck.Enable();
        mouseClick.Enable();
    }

    void OnDisable()
    {
        movePuck.Disable();
        mouseClick.Disable();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
    }

    void Update()
    {
        // ERROR: You should not move a non-kinematic Rigidbody using Transform
        // Vector3 position = mousePosition();
        // transform.position = position;

        // Check for mouse click
        if (mouseClick.triggered)
        {
            isMousePressed = true;
        }
    }

    void FixedUpdate()
    {
        // When mouse is clicked, add impulse to move paddle towards mouse pos
        if (isMousePressed)
        {
            Vector3 targetPosition = mousePosition();
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            Vector3 impulse = moveDirection * impulseForce;
            rigidbody.AddForce(impulse, ForceMode.Impulse);
            isMousePressed = false;
        }
    }

    private Vector3 mousePosition()
    {
        // Use raycasting to turn the mouse position into a position on the board
        Plane plane = new Plane(Vector3.up, transform.position);
        Vector2 mouse = movePuck.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        float contact;
        plane.Raycast(ray, out contact);
        return ray.GetPoint(contact);
    }
}