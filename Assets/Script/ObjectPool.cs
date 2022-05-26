using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject poolingObjectPrefb;

    Stack<GameObject> poolingObjectStack = new Stack<GameObject>();

    private void Awake()
    {
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for(int i =0; i<initCount; i++)
        {
            poolingObjectStack.Push(CreateNewObject());
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(poolingObjectPrefb);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public GameObject GetObject()
    {
        if(poolingObjectStack.Count > 0)
        {
            GameObject obj = poolingObjectStack.Pop();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        poolingObjectStack.Push(obj);
    }
}
