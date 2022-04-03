using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSight : MonoBehaviour
{
    public bool sighted = false;
    [Tooltip("Player layermask"), SerializeField]
    private LayerMask player;
    private void OnTriggerEnter(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 100, player))
        {
            sighted = true;
        }
    }
}