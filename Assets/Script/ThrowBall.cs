using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{
    public StaticBallPos ballPos;

    public RectTransform powerGage;
    public RectTransform arrow;

    public Transform frontPos;
    public Transform backPos;

    public float gageSpeed;
    public float minPower;
    public float maxPower;
    public float spawnTime;

    private Camera arCamera;
    private float gagePower = 0;
    private Vector3 firstPos;
    private Vector3 touchPos;
    public float angle;
    private Vector3 dir;
    private float throwPower;
    private float time = 0;
    private bool spawnWait = false;


    private void Start()
    {
        firstPos = transform.position;
        arCamera = Camera.main;
        powerGage = ballPos.powerGage;
        arrow = ballPos.arrow;
    }

    private void OnMouseDrag()
    {
        touchPos = Input.mousePosition;
        arrow.gameObject.SetActive(true);

        //던지는 힘
        float distance = (frontPos.position - transform.position).magnitude;
        float fullPower = (frontPos.position - backPos.position).magnitude;
        gagePower = distance / fullPower;
        if (gagePower > 1)
            gagePower = 1;
        powerGage.localScale = new Vector3(1,gagePower,1);

        //던지는 방향
        angle = Mathf.Atan2(touchPos.y - arrow.position.y, touchPos.x - arrow.position.x) * Mathf.Rad2Deg;
        if (angle > -60)
            angle = -60;
        if (angle < -120)
            angle = -120;
        arrow.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        Debug.Log(arCamera.WorldToScreenPoint(firstPos).y);
        Debug.Log(Input.mousePosition.y);

        //공 끌어당기기
        if (Input.mousePosition.y < arCamera.WorldToScreenPoint(transform.position).y)
            transform.position = Vector3.Lerp(transform.position, backPos.position, Time.deltaTime * gageSpeed);
    }

    private void OnMouseUp()
    {
        arrow.gameObject.SetActive(false);

        //던지는 처리
        dir = Quaternion.AngleAxis(angle + 90, Vector3.forward) * (frontPos.position - backPos.position).normalized;
        throwPower = GameManager.Remap(gagePower, 0, 1, minPower, maxPower);

        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.AddForce(dir * throwPower, ForceMode.Impulse);

        spawnWait = true;
    }

    private void Update()
    {
        if(spawnWait)
        {
            time += Time.deltaTime;
            if(time > spawnTime)
            {
                GameManager.Instance.SetBall();
                Destroy(gameObject);
            }
        }
    }

}
