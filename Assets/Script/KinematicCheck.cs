using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicCheck : MonoBehaviour
{
    public SheepBottle sheepBottle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            sheepBottle.inTrigger = true;
            sheepBottle.rigidbody.isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball" && !sheepBottle.isCollision)
        {
            sheepBottle.inTrigger = false;
            sheepBottle.rigidbody.isKinematic = true;
        }
    }
}
