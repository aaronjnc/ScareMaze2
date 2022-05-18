using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Scarecrow : Possessable
{
    private Rigidbody rb;
    private Animator animator;
    private GhostMovement movement;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private LayerMask npcLayer;
    private Vector3 startPos;
    private Vector3 startRot;
    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<GhostMovement>();
        animator = GetComponent<Animator>();
        startPos = transform.position;
        startRot = transform.eulerAngles;
    }
    public override void Possess()
    {
        possessed = true;
        movement.enabled = true;
        scarable = false;
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        animator.SetBool("isPossessed", true);
        StartCoroutine("ScareCooldown");
    }

    protected override void Scare(CallbackContext ctx)
    {
        if (scarable && possessed)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            movement.enabled = false;
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            Vector3 newPos = transform.position + transform.forward * 2;
            ghost.transform.position = new Vector3(newPos.x, ghost.transform.position.y, newPos.z);
            ghost.SetActive(true);
            possessed = false;
            scarable = false;
            animator.SetBool("isPossessed", false);
            this.enabled = false;
            StartCoroutine(Scare(.05f));
            StartCoroutine(PossessCooldown(ghost));
            StartCoroutine("Respawn");
        }
    }
    IEnumerator Scare(float time)
    {
        yield return new WaitForSeconds(time);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15, npcLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            PersonSight sight = colliders[i].gameObject.GetComponentInChildren<PersonSight>();
            if (sight.sighted)
            {
                PersonMover person = sight.gameObject.GetComponentInParent<PersonMover>();
                person.Scare();
            }
        }
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        transform.position = startPos;
        transform.eulerAngles = startRot;
    }
}
