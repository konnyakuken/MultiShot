using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private int RandomItem = 0; 
   
    void Start()
    {
        RandomItem = Random.Range(0, 3);
        print(RandomItem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") ==true)
        {
            collision.GetComponent<StatusController>().ItemResult(RandomItem);
            Destroy(this.gameObject);

        }
    }
}
