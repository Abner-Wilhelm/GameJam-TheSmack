using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform orientation;
    public float drag;

    float horizontalInput;
    float verticalInput;

    public static PlayerMovement Instance;

    public bool isFrozen = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
}
    }

    Vector3 moveDir;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        if(isFrozen)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        MyInput();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        if(isFrozen)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Force);
    }

    internal void TeleportToPoint(Transform teleportTransform)
    {
       transform.position = teleportTransform.position;
    }
}
