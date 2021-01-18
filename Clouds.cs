using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds: MonoBehaviour
{
    public float speed = 2.0f;
    public Vector2 dir;
    Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed*dir*Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Delete")
        {
            Destroy(this.gameObject);
        }
    }
}
