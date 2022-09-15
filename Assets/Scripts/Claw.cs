using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public int speed = 5;
    public bool DEBUG;

    public LayerMask GrabbingMask;
    public Transform HoldPos;
    public GameObject HeldGO;
    public bool Holding;
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

        transform.Translate(new Vector3(hori, 0, vert) * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Holding)
            {
                RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, 25, GrabbingMask);
                if (hits.Length > 0)
                {
                    HeldGO = hits[0].collider.gameObject;

                    HeldGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    HeldGO.transform.SetParent(HoldPos, true);
                    HeldGO.transform.localPosition = Vector3.zero;
                    //HeldGO.transform.localScale = Vector3.one;

                    Holding = true;
                }
            }
            else
            {
                HeldGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                HeldGO.transform.SetParent(null);
                //HeldGO.transform.localPosition = Vector3.zero;

                HeldGO = null;

                Holding = false;
            }

        }
    }

    private void OnDrawGizmos()
    {
        if (DEBUG)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 20, Color.red, 0.1f);
        }
    }
}
