using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Speeds")]
    public float acceleration = 5;
    public float deccelerationRate = 0.5f;
    public float ReturnSpeed = 5;

    [Header("Item Holding")]
    public LayerMask GrabbingMask;
    public Transform HoldPos;
    public GameObject HeldGO;
    [SerializeField] private bool _holding;

    [Header("Dumping")]
    public Transform DumpPos;

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
        //transform.Translate(new Vector3(hori, 0, vert) * acceleration * Time.deltaTime);

        if (!_holding)
        {
            float hori = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");

            _rb.AddForce(new Vector3(hori, 0, vert) * acceleration * Time.deltaTime, ForceMode.Impulse);
            //transform.Translate(new Vector3(hori, 0, vert) * acceleration * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, 25, GrabbingMask);
                if (hits.Length > 0)
                {
                    TakeObject(hits[0].collider.gameObject);
                }
            }
        }
        else
        {
            Vector3 returnMvt = DumpPos.position - transform.position;

            if (returnMvt.magnitude >= 0.1f)
            {
                if(returnMvt.magnitude >= 1)
                {
                    returnMvt.Normalize();
                }
                _rb.AddForce(returnMvt * ReturnSpeed * Time.deltaTime, ForceMode.Impulse);
                //transform.Translate(mvt.normalized * ReturnSpeed * Time.deltaTime);
            }
            else
            {
                DropObject();
            }
        }

    }

    private void TakeObject(GameObject GameObj)
    {
        HeldGO = GameObj;

        HeldGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        HeldGO.transform.SetParent(HoldPos, true);
        HeldGO.transform.localPosition = Vector3.zero;

        _rb.velocity = Vector3.zero;
        _holding = true;
    }

    private void DropObject()
    {
        HeldGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        HeldGO.transform.SetParent(null);

        _rb.velocity = Vector3.zero;
        HeldGO = null;
        _holding = false;
    }

    private void OnDrawGizmos()
    {
        if (DEBUG)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 20, Color.red, 0.1f);
        }
    }
}
