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
        }
    }
    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 100, player))
        {
            animator.SetBool("ghostSpotted", true);
            sighted = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("ghostSpotted", false);
        sighted = false;
    }
}
