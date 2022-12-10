using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    private float moveSpeed = 5.0f;
    private float rotateSpeed = 3.0f;
    private Vector2 inputAxis = Vector2.zero;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        Vector2 controlSignal = Vector2.zero;
        controlSignal= inputAxis;

        //回転させる角度
        float angle = controlSignal.x * rotateSpeed;
        transform.Rotate(new Vector3(0, 0, -angle));

        float angleDir = (transform.eulerAngles.z + 90) * Mathf.Deg2Rad;
        dir = new Vector3(Mathf.Cos(angleDir), Mathf.Sin(angleDir), 0.0f);
        rb.velocity = dir.normalized * moveSpeed * controlSignal.y;
    }

}