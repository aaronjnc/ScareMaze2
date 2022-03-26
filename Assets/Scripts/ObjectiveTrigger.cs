using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public PersonObjective parent;
    public LayerMask personLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Person")
        {
            parent.setPickedUp(other.gameObject);
        }
    }
}
