using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;

    private Animator _animator;
    private Rigidbody _rb;

    private float horizontalInput;
    private float verticalInput;
    private float MoveSpeed = 5f;
    private float RotationSpeed = 10f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        horizontalInput = _joystick.Horizontal;
        verticalInput = _joystick.Vertical;
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (!Mathf.Approximately(horizontalInput,0f) || !Mathf.Approximately(verticalInput, 0f))
        {
            _animator.SetFloat("Speed_f", 1f);
            SetMove(moveDirection);
            SetRotation(moveDirection);   
        }
        else
            _animator.SetFloat("Speed_f", 0f);

    }

    private void SetRotation(Vector3 moveDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, Time.deltaTime * RotationSpeed));
    }

    private void SetMove(Vector3 moveDirection)
    {
        _rb.velocity = moveDirection * MoveSpeed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _animator.SetInteger("WeaponType_int", 12);
            _animator.SetInteger("MeleeType_int", 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _animator.SetInteger("WeaponType_int", 0);
            _animator.SetInteger("MeleeType_int", 0);
        }
    }
}
