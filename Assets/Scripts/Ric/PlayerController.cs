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
    [SerializeField] private Transform _raycastStartPosition;
    [SerializeField] private float _rayDistance = 0.5f;
    [SerializeField] private LayerMask _interactableLayer;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _charController = null;
    private bool _characterIsMoving = false;
    private bool _characterIsUsingCrane = false;
    [SerializeField] private Claw _claw = null;

    private Interactable _interactable;

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
        Ray _forwardRay = new Ray(_raycastStartPosition.position,transform.forward);
        
        bool hasHit = Physics.Raycast(_forwardRay, out hit, _rayDistance, _interactableLayer);

        if (hasHit)
        {
            _interactable = hit.collider.gameObject.GetComponent<Interactable>();
            _interactable.ShowUI();
        }
        else
        {
            _interactable?.HideUI();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (hasHit)
            {
                if(hit.collider.tag == "CuttingBoard")
                {
                    CuttingBoard cuttingBoard = hit.collider.gameObject.GetComponent<CuttingBoard>();
                    if (cuttingBoard != null)
                    {
                        cuttingBoard.Use();
                    }
                }
                else if (_claw != null)
                {
                    if (hit.collider.tag == "CraneConsole")
                    {
                        _characterIsUsingCrane = !_characterIsUsingCrane;
                        if (_characterIsUsingCrane)
                        {
                            _claw.Activate();
                            _walkingSpeed = 0;
                            _rotationSpeed = 0;
                            _claw.enabled = true;
                        }
                        else 
                        {
                            _claw.Deactivate();
                            _claw.enabled = false;
                            _claw.firstPersonCamera.SetActive(false);
                            _claw.thirdPersonCamera.SetActive(true);
                            _walkingSpeed = _defaultWalkingSpeed;
                            _rotationSpeed = _defaultRotationSpeed;
                        }
                    }
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
