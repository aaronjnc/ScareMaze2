using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTrigger : MonoBehaviour
{
    public LevelManager levelManager;

    public PersonObjective keyObjective;
    public PersonObjective lockObjective;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Person" && other.gameObject.GetComponent<PersonMover>().getPickedUpObjective() == keyObjective.gameObject)
        {
            Debug.Log("Unlock door");
            levelManager.unlockDoor();
        }
    }
}
