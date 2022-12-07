using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10.0f;
    Vector3 dir;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        // 自身の向きベクトル取得
        float angleDir = (transform.eulerAngles.z + 90)  * Mathf.Deg2Rad;//(Mathf.PI / 180.0f); //自分の角度をラジアンで取得
        dir = new Vector3(Mathf.Cos(angleDir ), Mathf.Sin(angleDir), 0.0f);// 向きを計算
        Invoke(nameof(DelayMethod), 2f);
    }

    void DelayMethod()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }


    void Update()
    {
        rb.velocity = dir * speed;

    }
}
