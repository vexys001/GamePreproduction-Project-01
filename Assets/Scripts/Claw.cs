using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    private Rigidbody _rb;
    public GameObject ClawObject;
    public Transform CameraTransform;

    [Header("Camera")]
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonCamera;
    private bool isFPCameraActive = false;



    [Header("Speeds")]
    public float Acceleration = 5;
    public float DeccelerationRate = 0.5f;

    [Header("Item Holding")]
    public LayerMask GrabbingMask;
    [SerializeField] private float _grabbingRadius = 0.5f;
    [SerializeField] private float _grabbingLength = 7.5f;
    public Transform HoldPos;
    [SerializeField] private GameObject _heldGO;
    [SerializeField] private bool _holding;

    [Header("DEBUG")]
    public bool DEBUG;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_holding)
            {
                RaycastHit[] hits = Physics.SphereCastAll(ClawObject.transform.position, _grabbingRadius, -ClawObject.transform.up, _grabbingLength, GrabbingMask);

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
        if (Input.GetKeyDown(KeyCode.Q))
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
}
