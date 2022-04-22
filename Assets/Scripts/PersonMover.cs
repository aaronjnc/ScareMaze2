using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonMover : MonoBehaviour
{
    public NavMeshAgent agent;
    private float agentDefaultSpeed;
    public float agentStuckSpeed;
    private float objectiveRange = 0.5f;

    public PersonSight sight;

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

    //Animation
    private Animator animator;

    //Scared
    public bool isScared;
    private bool stopped = false;

    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private AudioClip[] scareClips;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        person = gameObject.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agentDefaultSpeed = agent.speed;
        rb = GetComponentInChildren<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        int num = Random.Range(0, scareClips.Length);
        audioSource.clip = scareClips[num];
    }

    private void Update()
    {
        if(sight.getSighted() && !stopped)
        {
            stopped = true;
            StartCoroutine(PauseMotion());
        }
        else
        {
            animator.SetFloat("movementSpeed", agent.speed);
            rb.position = gameObject.transform.position;
            if (comparePosition(person.position, objectivePosition) && finalLocation)
            {
                Debug.Log("Final Location");
                agent.speed = agentDefaultSpeed;
                beenTo.Add(objective);
                objectiveSet = false;
                objective = null;
            }
            else if (comparePosition(person.position, objectivePosition) && objective.getPickedUp() && pickedUpObjective == null)
            {
                Debug.Log("Objective Picked up By Someone Else");
                objectiveSet = false;
            }
            else if (objectiveSet)
            {
                GoToObjective();
                if (agent.velocity == Vector3.zero)
                {
                    StartCoroutine(StopStuck());
                }
            }
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
        bool betweenMaxAndMinX = firstPosition.x <= secondPosition.x + objectiveRange && firstPosition.x >= secondPosition.x - objectiveRange;
        bool betweenMaxAndMinZ = firstPosition.z <= secondPosition.z + objectiveRange && firstPosition.z >= secondPosition.z - objectiveRange;
        return (betweenMaxAndMinX && betweenMaxAndMinZ);
    }

    public void Scare()
    {
        this.isScared = true;
        if(pickedUpObjective != null)
        {
            pickedUpObjective.SetActive(true);
            pickedUpObjective.GetComponent<PersonObjective>().Drop(this.gameObject.transform.position);
        }
        agent.speed = 0;
        StartCoroutine("DeathWait");
    }

    IEnumerator StopStuck()
    {
        Debug.Log("Stop being stuck");
        agent.speed = 1.5f;
        yield return new WaitForSeconds(0.5f);
        agent.speed = agentDefaultSpeed;
    }

    IEnumerator PauseMotion()
    {
        Debug.Log("Saw ghost");
        agent.speed = 0;
        yield return new WaitForSeconds(1f);
        agent.speed = agentDefaultSpeed;
        stopped = false;
    }

    IEnumerator DeathWait()
    {
        audioSource.Play();
        yield return new WaitForSeconds(.25f);
        levelManager.KillPerson();
        Destroy(this.gameObject);
    }
}
