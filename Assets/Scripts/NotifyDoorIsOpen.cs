using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyDoorIsOpen : MonoBehaviour
{
    private bool doorIsOpen = false;
    public PersonObjective finalObjective;
    public PersonObjective door;
    public PersonObjective key;

    public void setDoorIsOpen()
    {
        doorIsOpen = true;
    }

    public void setObjectives(PersonObjective final, PersonObjective door, PersonObjective key)
    {
        this.finalObjective = final;
        this.door = door;
        this.key = key;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Person")
        {
            PersonMover person = other.gameObject.GetComponent<PersonMover>();
            if (doorIsOpen | person.getObjective() == key)
            {
                if (!person.getBeenTo().Contains(door) && person.getObjective() != finalObjective)
                {
                    Debug.Log("Go to the door because you see the door");
                    person.setObjective(finalObjective, true, false);
                }
            }
        }
        
    }
}
