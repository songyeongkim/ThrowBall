using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool checkedUpper = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GoalUpper" && !checkedUpper)
        {
            checkedUpper = true;
        }

        if(other.tag == "GoalBottom" && checkedUpper)
        {
            GameManager.Instance.Goal();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            GameManager.Instance.Goal();
        }
    }
}
