using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerCam;
    [SerializeField] private float throwForce;
    private Rigidbody rb;

    public Transform destination;

    private bool inRange;
    private bool beingCarried;
    private bool touchWall;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.position);

        if (distance <= 3f) inRange = true;
        else inRange = false;

        if (inRange && Input.GetMouseButton(0)) //pick object
        {
            rb.useGravity = false;
            rb.freezeRotation = true;
            transform.position = destination.position;
            transform.parent = destination.transform;
            beingCarried = true;
        }

        if (beingCarried)
        {
            if (touchWall) //if object collides with wall, drop it
            {
                rb.useGravity = true;
                rb.freezeRotation = false;
                transform.parent = null;
                beingCarried = false;
                touchWall = false;
            }

            if (Input.GetMouseButton(1)) //Throw object
            {
                rb.useGravity = true;
                rb.freezeRotation = false;
                transform.parent = null;
                beingCarried = false;
                rb.AddForce(playerCam.forward * throwForce, ForceMode.Impulse);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (beingCarried) touchWall = true;
    }
}
