using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBallPos : MonoBehaviour
{
    private Camera arCamera;
    public ThrowBall ball;

    public RectTransform powerGage;
    public RectTransform arrow;

    private void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        transform.position = arCamera.gameObject.transform.position;
    }
}
