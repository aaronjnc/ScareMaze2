using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public abstract class Possessable : MonoBehaviour
{
    protected bool possessed = false;
    protected PlayerControls controls;
    [SerializeField] protected float possessCooldown;
    protected void Start()
    {
        controls = new PlayerControls();
        controls.Possession.Possess.performed += Scare;
        controls.Possession.Possess.Enable();
    }
    protected abstract void Scare(CallbackContext ctx);

    public abstract void Possess();

    protected void OnDestroy()
    {
        if (controls != null)
            controls.Disable();
    }

    protected IEnumerator PossessCooldown(GameObject ghost)
    {
        yield return new WaitForSeconds(possessCooldown);
        ghost.GetComponent<GhostPossession>().enabled = true;
    }
}
