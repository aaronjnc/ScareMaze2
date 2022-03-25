using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Possessable : MonoBehaviour
{
    public enum PossessableType
    {
        Pumpkin,
        Scarecrow,
        Speaker,
    }
    [Tooltip("Type of possessable")]
    public PossessableType possessableType;
    private bool possessed = false;
    private PlayerControls controls;
    [SerializeField] private float possessCooldown;
    void Start()
    {
        controls = new PlayerControls();
        controls.Possession.Possess.performed += Scare;
        controls.Possession.Possess.Enable();
    }
    public void Scare(CallbackContext ctx)
    {
        if (possessed)
        {
            GameObject ghost = GhostMovement.Instance.gameObject;
            ghost.SetActive(true);
            ghost.transform.forward = transform.forward;
            possessed = false;
            StartCoroutine(PossessCooldown(ghost));
        }
    }
    public void Possess()
    {
        possessed = true;
    }

    private void OnDestroy()
    {
        if (controls != null)
            controls.Disable();
    }

    IEnumerator PossessCooldown(GameObject ghost)
    {
        yield return new WaitForSeconds(possessCooldown);
        ghost.GetComponent<GhostPossession>().enabled = true;
    }
}
