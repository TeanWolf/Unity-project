using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject DeathScene;
    private PlayerControler playerControler;
    public GameObject otherGameObject;
    void Awake()
    {
        playerControler = otherGameObject.GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControler.isDead!=false)
        {
            Debug.Log("Cdox");
            DeathScene.SetActive(true);
        }
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
