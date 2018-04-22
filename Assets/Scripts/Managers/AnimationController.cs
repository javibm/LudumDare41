using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        GameController.OnPlayerMovementTurn += GoToRun;
        GameController.OnMinigolfTurn += GoToGolf;
        GameController.OnBallShot += GoToHit;
        GameController.OnPlayerRunning += GoToRun;
        GameController.OnPlayerStop += GoToIdle;
        GameController.OnPlayerDead += GoToRun;
        GameController.OnPlayerWin += GoToWin;
    }

    void GoToRun()
    {
        animator.SetBool(golfingBool, false);
        animator.SetBool(hitBallBool, false);
        animator.SetBool(runningBool, true);
        animator.SetFloat(randomIdleFloat, Random.Range(0.0f, 1.0f));
    }

    void GoToIdle()
    {
        animator.SetBool(runningBool, false);
    }

    void GoToGolf()
    {
        animator.SetBool(golfingBool, true);
    }

    void GoToHit()
    {
        animator.SetBool(hitBallBool, true);
    }

    void GoToWin()
    {
        animator.Play(goToWinState);
    }

    void OnDestroy()
    {
        GameController.OnPlayerMovementTurn -= GoToRun;
        GameController.OnMinigolfTurn -= GoToGolf;
        GameController.OnBallShot -= GoToHit;
        GameController.OnPlayerRunning -= GoToRun;
        GameController.OnPlayerStop -= GoToIdle;
        GameController.OnPlayerDead -= GoToRun;
        GameController.OnPlayerWin -= GoToWin;
    }


    private Animator animator;
    private int golfingBool = Animator.StringToHash("Golfing");
    private int hitBallBool = Animator.StringToHash("HitBall");
    private int runningBool = Animator.StringToHash("Running");
    private int randomIdleFloat = Animator.StringToHash("RandomIdle");
    private int goToWinState = Animator.StringToHash("Win");

}
