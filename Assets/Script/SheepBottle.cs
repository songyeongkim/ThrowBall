using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBottle : MonoBehaviour
{
    public bool isCollision = false;
    public bool inTrigger = false;
    public GameObject explosion;
    public GameObject bottleShape;
    public Rigidbody rigidbody;
    public Transform bottlePos;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            gameObject.transform.SetParent(null);
            isCollision = true;
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(0.2f);
        SetExplosion();
        yield return new WaitForSeconds(4f);
    }

    public void SetDefault()
    {
        explosion.SetActive(false);
        bottleShape.SetActive(true);

        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;

        transform.position = bottlePos.position;
        transform.rotation = bottlePos.rotation;
    }

    public void SetExplosion()
    {
        explosion.SetActive(true);
        bottleShape.SetActive(false);

        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = true;
    }



    private void Update()
    {
        if(inTrigger && !isCollision)
        {
            transform.position = bottlePos.position;
            transform.rotation = bottlePos.rotation;
        }
    }
}
