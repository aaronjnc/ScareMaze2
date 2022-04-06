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
    }

    protected override void Scare(InputAction.CallbackContext ctx)
    {
        if (possessed)
        {
            GameObject ghost = GhostInfo.Instance.gameObject;
            ghost.transform.forward = transform.forward;
            Vector3 newPos = transform.position + transform.forward * 2;
            ghost.transform.position = new Vector3(newPos.x, ghost.transform.position.y, newPos.z);
            ghost.SetActive(true);
            possessed = false;
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
                Debug.Log("AH");
            }
        }
    }
}
