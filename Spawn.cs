using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject cloud;
    public float TimeSpawn;

    void Start()
    {
        StartCoroutine(SpawnCD());
    }

    void Repeat()
    {
        StartCoroutine(SpawnCD());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Delete")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpawnCD()
    {
        yield return new WaitForSeconds(TimeSpawn);
        Instantiate(cloud, SpawnPos.position, Quaternion.identity);
        Repeat();
    }
}
