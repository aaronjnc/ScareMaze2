using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonMover : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform person;

    public Rigidbody rb;

    //Objective
    public Vector3 objectivePosition;
    public bool objectiveSet;
    public bool finalLocation;

    public Vector3 doorLocation;

    public PersonObjective objective;
    public GameObject pickedUpObjective;

    public List<PersonObjective> beenTo;

    //Escape
    public bool escaped;


    //Scared
    public bool isScared;

    private void Awake()
    {
        person = gameObject.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        rb.position = gameObject.transform.position;
        if(comparePosition(person.position, objectivePosition) && finalLocation)
        {
            Debug.Log("Final Location");
            beenTo.Add(objective);
            objectiveSet = false;
            objective = null;
        }
        else if(comparePosition(person.position, objectivePosition) && objective.getPickedUp() && pickedUpObjective == null)
        {
            Debug.Log("Objective Picked up By Someone Else");
            objectiveSet = false;
        }
        else if(objectiveSet)
        {
            GoToObjective();
        }
    }

    public void setEscaped(bool escaped)
    {
        this.escaped = escaped;
    }

    public void setObjective(PersonObjective objective, bool startPosition, bool finalLocation)
    {
        this.finalLocation = finalLocation;
        if (startPosition)
        {
            this.objectivePosition = objective.getStartPosition();
            this.objective = objective;
            objectiveSet = true;
        }
        else
        {
            this.doorLocation = objective.getFinishPosition();
            objectiveSet = false;
        }
    }

    private void GoToObjective()
    {
        agent.SetDestination(objectivePosition);
    }

    public bool getObjectiveSet()
    {
        return this.objectiveSet;
    }

    public PersonObjective getObjective()
    {
        return this.objective;
    }

    public void setPickedUpObjective(GameObject objective)
    {
        pickedUpObjective = objective;
    }

    public GameObject getPickedUpObjective()
    {
        return this.pickedUpObjective;
    }

    public List<PersonObjective> getBeenTo()
    {
        return beenTo;
    }

    public bool getEscaped()
    {
        return this.escaped;
    }

    private bool comparePosition(Vector3 firstPosition, Vector3 secondPosition)
    {
        return (firstPosition.x == secondPosition.x) && (firstPosition.z == secondPosition.z);
    }

    private void Scare()
    {
        this.isScared = true;
        if(pickedUpObjective != null)
        {
            pickedUpObjective.SetActive(true);
            pickedUpObjective.GetComponent<PersonObjective>().Drop(this.gameObject.transform.position);
        }
    }
}
