﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        GameController.OnPlayerMovementTurn += ZoomIn;
        GameController.OnMinigolfTurn += ZoomOut;

        camera = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");

        camera.orthographicSize = normalZoom;

        offset = transform.position - player.transform.position;
        zoomOffset = normalZoom - minZoom;
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    private void ZoomIn()
    {
        StopAllCoroutines();
        if (camera.orthographicSize != minZoom)
        {
            StartCoroutine(ZoomInCamera(zoomOffset));
        }
    }

    private void ZoomOut()
    {
        StopAllCoroutines();
        if (camera.orthographicSize != normalZoom)
        {
            StartCoroutine(ZoomOutCamera(zoomOffset));
        }
    }

    private IEnumerator ZoomInCamera(float zoom)
    {
        float time = 0.0f;
        float cameraZoom = camera.orthographicSize;

        while (time < timeTransition)
        {
            camera.orthographicSize = cameraZoom - zoom * zoomCurve.Evaluate(time / timeTransition);
            time += Time.deltaTime;
            yield return null;
        }
        camera.orthographicSize = minZoom;
    }

    private IEnumerator ZoomOutCamera(float zoom)
    {
        float time = 0.0f;
        float cameraZoom = camera.orthographicSize;

        while (time < timeTransition)
        {
            camera.orthographicSize = cameraZoom + zoom * zoomCurve.Evaluate(time / timeTransition);
            time += Time.deltaTime;
            yield return null;
        }
        camera.orthographicSize = normalZoom;
    }

    [SerializeField]
    private AnimationCurve zoomCurve;
    [SerializeField]
    private float minZoom;
    [SerializeField]
    private float normalZoom;
    [SerializeField]
    private float timeTransition;

    private GameObject player;

    private Vector3 offset;

    private Camera camera;

    private float zoomOffset;
}