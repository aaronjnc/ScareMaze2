using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<PersonObjective> personObjectives;
    public List<PersonMover> persons;

    public PersonObjective finalObjective;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        foreach(PersonMover person in persons)
        {
            int randomIndex = rnd.Next(personObjectives.Count);
            PersonObjective objective = personObjectives[randomIndex];
            Debug.Log(randomIndex);
            person.setObjective(objective, true, !objective.getSecondaryObjective());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (persons.Count == 0)
        {
            Debug.Log("Lose");
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
                Destroy(person.gameObject);
            }

            foreach (PersonMover person in persons)
            {
                if (person.getBeenTo().Count != personObjectives.Count && !person.getObjectiveSet())
                {
                    Debug.Log("Reassigning Task");
                    System.Random rnd = new System.Random();
                    PersonObjective objective;
                    do
                    {
                        int randomIndex = rnd.Next(personObjectives.Count);
                        objective = personObjectives[randomIndex];
                    } while (person.getBeenTo().Contains(objective));
                    person.setObjective(objective, true, !objective.getSecondaryObjective());
                }
                else if (person.getBeenTo().Count == personObjectives.Count && !person.getObjectiveSet())
                {
                    person.setObjective(finalObjective, true, true);
                }
                else if (person.getEscaped())
                {
                    persons.Remove(person);
                    Destroy(person.gameObject);
                }
            }
        }
    }
}
