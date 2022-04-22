using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Speaker : Possessable
{
    [SerializeField]
    private LayerMask npcLayer;
    public override void Possess()
    {
        possessed = true;
        scarable = false;
        StartCoroutine("ScareCooldown");
    }

    protected override void Scare(InputAction.CallbackContext ctx)
    {
        if (scarable)
        {
            possessed = false;
            controls.Movement.Movement.Disable();
            Camera.main.transform.position = new Vector3(transform.position.x, 15, transform.position.z);
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            ghost.transform.position = new Vector3(spawnPos.position.x, ghost.transform.position.y, spawnPos.position.z);
            ghost.SetActive(true);
            StartCoroutine(Scare(.05f));
            StartCoroutine(PossessCooldown(ghost));
        }
    }
    IEnumerator Scare(float time)
    {
        yield return new WaitForSeconds(time);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, npcLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            PersonSight sight = colliders[i].gameObject.GetComponentInChildren<PersonSight>();
            if (!sight.sighted)
            {
                PersonMover person = sight.gameObject.GetComponentInParent<PersonMover>();
                person.Scare();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
