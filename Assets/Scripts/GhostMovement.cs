using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class GhostMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody player;
    [SerializeField] private Transform mainCamera;
    private Vector2 dir = Vector2.zero;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        player = GetComponent<Rigidbody>();
        controls.Movement.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        controls.Movement.Movement.canceled += ctx => dir = Vector2.zero;
        controls.Movement.Movement.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.velocity = new Vector3(dir.x * speed, 0, dir.y * speed);
        mainCamera.position = new Vector3(player.position.x, mainCamera.position.y, player.position.z);
        if (dir != Vector2.zero)
        {
            float rotation = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotation, 0), Time.deltaTime * rotateSpeed);
        }
    }
}
