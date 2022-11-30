using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;
    bool isOpened = false;
    [SerializeField]
    Animator doorAnimator, pressurePlate;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<TurnGold>() != null)
        {
            TurnGold turnGoldScript = other.transform.GetComponent<TurnGold>();
            if (turnGoldScript.isGold == false)
            {
                Debug.Log("This isn't heavy enough, I wonder if I can make it heavier?");
            }
            else
            {
                if (isOpened == false)
                {
                    pressurePlate.SetTrigger("PressurePlateLower");
                    doorAnimator.SetTrigger("PressurePlateTrigger");
                    isOpened = true;
                }
            }
        }
    }
}
