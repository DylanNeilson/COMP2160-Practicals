using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    private PlayerActions actions;
    private InputAction jumpAction;

    private enum JumpState
    {
        Jumping,
        OnGround,
    }
    private JumpState jumpState;

    private float speed;
    private float verticalVelocity;

    [Range(0, 10)][SerializeField] private float jumpSpeed;
    [Range(-10, -5)][SerializeField] private float gravity;
    void Awake()
    {
        actions = new PlayerActions();
        jumpAction = actions.movement.jump;
    }
    void OnEnable()
    {
        jumpAction.Enable();
    }
    void OnDisable()
    {
        jumpAction.Disable();
    }
    void Start()
    {
        jumpState = JumpState.OnGround;
    }

    void Update()
    {
        switch (jumpState)
        {
            case JumpState.OnGround:
                speed = 0;
                if (jumpAction.ReadValue<float>() > 0)
                {
                    jumpState = JumpState.Jumping;
                    speed = jumpSpeed;
                }
                break;
            case JumpState.Jumping:
                speed += gravity * Time.deltaTime;
                transform.Translate(Vector3.up * speed * Time.deltaTime);

                if (transform.position.y <= 0)
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    jumpState = JumpState.OnGround;
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }

}
