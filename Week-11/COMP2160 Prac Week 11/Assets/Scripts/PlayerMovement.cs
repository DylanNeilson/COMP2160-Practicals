using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    //Input
    private PlayerActions playerActions;
    private InputAction walkInput;
    private InputAction jumpInput;

    //Physics
    private Rigidbody rigidbody;

    //Movement
    private bool jump;
    private Vector3 walkDirection;
    [Range(0,10)] [SerializeField] float speed;
    [Range(0,10)] [SerializeField] float jumpForce;

    void Awake()
    {
        playerActions = new PlayerActions();
        walkInput = playerActions.Movement.Walk;
        jumpInput = playerActions.Movement.Jump;
    }

    void OnEnable()
    {
        walkInput.Enable();
        jumpInput.Enable();
    }

    void OnDisable()
    {
        walkInput.Disable();
        jumpInput.Disable();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); //Assign the rigidbody
    }

    void Update()
    {
        jump = jump || jumpInput.WasPressedThisFrame(); //"jump ||" to counter non-synchronisation between Update and FixedUpdate
        walkDirection = new Vector3(walkInput.ReadValue<Vector2>().x,0,walkInput.ReadValue<Vector2>().y);
    }

    void FixedUpdate()
    {
        //Move the player
        rigidbody.MovePosition(transform.position + walkDirection * speed * Time.fixedDeltaTime);

        //Make them jump
        if (jump)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }


    }
}
