using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<PersonObjective> personObjectives;
    public List<PersonMover> persons;

    public bool isTutorial;

    public bool doorUnlocked;
    public PersonObjective key;
    public PersonObjective door;
    public ExitGate exitGate;

    private int totalPeople;
    private int numberEscaped = 0;

    public PersonObjective finalObjective;

    System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        totalPeople = persons.Count;
        foreach(PersonMover person in persons)
        {
            if(!isTutorial)
            {
                PersonObjective objective = getRandomObjective();
                person.setObjective(objective, true, !objective.getIsKey());
            }
            else
            {
                person.setObjective(key, true, !key.getIsKey());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numberEscaped == totalPeople)
        {
            Lose();
        }
        else
        {
            List<PersonMover> remove = new List<PersonMover>();
            foreach(PersonMover person in persons)
            {
                if(person.getEscaped())
                {
                    remove.Add(person);
                }
            }

            foreach (PersonMover person in remove)
            {
                persons.Remove(person);
                numberEscaped++;
                Destroy(person.gameObject);
            }

            foreach (PersonMover person in persons)
            {
                if(!person.getEscaped())
                {
                    if ((doorUnlocked && person.getBeenTo().Contains(door)))
                    {
                        person.setObjective(finalObjective, true, true);
                    }
                    else if(person.getPickedUpObjective() == key.gameObject && person.getBeenTo().Contains(door))
                    {
                        Debug.Log("Go to the door");
                        person.setObjective(door, true, false);
                    }
                    else if(person.getObjective() == key && key.getPickedUp() && person.getPickedUpObjective() == null)
                    {
                        Debug.Log("Can't get key");
                        PersonObjective objective;
                        do
                        {
                            objective = getRandomObjective();
                        } while (person.getBeenTo().Contains(objective));
                        person.setObjective(objective, true, !objective.getIsKey());
                    }
                    else if (person.getBeenTo().Count != personObjectives.Count - 1 && !person.getObjectiveSet())
                    {
                        Debug.Log("Reassigning Task");
                        PersonObjective objective;
                        do
                        {
                            objective = getRandomObjective();
                        } while (person.getBeenTo().Contains(objective) | (person.getPickedUpObjective() == key.gameObject && objective == key));
                        person.setObjective(objective, true, !objective.getIsKey());
                    }
                }
                else
                {
                    persons.Remove(person);
                    Destroy(person.gameObject);
                }
            }
        }
    }

    private PersonObjective getRandomObjective()
    {
        int randomIndex = rnd.Next(personObjectives.Count);
        return personObjectives[randomIndex];
    }

    public void unlockDoor()
    {
        this.doorUnlocked = true;
        exitGate.OpenGate();
    }

    private void Lose()
    {
        Debug.Log("Lose");
    }
}
