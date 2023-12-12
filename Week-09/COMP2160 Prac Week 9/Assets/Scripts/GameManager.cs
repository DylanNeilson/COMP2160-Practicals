using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputAction firstMove;
    private InputAction secondMove;
    private InputAction thirdMove;
    private InputAction fourthMove;

    [SerializeField] private int danceCommands;
    private int currentCommand;
    public int CurrentCommand
    {
        get
        {
            return currentCommand;
        }
    }

    [SerializeField] private float danceSpeed;
    private float speed;
    private float timer;

    private int currentScore;
    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
    }
    private int streak;
    public int Streak
    {
        get
        {
            return streak;
        } 
    }

    public delegate void doHit();
    public doHit DoHit;

    void Awake()
    {
        playerActions = new PlayerActions();
        firstMove = playerActions.dancing.FirstMove;
        secondMove = playerActions.dancing.SecondMove;
        thirdMove = playerActions.dancing.ThirdMove;
        fourthMove = playerActions.dancing.FourthMove;
    }

    void OnEnable()
    {
        firstMove.Enable();
        secondMove.Enable();
        thirdMove.Enable();
        fourthMove.Enable();        
    }

    void OnDisable()
    {
        firstMove.Disable();
        secondMove.Disable();
        thirdMove.Disable();
        fourthMove.Disable();
    }

    void Start()
    {
        firstMove.performed += FirstMoveCheck;
        secondMove.performed += SecondMoveCheck;
        thirdMove.performed += ThirdMoveCheck;
        fourthMove.performed += FourthMoveCheck;

        speed = danceSpeed;
        timer = speed;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Miss();
        }
    }

    public void Hit()
    {
        currentScore++;
        streak++;
        DoHit?.Invoke();
        NewMove();
    }

    public delegate void doMiss();
    public doMiss DoMiss;
    public void Miss()
    {
        streak = 0;
        DoMiss?.Invoke();
        NewMove();
    }

    public delegate void doNewMove();
    public doNewMove DoNewMove;
    void NewMove()
    {
        int lastCommand = currentCommand;
        while (currentCommand == lastCommand)
        {
            currentCommand = Random.Range(0,danceCommands);
        }
        DoNewMove?.Invoke();
        timer = speed;
    }

        void FirstMoveCheck(InputAction.CallbackContext context)
    {
        CheckMove(0);
    }

    void SecondMoveCheck(InputAction.CallbackContext context)
    {
        CheckMove(1);
    }

    void ThirdMoveCheck(InputAction.CallbackContext context)
    {
        CheckMove(2);
    }

    void FourthMoveCheck(InputAction.CallbackContext context)
    {
        CheckMove(3);
    }

    void CheckMove(int move)
    {
        if (currentCommand == move)
        {
            Hit();
        }
        else
        {
            Miss();
        }
    }
}
