using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonObjective : MonoBehaviour
{
    public Vector3 objectStartPosition;
    public bool secondaryObjective;
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

    public void setPickedUp(GameObject pickedUpBy)
    { 
        Debug.Log("Person");
        pickedUp = true;
        owner = pickedUpBy.GetComponent<PersonMover>();
        Debug.Log(owner);
        if(secondaryObjective)
        {
            owner.setObjective(this, false);
        }
        
    }
}
