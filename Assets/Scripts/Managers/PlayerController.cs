using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Awake()
    {
        shouldMove = false;
        Invoke("StartMovement", 1.7f);
    }
    void Start()
    {
        GameController.OnMinigolfTurn += StopMovement;
        GameController.OnPlayerMovementTurn += ResumeMovement;
        GameController.OnPlayerDead += StopMovement;
        GameController.OnPlayerWin += StopMovement;
    }

    void OnDestroy()
    {
        GameController.OnMinigolfTurn -= StopMovement;
        GameController.OnPlayerMovementTurn -= ResumeMovement;
        GameController.OnPlayerDead -= StopMovement;
        GameController.OnPlayerWin -= StopMovement;
    }

    void Update()
    {
        if (shouldMove)
        {
            if (Input.GetMouseButton(0))
            {
                GameController.Instance.PlayerRun();
                transform.position += Time.deltaTime * speed * transform.forward;
            }
            else
            {
                GameController.Instance.PlayerStop();
            }
        }

        if (shouldMove && transform.position.y < -1)
        {
            GameController.Instance.PlayerDead();
        }
    }

    void FixedUpdate()
    {
        if (shouldMove)
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);

                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
            }

        }
    }

    public void StopMovement()
    {
        shouldMove = false;
    }

    public void ResumeMovement()
    {
        shouldMove = true;
    }

    public void StartMovement()
    {
        shouldMove = true;
        GameController.Instance.BallReady(ballTransform);
    }

    private bool shouldMove;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float speedRotation;

    [SerializeField]
    private Transform ballTransform;
}
