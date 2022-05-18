using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSight : MonoBehaviour
{
    public bool sighted = false;
    [Tooltip("Player layermask"), SerializeField]
    private LayerMask player;

    public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 100, player))
        {
            animator.SetBool("ghostSpotted", true);
            sighted = true;
            GhostInfo.Instance.sighted = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 100, player))
        {
            animator.SetBool("ghostSpotted", true);
            sighted = true;
            GhostInfo.Instance.sighted = true;
        }
        else
        {
            animator.SetBool("ghostSpotted", false);
            sighted = false;
            GhostInfo.Instance.sighted = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("ghostSpotted", false);
        sighted = false;
        GhostInfo.Instance.sighted = false;
    }

    public bool getSighted()
    {
        return this.sighted;
    }
}
