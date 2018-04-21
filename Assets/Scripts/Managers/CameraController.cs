using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        GameController.OnPlayerMovementTurn += ZoomIn;
        GameController.OnMinigolfTurn += ZoomOut;

        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }

    private void ZoomIn()
    {

    }

    private void ZoomOut()
    {

    }

    [SerializeField]
    private GameObject player;

    private Vector3 offset;
}