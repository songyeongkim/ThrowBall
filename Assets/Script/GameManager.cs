using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera arCamera;
    public GameObject powerUI;
    public RectTransform powerGage;
    public RectTransform arrow;
    public GameObject ball;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            return;
    }

    public void SetBall()
    {
        GameObject Ball = Instantiate(ball, arCamera.gameObject.transform);
        powerUI.SetActive(true);
        Ball.GetComponent<StaticBallPos>().powerGage = powerGage;
        Ball.GetComponent<StaticBallPos>().arrow = arrow; 
    }

    public static float Remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }
}
