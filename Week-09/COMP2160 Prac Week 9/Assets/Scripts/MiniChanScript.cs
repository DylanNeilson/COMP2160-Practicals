using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniChanScript : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        gm = GameObject.FindObjectOfType<GameManager>();
        gm.DoHit += IncreaseStreak;
        gm.DoMiss += ResetStreak;
    }

    void IncreaseStreak()
    {
        animator.SetInteger("Streak",gm.Streak);
    }

    void ResetStreak()
    {
        animator.SetInteger("Streak",gm.Streak);
    }
}
