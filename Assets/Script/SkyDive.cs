using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDive : GoalPost
{
    public float rotateSpeed;
    public float fallingSpeed;

    public Transform upPos;
    public Transform downPos;

    private void Start()
    {
        transform.position = upPos.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);

        if (transform.position.y <= downPos.position.y)
            transform.position = upPos.position;

    }
}
