using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotController : MonoBehaviour
{
    [SerializeField] private float ShotGauge = 0f;
    [SerializeField] GameObject Bullet;
    [SerializeField] Slider slider;

    void Start()
    {
        slider.value = 0;
    }

    
    void Update()
    {
        if(ShotGauge < 3)
        {
            ShotGauge += 0.3f * Time.deltaTime;
            slider.value = ShotGauge;
        }

        if (Input.GetMouseButtonDown(0) && ShotGauge >1)
        {
            ShotGauge -= 1;
            Instantiate(Bullet, transform.position, transform.rotation);
        }
    }
}
