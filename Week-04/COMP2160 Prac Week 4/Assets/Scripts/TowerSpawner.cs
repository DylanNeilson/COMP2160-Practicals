using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSpawner : MonoBehaviour
{
    private PlayerActions actions;
    private InputAction spawnPositionAction;
    private InputAction mouseClick;


    private LayerMask groundLayer;
    Camera cam;
    Vector3 pos = new Vector3(200, 200, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }

    void Awake()
    {
        actions = new PlayerActions();
        spawnPositionAction = actions.TowerDefence.SpawnPosition;
        mouseClick = actions.TowerDefence.SpawnTower;
        
        Vector3 mousePosition = new Vector3(spawnPositionAction.ReadValue<Vector2>().x,spawnPositionAction.ReadValue<Vector2>().y, 0);
    } 

    void OnEnable()
    {
        spawnPositionAction.Enable();
        mouseClick.Enable();
    }

    void OnDisable()
    {
        spawnPositionAction.Disable();
        mouseClick.Disable();
    }

    private float DistanceToGround()
    {
        // cast a ray from our position, pointing down, reaching as far as possible
        Vector3 pos = transform.position;
        Vector3 dir = Vector3.down;
        float distance = float.PositiveInfinity;
        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, distance, groundLayer))
        {
            distance = hit.distance;
        }
            return distance;
    }
}
