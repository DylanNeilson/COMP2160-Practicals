using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Rect flightRect;

    [SerializeField] private Bullet bulletPrefab;
    private Transform gun;
    [SerializeField] private float gunCooldown = 0.5f; // seconds
    private float gunTimer = 0;

    [SerializeField] private Bomb bombPrefab;
    private Transform drop;
    [SerializeField] private float bombCooldown = 1.0f; // seconds
    private float bombTimer = 0;

    [SerializeField] private Explosion explosionPrefab;

    [SerializeField] private float dyingPause = 2; // seconds
    private bool dying = false;
    private float dyingTimer = 0;

    [SerializeField] private SpriteRenderer sprite;

    private Backdrop backdrop;

    private Vector3 velocity;

    private PlayerActions playerActions;
    private InputAction movementAction;
    private InputAction fireAction;
    private InputAction bombAction;


    void Awake()
    {
        playerActions = new PlayerActions();
        movementAction = playerActions.Plane.Movement;
        fireAction = playerActions.Plane.Fire;
        bombAction = playerActions.Plane.Bomb;

    }

    void OnEnable()
    {
        movementAction.Enable();
        fireAction.Enable();
        bombAction.Enable();
    }

    void OnDisable()
    {
        movementAction.Disable();
        fireAction.Disable();
        bombAction.Disable();
    }
    void Start()
    {
        // get the camera rect
        // note this assumes the window is aligned with the world x/y axes
        Camera camera = Camera.main;
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));

        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        // shrink the box by the size of the sprite
        flightRect.xMin = bottomLeft.x + sprite.bounds.extents.x / 2;
        flightRect.xMax = topRight.x - sprite.bounds.extents.x / 2;
        flightRect.yMin = bottomLeft.y + sprite.bounds.extents.y / 2;
        flightRect.yMax = topRight.y -  + sprite.bounds.extents.y / 2;

        // Get the gun and bomb points
        gun = transform.Find("Gun");
        drop = transform.Find("Drop");

        // Get the backdrop
        backdrop = FindObjectOfType<Backdrop>();
    }

    void Update()
    {
        // if we're dying, wait for the timer then reset
        if (dying)
        {
            dyingTimer -= Time.deltaTime;
            if (dyingTimer <= 0)
            {
                GameManager.Instance.Die();
            }
            return;
        }        

        float vx = movementAction.ReadValue<Vector2>().x;
        float vy = movementAction.ReadValue<Vector2>().y;

        velocity = new Vector3(vx, vy, 0) * speed;
        transform.Translate(velocity * Time.deltaTime);

        // clamp to stay on screen
        transform.localPosition = flightRect.Clamp(transform.localPosition);

        FireGun();
        DropBomb(velocity.x);
    }

    private void FireGun() 
    {
        gunTimer -= Time.deltaTime;
        float fireButton = fireAction.ReadValue<float>();
        if (fireButton > 0 && gunTimer <= 0)
        {
            gunTimer += gunCooldown;
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = gun.position;
        }        
    }

    private void DropBomb(float vx) 
    {
        bombTimer -= Time.deltaTime;
        float bombButton = bombAction.ReadValue<float>();
        if (bombButton > 0 && bombTimer <= 0)
        {
            bombTimer += bombCooldown;
            Bomb bomb = Instantiate(bombPrefab);
            bomb.transform.position = drop.position;

            // add the base speed
            bomb.Velocity = Vector3.right * backdrop.Speed;
        }        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;

        // die if colliding with terrain, missles or radars
        if (!Layers.Instance.Checkpoint.Contains(other))
        {
            Die();
        }
    }

    private void Die()
    {
        // hide the ship
        sprite.enabled = false;

        // create an explosion and wait for it to complete before telling the GameManager
        Explosion explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        dying = true;
        dyingTimer = dyingPause;
    }

    public void Revive()
    {
        sprite.enabled = true;
        dying = false;
    }

}
