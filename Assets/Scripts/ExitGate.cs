using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private Transform gate;
    public float turnTime;
    private bool open = false;

    private void Awake()
    {
        gate = gameObject.transform;
    }

    private void Update()
    {
        if(open)
        {
            Quaternion target = Quaternion.Euler(0f, 30f, 0f);
            gate.rotation = Quaternion.RotateTowards(gate.rotation, target, turnTime * Time.deltaTime);
        }
    }

    public void OpenGate()
    {
        Debug.Log("Opening Gate");
        open = true;
    }

}
