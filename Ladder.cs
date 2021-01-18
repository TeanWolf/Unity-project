using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    //GameObject LadderCollider;

    void Start()
    {
        //LadderCollider.GetComponent<BoxCollider2D>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().gravityScale = 0;
        //LadderCollider.GetComponent<BoxCollider2D>().enabled = false;
        if (other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.W))
            {
                //LadderCollider.GetComponent<BoxCollider2D>().enabled = true;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                //LadderCollider.GetComponent<BoxCollider2D>().enabled = true;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
        //LadderCollider.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
