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
    [SerializeField]
    private float respawnTime;
    private Vector3 startPos;
    private Vector3 startRot;
    private void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.eulerAngles;
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
        if (scarable && possessed)
        {
            possessed = false;
            moving = false;
            scarable = false;
            rb.velocity = Vector3.zero;
            controls.Movement.Movement.Disable();
            Camera.main.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            ghost.transform.position = new Vector3(spawnPos.position.x, ghost.transform.position.y, spawnPos.position.z);
            ghost.SetActive(true);
            StartCoroutine(PossessCooldown(ghost));
            StartCoroutine("Respawn");
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
            collision.gameObject.GetComponent<PersonMover>().Scare();
        }
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = startPos;
        transform.eulerAngles = startRot;
    }
}
