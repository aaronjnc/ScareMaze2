using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Scarecrow : Possessable
{
    private Animator animator;
    private GhostMovement movement;
    void Start()
    {
        base.Start();
        movement = GetComponent<GhostMovement>();
        animator = GetComponent<Animator>();
    }
    public override void Possess()
    {
        possessed = true;
        movement.enabled = true;
        scarable = false;
        animator.SetBool("isPossessed", true);
        StartCoroutine("ScareCooldown");
    }

    protected override void Scare(CallbackContext ctx)
    {
        if (scarable && possessed)
        {
            movement.enabled = false;
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            Vector3 newPos = transform.position + transform.forward * 2;
            ghost.transform.position = new Vector3(newPos.x, ghost.transform.position.y, newPos.z);
            ghost.SetActive(true);
            possessed = false;
            animator.SetBool("isPossessed", false);
            StartCoroutine(PossessCooldown(ghost));
        }
    }
}
