using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Slider slider;
    public Image SliderImage;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            slider.value -= 1;
            if(slider.value == 2)
            {
                SliderImage.color = new Color(243.0f/255, 230.0f/255, 107.0f/ 255,1);
            }else if(slider.value == 1)
            {
                SliderImage.color = new Color(239.0f/255, 107.0f/255, 122.0f/255,1);
            }

            if(slider.value == 0)
            {
                slider.value = 3;
                SliderImage.color = new Color(142.0f / 255, 231.0f / 255, 134.0f / 255, 1);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet")==true)
        {
            Destroy(collision.gameObject);
            Damage();
        }
    }

    private void Damage()
    {
        slider.value -= 1;

        if (slider.value == 2)
        {
            SliderImage.color = new Color(243.0f / 255, 230.0f / 255, 107.0f / 255, 1);
        }
        else if (slider.value == 1)
        {
            SliderImage.color = new Color(239.0f / 255, 107.0f / 255, 122.0f / 255, 1);
        }
        else if (slider.value == 0)
        {
            print("GameOver");
            slider.value = 3;
            SliderImage.color = new Color(142.0f / 255, 231.0f / 255, 134.0f / 255, 1);
        }
    }
}
