using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonObjective : MonoBehaviour
{
    public Vector3 objectStartPosition;
    public bool isKey;
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
        if(isKey)
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

    public bool getIsKey()
    {
        return isKey;
    }

    public void setPickedUp(bool pickedUp, GameObject pickedUpBy = null)
    {
        this.pickedUp = pickedUp;
        if (isKey)
        {
            owner = pickedUpBy.GetComponent<PersonMover>();
            Debug.Log("Assigning new position");
            owner.setObjective(this, false, true);
            owner.setPickedUpObjective(gameObject);
            this.gameObject.SetActive(false);
        }
    }

    public void setEscaped(GameObject escapee, bool escaped)
    {
        owner = escapee.GetComponent<PersonMover>();
        owner.setEscaped(escaped);
    }

    public void Drop(Vector3 where)
    {
        this.pickedUp = false;
        this.gameObject.transform.position = where;
        StartCoroutine(DropTimer());
    }

    IEnumerator DropTimer()
    {
        yield return new WaitForSeconds(despawnTime);

        if(!pickedUp)
        {
            this.gameObject.transform.position = objectStartPosition;
        }
    }
}
