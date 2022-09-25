using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed = 10.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _charController = null;
    private bool _characterIsMoving = false;
    [SerializeField] private float _rotationSpeed = 1f;

    [Header("Gravity Variables")]
    [SerializeField] private float _defaultYVelocity = -1.0f;


    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        if (_characterIsMoving)
        {
            HandleRotation();
        }        
    }

    private void Movement()
    {
        _moveDirection = new Vector3(-Input.GetAxisRaw("Horizontal"),0,-Input.GetAxisRaw("Vertical"));
        _moveDirection.Normalize();

        if (_moveDirection.magnitude > 0)
        {
            _characterIsMoving = true;
        }
        else
        {
            _characterIsMoving = false;
        }
        _moveDirection.y += _defaultYVelocity;
        _charController.Move(_moveDirection * _walkingSpeed * Time.deltaTime);       
    }

    private void HandleRotation()
    {
        Vector3 _positionToLookAt;
        _positionToLookAt.x = _moveDirection.x;
        _positionToLookAt.y = 0.0f;
        _positionToLookAt.z = _moveDirection.z;
        Quaternion _currentRotation = transform.rotation;
        Quaternion _targetRotation = Quaternion.LookRotation(_positionToLookAt);
        transform.rotation = Quaternion.Slerp(_currentRotation, _targetRotation, _rotationSpeed);             
    }
}
