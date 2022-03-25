using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GhostPossession : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private float possessRadius;
    [SerializeField] private LayerMask possessableLayer;
    [SerializeField] private Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Possession.Possess.performed += Possess;
        controls.Possession.Possess.Enable();
    }

    private void Possess(CallbackContext ctx)
    {
        if (!gameObject.scene.IsValid())
            return;
        Collider[] col = Physics.OverlapSphere(transform.position, possessRadius, possessableLayer);
        float distance = float.MaxValue;
        GameObject possess = null;
        for (int i = 0; i < col.Length; i++)
        {
            Vector3 pos = col[i].transform.position;
            Vector3 dir = pos - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, possessRadius, possessableLayer))
            {
                if (hit.collider == col[i])
                {
                    float dis = Vector3.Distance(transform.position, pos);
                    if (dis < distance)
                    {
                        distance = dis;
                        possess = col[i].gameObject;
                    }
                }
            }
        }
        if (possess != null)
        {
            gameObject.SetActive(false);
            mainCamera.position = new Vector3(possess.transform.position.x, mainCamera.position.y, possess.transform.position.z);
            possess.GetComponent<Possessable>().Possess();
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (controls != null)
            controls.Disable();
    }

    private void OnEnable()
    {
        if (controls != null)
            controls.Enable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, possessRadius);
    }
}
