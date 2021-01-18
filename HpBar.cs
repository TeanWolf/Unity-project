using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpBar : MonoBehaviour
{
    public Slider slider;
    public Image hpBar;
    public float fill;
    private PlayerControler playerControler;

    void Start()
    {
        fill = 1f;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }


    // Update is called once per frame
    void Update()
    {
        fill -= playerControler.healthPlayer ;
        hpBar.fillAmount = fill;
    }
}
