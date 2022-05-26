using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepWithJar : GoalPost
{
    public ObjectPool objectPool;
    public Transform fromPoint;
    public Transform toPoint;

    public float moveSpeed;
    public float spawnTime;
    public float rotateSpeed;
    public float jumpForce;


    private List<GameObject> sheepFlock = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnTime());
    }

    IEnumerator SpawnTime()
    {
        SpawnSheep();
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnTime());
    }

    void SpawnSheep()
    {
        GameObject sheep = objectPool.GetObject();
        sheep.transform.position = fromPoint.position;
        sheep.transform.SetParent(transform);
        sheep.GetComponent<SheepMove>().SetSpeed(moveSpeed,rotateSpeed,jumpForce);
        sheep.GetComponent<SheepMove>().SetDestination(toPoint);
        sheepFlock.Add(sheep);
    }


    public void ReturnSheep(GameObject sheep)
    {
        objectPool.ReturnObject(sheep);
    }
}
