using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField]
    private float velocityMagnitudeWin = 0.75f;

    private Rigidbody ballRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && ballRigidbody == null)
        {
            ballRigidbody = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (ballRigidbody.velocity.magnitude < velocityMagnitudeWin)
            {
                if (!_enterInHole)
                {
                    _enterInHole = true;
                    GameController.Instance.PlayerWin();
                }
                ballRigidbody.useGravity = false;
                ballRigidbody.transform.position = Vector3.Lerp(ballRigidbody.transform.position, _ballEndTransform.position, 5f * Time.deltaTime);
            }
        }
    }

    [SerializeField]
    private Transform _ballEndTransform;

    private bool _enterInHole = false;
}
