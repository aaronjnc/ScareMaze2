using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonObjective : MonoBehaviour
{
    public Vector3 objectStartPosition;
    public bool secondaryObjective;
    public GameObject secondPosition;
    public Vector3 objectFinishPosition;

    public LayerMask personMask;

    //person who has gotten the objective
    public PersonMover owner;
    public bool pickedUp = false;
    public float despawnTime;
    public bool dropped;

    private void Awake()
    {
        objectStartPosition = gameObject.transform.position;
        if(secondaryObjective)
        {
            objectFinishPosition = secondPosition.transform.position;
        }
    }

    private void Update()
    {
        if(dropped)
        {
            gameObject.transform.position = objectStartPosition;
            dropped = false;
        }
    }

    public Vector3 getStartPosition()
    {
        return this.objectStartPosition;
    }

    public Vector3 getFinishPosition()
    {
        return this.objectFinishPosition;
    }

    public bool getPickedUp()
    {
        return this.pickedUp;
    }

    public bool getSecondaryObjective()
    {
        return secondaryObjective;
    }

    public void setPickedUp(GameObject pickedUpBy)
    { 
        pickedUp = true;
        owner = pickedUpBy.GetComponent<PersonMover>();
        if(secondaryObjective)
        {
            Debug.Log("Assigning new position");
            owner.setObjective(this, false, true);
        }
        owner.setPickedUpObjective(gameObject);
    }

    public void setEscaped(GameObject escapee, bool escaped)
    {
        owner = escapee.GetComponent<PersonMover>();
        owner.setEscaped(escaped);
    }
}
