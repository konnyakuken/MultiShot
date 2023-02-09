using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotController : MonoBehaviour
{
    [SerializeField] public float ShotGauge = 0f;
    [SerializeField] GameObject Bullet;
    [SerializeField] public Slider slider;
    public int MaxGauge = 3;

    void Start()
    {
        slider.value = 0;
    }

    
    void Update()
    {
        if(ShotGauge < MaxGauge)
        {
            ShotGauge += 0.3f * Time.deltaTime;
            slider.value = ShotGauge;
        }

        if (Input.GetMouseButtonDown(0) && ShotGauge >1)
        {
            BulletShot();
        }
    }

    public void BulletShot()
    {
        ShotGauge -= 1;
        Instantiate(Bullet, transform.position, transform.rotation);
    }
}
