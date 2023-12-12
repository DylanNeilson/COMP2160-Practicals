using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dance : MonoBehaviour
{
    private Animator animator;
    private PlayerActions actions;
    private InputAction firstMove;
    private InputAction secondMove;
    private InputAction thirdMove;
    private InputAction fourthMove;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (firstMove != null)
        {
            firstMove.performed += DoFirstMove;
        }
        if (secondMove != null)
        {
            secondMove.performed += DoSecondMove;
        }
        if (thirdMove != null)
        {
            thirdMove.performed += DoThirdMove;
        }
        if (fourthMove != null)
        {
            fourthMove.performed += DoFourthMove;
        }
    }

    void Awake()
    {
        actions = new PlayerActions();
        firstMove = actions.dancing.FirstMove;
        secondMove = actions.dancing.SecondMove;
        thirdMove = actions.dancing.ThirdMove;
        fourthMove = actions.dancing.FourthMove;

        if (firstMove != null)
        {
            firstMove.performed += DoFirstMove;
        }
        if (secondMove != null)
        {
            secondMove.performed += DoSecondMove;
        }
        if (thirdMove != null)
        {
            thirdMove.performed += DoThirdMove;
        }
        if (fourthMove != null)
        {
            fourthMove.performed += DoFourthMove;
        }
    }

    private void OnEnable()
    {
        firstMove.Enable();
        secondMove.Enable();
        thirdMove.Enable();
        fourthMove.Enable();
    }

    private void OnDisable()
    {
        firstMove.Disable();
        secondMove.Disable();
        thirdMove.Disable();
        fourthMove.Disable();

        firstMove.performed -= DoFirstMove;
        secondMove.performed -= DoSecondMove;
        thirdMove.performed -= DoThirdMove;
        fourthMove.performed -= DoFourthMove;
    }

    void DoFirstMove(InputAction.CallbackContext context)
    {
        animator.SetInteger("Dance Move", 1);
    }

    void DoSecondMove(InputAction.CallbackContext context)
    {
        animator.SetInteger("Dance Move", 2);
    }

    void DoThirdMove(InputAction.CallbackContext context)
    {
        animator.SetInteger("Dance Move", 3);
    }

    void DoFourthMove(InputAction.CallbackContext context)
    {
        animator.SetInteger("Dance Move", 4);
    }
}