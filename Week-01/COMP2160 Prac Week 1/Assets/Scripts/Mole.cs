using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Mole : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float stateTimer;
    private string currentState;

    [SerializeField] private Color downColor;
    [SerializeField] private Color upColor;
    [SerializeField] private Color missedColor;
    [SerializeField] private float minStateTimerVal;
    [SerializeField] private float maxStateTimerVal;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        SetState("Down");
    }

    void OnMouseDown()
    {
        if (currentState == "Up")
        {
            SetState("Down");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == "Up")
        {
            if (stateTimer > 0)
            {
                stateTimer -= Time.deltaTime;
            }
            else
            {
                SetState("Missed");
            }
        }
        else if (currentState == "Missed")
        {
            if (stateTimer > 0)
            {
                stateTimer -= Time.deltaTime;
            }
            else
            {
                SetState("Down");
            }
        }
        else if (currentState == "Down")
        {
            if (stateTimer > 0)
            {
                stateTimer -= Time.deltaTime;
            }
            else
            {
                SetState("Up");
            }
        }
    }

    private void SetState(string newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case "Down":
                sprite.color = downColor;
                stateTimer = Random.Range(minStateTimerVal, maxStateTimerVal);
                break;
            case "Up":
                sprite.color = upColor;
                stateTimer = Random.Range(minStateTimerVal, maxStateTimerVal);
                break;
            case "Missed":
                sprite.color = missedColor;
                stateTimer = Random.Range(minStateTimerVal, maxStateTimerVal);
                break;
        }
    }
}