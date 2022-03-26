using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInfo : MonoBehaviour
{
    private static GhostInfo _instance;
    public static GhostInfo Instance
    {
        get
        {
            return _instance;
        }
    }
    [SerializeField] private float health;
    private GhostMovement movement;
    private void Start()
    {
        _instance = this;
        movement = GetComponent<GhostMovement>();
    }
    public GhostMovement GetMovement()
    {
        return movement;
    }
}
