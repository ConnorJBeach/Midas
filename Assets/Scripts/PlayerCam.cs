using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX, sensY;
    float xRotation, yRotation;
    public Transform orientation;
    PickUp pickUp;
    private RaycastHit hit;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 5.0f;
    [SerializeField] private Material goldCardboard, regularCardboard;
    [SerializeField] private bool isGold = false;

    public TurnGold turnGoldScript;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pickUp = GetComponent<PickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            ShootArray();
        }
    }

    void ShootArray()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Box"))
            {
                TurnGold goldScript = hit.transform.GetComponent<TurnGold>();

                if (goldScript.isGold == false && pickUp.beingHeld == false)
                {
                    goldScript.ChangeObjectToGold();
                }

                else if (goldScript.isGold == true)
                {
                    goldScript.ChangeObjectBack();
                }
            }
        }
    }
}
