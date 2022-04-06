using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JackOLantern : Possessable
{
    void Start()
    {
        base.Start();
    }

    public override void Possess()
    {
        possessed = true;
    }

    protected override void Scare(InputAction.CallbackContext ctx)
    {
        if (possessed)
        {
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            Vector3 newPos = transform.position + transform.forward * 2;
            ghost.transform.position = new Vector3(newPos.x, ghost.transform.position.y, newPos.z);
            ghost.SetActive(true);
            possessed = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 15);
            StartCoroutine(PossessCooldown(ghost));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 15);
    }
}
