using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnGold : MonoBehaviour
{

    public bool isGold = false;
    [SerializeField] private Material goldMaterial, cardboardMaterial;
    Renderer r;
    Rigidbody objectRB;

    private void Start()
    {
        r = transform.GetComponent<Renderer>();
        objectRB = transform.GetComponent<Rigidbody>();
    }

    public void ChangeObjectToGold()
    {
        if (isGold == false)
        {
            r.material = goldMaterial;
            objectRB.constraints = RigidbodyConstraints.FreezePositionY;
            objectRB.constraints = RigidbodyConstraints.FreezePositionZ;
            objectRB.mass = 100;
            isGold = true;
        }
    }

    public void ChangeObjectBack()
    {
        if (isGold == true)
        {
            r.material = cardboardMaterial;
            objectRB.constraints = RigidbodyConstraints.None;
            objectRB.mass = 1;
            isGold = false;
        }
    }
}
