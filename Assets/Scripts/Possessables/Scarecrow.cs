using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Scarecrow : Possessable
{
    private GhostMovement movement;
    void Start()
    {
        base.Start();
        movement = GetComponent<GhostMovement>();
    }
    public override void Possess()
    {
        possessed = true;
        movement.enabled = true;
    }

    protected override void Scare(CallbackContext ctx)
    {
        if (possessed)
        {
            movement.enabled = false;
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            Vector3 newPos = transform.position + transform.forward * 2;
            ghost.transform.position = new Vector3(newPos.x, ghost.transform.position.y, newPos.z);
            ghost.SetActive(true);
            possessed = false;
            StartCoroutine(PossessCooldown(ghost));
        }
    }
}
