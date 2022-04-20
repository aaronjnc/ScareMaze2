using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Harvester : Possessable
{
    [SerializeField]
    private Vector2 moveDir;
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
        scarable = false;
        controls.Movement.Movement.Enable();
        Camera.main.transform.position = new Vector3(transform.position.x, 25, transform.position.z);
        StartCoroutine("ScareCooldown");
    }

    protected override void Scare(InputAction.CallbackContext ctx)
    {
        if (scarable)
        {
            possessed = false;
            moving = false;
            rb.velocity = Vector3.zero;
            controls.Movement.Movement.Disable();
            Camera.main.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            ghost.transform.position = new Vector3(spawnPos.position.x, ghost.transform.position.y, spawnPos.position.z);
            ghost.SetActive(true);
            StartCoroutine(PossessCooldown(ghost));
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            rb.velocity = velocity * new Vector3(moveDir.x, 0, moveDir.y);
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Person"))
        {
            Destroy(collision.gameObject);
        }
    }
}
