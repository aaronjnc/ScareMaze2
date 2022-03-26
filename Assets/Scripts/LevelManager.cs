using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<PersonObjective> personObjectives;
    public List<PersonMover> persons;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        foreach(PersonMover person in persons)
        {
            int randomIndex = rnd.Next(personObjectives.Count);
            Debug.Log(randomIndex);
            person.setObjective(personObjectives[randomIndex], true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
