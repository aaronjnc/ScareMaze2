using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JackOLantern : Possessable
{
    [SerializeField]
    private LayerMask npcLayer;
    void Start()
    {
        base.Start();
    }

    public override void Possess()
    {
        possessed = true;
        scarable = false;
        StartCoroutine("ScareCooldown");
    }

    protected override void Scare(InputAction.CallbackContext ctx)
    {
        if (possessed && scarable)
        {
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            ghost.transform.position = new Vector3(spawnPos.position.x, ghost.transform.position.y, spawnPos.position.z);
            ghost.SetActive(true);
            possessed = false;
            scarable = false;
            StartCoroutine(Scare(.05f));
            StartCoroutine(PossessCooldown(ghost));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 15);
    }
    IEnumerator Scare(float time)
    {
        yield return new WaitForSeconds(time);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15, npcLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            PersonSight sight = colliders[i].gameObject.GetComponentInChildren<PersonSight>();
            if (sight.sighted)
            {
                PersonMover person = sight.gameObject.GetComponentInParent<PersonMover>();
                person.Scare();
            }
        }
    }
}
