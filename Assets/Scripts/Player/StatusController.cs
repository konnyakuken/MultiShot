using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StatusController : MonoBehaviour
{
    public Slider slider;
    public Image SliderImage;
    private bool isHit = false;

    [SerializeField]
    private TextMeshProUGUI EventText;

    private enum Item_TYPE
    {
        Life = 0,    // 体力全回復
        Mix = 1,  // 体力回復、銃弾全回復
        Bullet = 2    // 銃弾量UP(Max5)
    }


    void Start()
    {
        slider.value = 3;
    }

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
            if (isHit == false)
            {
                slider.value -= 1;
                LifeReflect();
                isHit = true;
            }
            DOVirtual.DelayedCall(0.3f, () => isHit = false) ;
        }
    }

    private void LifeReflect()
    {

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
        else
        {
            SliderImage.color = new Color(142.0f / 255, 231.0f / 255, 134.0f / 255, 1);
        }
    }

    public void ItemResult(int result)
    {
        GameObject child = transform.GetChild(1).gameObject;
        ShotController shotController = child.GetComponent<ShotController>();
        switch (result)
        {
            case (int)Item_TYPE.Life:
                slider.value = 3;
                LifeReflect();
                EventText.text = "体力が全回復した！";
                DOVirtual.DelayedCall(3f, () => EventText.text = "");
                break;
            case (int)Item_TYPE.Mix:
                slider.value += 1;
                shotController.ShotGauge = shotController.MaxGauge;
                shotController.slider.value = shotController.ShotGauge;
                LifeReflect();
                EventText.text = "体力と銃弾ゲージが回復した！";
                DOVirtual.DelayedCall(3f, () => EventText.text = "");
                break;
            case (int)Item_TYPE.Bullet:
                if(shotController.MaxGauge == 5)
                {
                    EventText.text = "何もおこらなかった!";
                }
                else
                {
                    shotController.MaxGauge += 1;
                    shotController.slider.maxValue = shotController.MaxGauge;
                    EventText.text = "最大装填数が" + shotController.MaxGauge.ToString() + "になった！";
                }
                DOVirtual.DelayedCall(3f, () => EventText.text = "");
                break;

        }
        print(result);
    }

}
