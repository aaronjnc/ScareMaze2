using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] 
    private float health;
    private GhostMovement movement;
    [HideInInspector]
    public bool sighted = false;
    [SerializeField]
    private float healthDropRate;
    [SerializeField]
    private Slider healthSlider;
    private bool invincible = false;
    private void Start()
    {
        _instance = this;
        movement = GetComponent<GhostMovement>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public GhostMovement GetMovement()
    {
        return movement;
    }
    private void FixedUpdate()
    {
        if (sighted && !invincible)
        {
            health -= healthDropRate*Time.deltaTime;
            healthSlider.value = health;
            if (health < 0)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().Lose();
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine("Invincible");
    }

    IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(1f);
        invincible = false;
    }
}
