using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private float speed = 5.0f;
    private Vector2 inputAxis;

    [SerializeField] GameObject Bullet;

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
        rb.velocity = inputAxis.normalized * speed;

        var pos = Camera.main.WorldToScreenPoint(transform.localPosition);
        var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
        transform.localRotation = rotation;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot = Instantiate(Bullet, transform.position, transform.rotation); 
        }

    }

}
