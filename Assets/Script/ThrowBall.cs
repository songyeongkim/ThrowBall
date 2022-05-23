using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{
    public RectTransform powerGage;
    public float throwPower = 0;
    public float gageSpeed;
    public float powerPlusSpeed;
    public RectTransform arrow;
    public Camera arCamera;

    public Transform frontPos;
    public Transform backPos;

    private Vector3 firstPos;
    private Vector3 touchPos;
    private float angle;



    //오래 누르고 있으면 게이지 변화 (power)
    //잡아서 뒤쪽으로 끌어야 공 던지는 준비 (dir)
    //뒤쪽으로 끈 채로 방향 전환 (rotation)
    //위쪽으로 드래그 - 뗄 시 공 던지기

    private void Start()
    {
        firstPos = transform.position;
    }

    private void OnMouseDrag()
    {
        touchPos = Input.mousePosition;

        //던지는 힘 0~1
        float distance = (frontPos.position - transform.position).magnitude;
        float fullPower = (frontPos.position - backPos.position).magnitude;
        throwPower = distance / fullPower * gageSpeed;
        if (throwPower > 1)
            throwPower = 1;
        powerGage.localScale = new Vector3(1,throwPower,1);


        //던지는 방향
        angle = Mathf.Atan2(touchPos.y - arrow.position.y, touchPos.x - arrow.position.x) * Mathf.Rad2Deg;
        arrow.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        Debug.Log(arCamera.WorldToScreenPoint(firstPos).y);
        Debug.Log(Input.mousePosition.y);

        //공 끌어당기기
        if (Input.mousePosition.y < arCamera.WorldToScreenPoint(transform.position).y)
            transform.localPosition = Vector3.Lerp(transform.localPosition, backPos.position, Time.deltaTime);
    }

}
