using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float _walkingSpeed = 10.0f;
    [SerializeField] private float _rotationSpeed = 1f;
    private float _defaultWalkingSpeed = 0f;
    private float _defaultRotationSpeed = 0f;    

    [Header("Other Variables")]
    [SerializeField] private float _defaultYVelocity = -1.0f;
    [SerializeField] private float _rayDistance = 0.5f;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _charController = null;
    private bool _characterIsMoving = false;
    private bool _characterIsUsingCrane = false;
    [SerializeField] private Claw _claw = null;



    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _defaultWalkingSpeed = _walkingSpeed;
        _defaultRotationSpeed = _rotationSpeed;
    }

    private void LateUpdate()
    {
        Movement();
        if (_characterIsMoving)
        {
            HandleRotation();
        }
        RaycastHit hit;
        Ray _forwardRay = new Ray(transform.position,transform.forward);
        Debug.DrawRay(transform.position,transform.forward* _rayDistance);
        if (Physics.Raycast(_forwardRay, out hit, _rayDistance) && hit.collider.tag == "CraneConsole")
        {
            Debug.Log("You are in range to the CraneControl");
            if(Input.GetKeyDown(KeyCode.E) && _claw != null)
            {
                _characterIsUsingCrane = !_characterIsUsingCrane;
                if (!_characterIsUsingCrane)
                {
                    _walkingSpeed = 0;
                    _rotationSpeed = 0;
                    _claw.enabled = true;
                }
                else 
                {
                    _claw.enabled = false;
                    _walkingSpeed = _defaultWalkingSpeed;
                    _rotationSpeed = _defaultRotationSpeed;
                }
            }
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
