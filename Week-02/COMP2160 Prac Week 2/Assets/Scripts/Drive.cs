using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drive : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float turningRate;
    private Vector3 direction = new Vector3(0,0,1);

    private PlayerActions actions;
    private InputAction movementAction;
    
    private InputAction turboAction;

    private float boost = 1f;
    [SerializeField] private float boostRate;

    void Awake()
    {
        actions = new PlayerActions();
        movementAction = actions.driving.movement;
        
        turboAction = actions.turbo.turbo;
        turboAction.performed += OnTurbo;
        turboAction.canceled += EndTurbo;
    }

    void OnEnable()
    {
        movementAction.Enable();
        turboAction.Enable();
    }

    void OnDisable()
    {
        movementAction.Disable();
        turboAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float acceleration = movementAction.ReadValue<Vector2>().y;

        if (acceleration > 0)
        {
            float steering = movementAction.ReadValue<Vector2>().x;
            transform.Rotate(Vector3.up * turningRate * steering * Time.deltaTime, Space.Self);
        }

        transform.Translate(Vector3.forward * speed * acceleration * boost * Time.deltaTime, Space.Self);
    }

    void OnTurbo(InputAction.CallbackContext context)
    {
        boost = boostRate;
    }

    void EndTurbo(InputAction.CallbackContext context)
    {
        boost = 1f;
    }
}
