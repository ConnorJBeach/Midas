using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Pickup Mechanics")]
    [SerializeField]
    Transform holdSpot;
    private GameObject item;
    private Rigidbody itemRB;
    public bool beingHeld = false;

    [Header("Pickup Options")]
    [SerializeField] private float pickupRange = 5.0f, pickupForce = 150.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (item == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    TurnGold turnGoldScript = hit.transform.GetComponent<TurnGold>();

                    if (turnGoldScript.isGold == false)
                    {
                        PickUpItem(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                DropItem();
            }
        }

        if (item != null)
        {
            MoveItem();
        }
    }

    void MoveItem()
    {
        if (Vector3.Distance(item.transform.position, holdSpot.position) > 0.1f)
        {
            Vector3 moveDirection = (holdSpot.position - item.transform.position);
            itemRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickUpItem(GameObject itemToPickUp)
    {
        if (itemToPickUp.GetComponent<Rigidbody>())
        {
            itemRB = itemToPickUp.GetComponent<Rigidbody>();
            itemRB.useGravity = false;
            itemRB.drag = 10;
            itemRB.constraints = RigidbodyConstraints.FreezeRotation;

            itemRB.transform.parent = holdSpot;
            item = itemToPickUp;
            beingHeld = true;
        }
    }

    void DropItem()
    {
        itemRB.useGravity = true;
        itemRB.drag = 1;
        itemRB.constraints = RigidbodyConstraints.None;

        itemRB.transform.parent = null;
        item = null;
        beingHeld = false;
    }
}
