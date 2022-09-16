using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public int acceleration = 5;
    public int ReturnSpeed = 5;
    public bool DEBUG;

    [Header("Item Holding")]
    public LayerMask GrabbingMask;
    public Transform HoldPos;
    public GameObject HeldGO;
    public bool Holding;

    [Header("Dumping")]
    public Transform DumpPos;

    // Start is called before the first frame update
    void Start()
    {
        Holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        //transform.Translate(new Vector3(hori, 0, vert) * acceleration * Time.deltaTime);

        if (!Holding)
        {
            transform.Translate(new Vector3(hori, 0, vert) * acceleration * Time.deltaTime);

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
            Vector3 mvt = DumpPos.position - transform.position;

            if (mvt.magnitude >= 0.05f)
            {
                transform.Translate(mvt.normalized * ReturnSpeed * Time.deltaTime);
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

        Holding = true;
    }

    private void DropObject()
    {
        HeldGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        HeldGO.transform.SetParent(null);

        HeldGO = null;
        Holding = false;
    }

    private void OnDrawGizmos()
    {
        if (DEBUG)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 20, Color.red, 0.1f);
        }
    }
}
