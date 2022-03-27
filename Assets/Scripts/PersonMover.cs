using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonMover : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform person;

    public LayerMask groundMask, personMask;

    public Rigidbody rb;

    //Objective
    public Vector3 objectivePosition;
    private bool objectiveSet;
    public PersonObjective objective;
    private bool objectiveGot;


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
        if(person.position == objectivePosition)
        {
            objectiveSet = false;
        }
        if(objectiveSet && objective.getPickedUp() && !objectiveGot)
        {
            //
        }
        if(objectiveSet)
        {
            GoToObjective();
        }
    }

    public void setObjective(PersonObjective objective, bool startPosition)
    {
        this.objective = objective;
        if (startPosition)
        {
            this.objectivePosition = objective.getStartPosition();
        }
        else
        {
            this.objectivePosition = objective.getFinishPosition();
        }
        objectiveSet = true;
    }

    public void setGroundMask(LayerMask groundMask)
    {
        this.groundMask = groundMask;
    }

    private void GoToObjective()
    {
        agent.SetDestination(objectivePosition);
    }

    public bool getObjectiveGot()
    {
        return this.objectiveGot;
    }
}
