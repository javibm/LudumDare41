using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnim : MonoBehaviour
{

    public float maxRotationX;
    public float maxRotationY;
    public float maxRotationZ;
    public float speed;
    public float maxDelay = 0.5f;

    private float delay;
    private Vector3 initRotation;
    private Vector3 newRot;
    private float sinSpeed;

    void Start()
    {

        initRotation = transform.rotation.eulerAngles;
        delay = Random.Range(0.0f, maxDelay);
    }

    void Update()
    {

        sinSpeed = Mathf.Sin((Time.time + delay) * speed);
        newRot = new Vector3(sinSpeed * maxRotationX, sinSpeed * maxRotationY, sinSpeed * maxRotationZ);
        transform.rotation = Quaternion.Euler(initRotation.x + newRot.x, initRotation.y + newRot.y, initRotation.z + newRot.z);
    }
}