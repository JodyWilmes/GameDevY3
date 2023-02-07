using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Camera Movement
    [SerializeField] private Transform _eyes;
    [SerializeField] private float _sensitivity;
    [Range(-90.0f, 0.0f)]
    [SerializeField] private float _camLimitMin;
    [Range(0.0f, 90.0f)]
    [SerializeField] private float _camLimitMax;
    private float _camAngle = 0.0f;

    // Movement
    [Range(1.0f, 15.0f)]
    [SerializeField] private float _speed;
    private Rigidbody _rb;

    // Jump
    [SerializeField] private float _jumpForce;
    [SerializeField] private KeyCode _jumpKey;

    // Interact
    [SerializeField] private float _interactRange;
    [SerializeField] private KeyCode _interactKey;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotateEyes();
        RotateBody();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        if (Input.GetKeyDown(_interactKey))
        {
            TryInteract();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void RotateEyes()
    {
        float yMouse = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        _camAngle -= yMouse;
        _camAngle = Mathf.Clamp(_camAngle, _camLimitMin, _camLimitMax);
        _eyes.localRotation = Quaternion.Euler(_camAngle, 0, 0);
    }

    private void RotateBody()
    {
        float xMouse = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xMouse);
    }

    private void Movement()
    {
        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * xDir + transform.forward * zDir;

        _rb.velocity = new Vector3(0, _rb.velocity.y, 0) + dir.normalized * _speed;
    }

    private void TryJump()
    {
        if (IsGrounded()){
            Jump(_jumpForce);
        }
    }

    private void Jump(float jumpforce)
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
    }

    private void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(_eyes.position, _eyes.forward, out hit, _interactRange))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return (Physics.Raycast(transform.position, -transform.up, out hit, 1.1f));
        //return Physics.SphereCast(transform.position, 1, -transform.up, out hit);
    }
}