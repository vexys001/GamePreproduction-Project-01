using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject ClawObject;
    public Transform CameraTransform;
    private bool _isActive = false;
    private List<Interactable> _interactables = new List<Interactable>();

    [Header("Camera")]
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    private bool isFPCameraActive = false;

    [Header("Speeds")]
    public float Acceleration = 5;
    public float DeccelerationRate = 0.5f;
    public float ReturnSpeed = 5;

    [Header("Item Holding")]
    public LayerMask GrabbingMask;
    [SerializeField] private float _grabbingRadius = 0.5f;
    [SerializeField] private float _grabbingLength = 7.5f;
    public Transform HoldPos;
    [SerializeField] GameObject _heldGO;
    [SerializeField] private bool _holding;

    [Header("DEBUG")]
    public bool DEBUG;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        RaycastHit[] hits;
        if (_isActive)
        {
            hits = Physics.SphereCastAll(ClawObject.transform.position, _grabbingRadius, -ClawObject.transform.up, _grabbingLength, GrabbingMask);

            ResetInteractable();
            
            if (hits.Length > 0 && !_holding && _isActive)
            {
                foreach (RaycastHit hit in hits)
                {
                    Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.ShowUI();
                        _interactables.Add(interactable);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!_holding)
                {
                    if (hits.Length > 0)
                    {
                        TakeObject(hits[0].collider.gameObject);
                    }
                }
                else
                {
                    DropObject();
                }
            }
        }

        
        SwapCamera();
    }

    private void Movement()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Vector3 input = Quaternion.Euler(0, CameraTransform.rotation.eulerAngles.y, 0) * new Vector3(hori, 0, vert);

        if (input.magnitude != 0)
        {
            _rb.AddForce(input * Acceleration * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            _rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, DeccelerationRate * Time.deltaTime);
        }
    }

    public GameObject GetHeldObject()
    {
        return _heldGO;
    }

    private void TakeObject(GameObject GameObj)
    {
        _heldGO = GameObj;
       _heldGO.GetComponent<Ingredient>().StopMoving();

        _heldGO.transform.SetParent(HoldPos, true);
        _heldGO.transform.localPosition = Vector3.zero;

        _rb.velocity = Vector3.zero;
        _holding = true;
    }

    private void DropObject()
    {
        _heldGO.GetComponent<Ingredient>().Fall();

        _heldGO.transform.SetParent(null);

        _rb.velocity = Vector3.zero;
        _heldGO.GetComponent<Ingredient>().Fall();
        _heldGO = null;

        ResetInteractable();

        _holding = false;
    }

    private void OnDrawGizmos()
    {
        if (DEBUG)
        {
            Debug.DrawLine(ClawObject.transform.position, ClawObject.transform.position - ClawObject.transform.up * 20, Color.red, 0.1f);
        }
    }

    private void SwapCamera()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(firstPersonCamera == null)
            {
                return;
            }
            isFPCameraActive = !isFPCameraActive;
            CheckWhichCamisActive();            
        }
    }
    

    private void CheckWhichCamisActive()
    {
        if (thirdPersonCamera.activeSelf)
        {
            thirdPersonCamera.SetActive(false);
            firstPersonCamera.SetActive(true);
        }
        else
        {
            firstPersonCamera.SetActive(false);
            thirdPersonCamera.SetActive(true);
        }
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
        ResetInteractable();
    }

    public void ResetInteractable()
    {
        foreach (Interactable interactable in _interactables)
        {
            if (interactable != null)
            {
                interactable.HideUI();
            }
        }
        _interactables.Clear();
    }
}
