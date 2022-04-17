using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Harvester : Possessable
{
    [SerializeField]
    private Vector2 moveDir;
    [SerializeField]
    private Transform spawnPos;
    private bool moving = false;
    private Rigidbody rb;
    [SerializeField]
    private float velocity;
    private void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        controls.Movement.Movement.performed += Move;
        controls.Movement.Movement.canceled += Cancel;
        Camera.main.transform.position = new Vector3(transform.position.x, 25, transform.position.z);
    }

    private void Move(CallbackContext ctx)
    {
        if (ctx.ReadValue<Vector2>() == moveDir)
        {
            moving = true;
        }
        else
        {
            moving = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void Cancel(CallbackContext ctx)
    {
        rb.velocity = Vector3.zero;
        moving = false;
    }

    public override void Possess()
    {
        possessed = true;
        controls.Movement.Movement.Enable();
    }

    protected override void Scare(InputAction.CallbackContext ctx)
    {
        possessed = false;
        moving = false;
        rb.velocity = Vector3.zero;
        controls.Movement.Movement.Disable();
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rb.velocity = velocity * new Vector3(moveDir.x, 0, moveDir.y);
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        }
    }
}
