using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public PersonObjective parent;
    public LayerMask personLayer;
    public bool finalObjective;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Person" && other.gameObject.GetComponent<PersonMover>().getObjective() == GetComponent<PersonObjective>())
        {
            if(!finalObjective)
            {
                parent.setPickedUp(other.gameObject);
            }
            else
            {
                parent.setEscaped(other.gameObject, finalObjective);
            }
        }
    }
}
