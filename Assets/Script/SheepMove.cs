using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMove : MonoBehaviour
{
    public SheepBottle bottle;


    private Transform toPoint;

    private SheepWithJar sheepWithJar;
    private float moveSpeed;
    private float rotateSpeed;
    private float jumpForce = 20f;
    private bool isJumping = false;

    private void OnEnable()
    {
        isJumping = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

        bottle.transform.SetParent(transform);
        bottle.SetDefault();
    }

    public void SetSpeed(float mSpeed, float rSpeed, float jForce)
    {
        moveSpeed = mSpeed;
        rotateSpeed = rSpeed;
        jumpForce = jForce;
    }

    public void SetDestination(Transform destination)
    {
        toPoint = destination;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, toPoint.position, moveSpeed * Time.deltaTime);
        transform.LookAt(toPoint);

        if (isJumping)
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SheepOut")
        {
            sheepWithJar = transform.parent.gameObject.GetComponent<SheepWithJar>();
            sheepWithJar.ReturnSheep(gameObject);
        }

        if(other.tag == "SheepFly")
        {
            Jump();
        }
    }

    public void Jump()
    {
        Vector3 dir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * 80), Mathf.Sin(Mathf.Deg2Rad * 80), 0);
        Debug.Log(dir);
        GetComponent<Rigidbody>().AddForce(dir * jumpForce);
        isJumping = true;
    }
}
