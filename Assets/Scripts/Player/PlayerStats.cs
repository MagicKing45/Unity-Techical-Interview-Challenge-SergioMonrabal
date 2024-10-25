using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public float maxLife = 10;
    public float life = 10;
    public float healthRegen = 0.1f;
    [SerializeField]
    private Slider healthSlider;
    private Image sliderColor;

    public void TakeDamage(float damage)
    {
        life-=damage;
        if (life <= 0) 
        {
            Dead();
        }
        UpdateLife();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = maxLife;
        healthSlider.value = life;
        sliderColor = healthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        UpdateLife();
    }

    // Update is called once per frame
    void Update()
    {
        if (life < maxLife)
        {
            life += healthRegen * Time.deltaTime;
            if (life > maxLife) life = maxLife;
            UpdateLife();
        }
        if (transform.position.y < -2.8) 
        {
            Dead();
        }
    }
    public void UpdateLife()
    {
        healthSlider.value = life;
        sliderColor.color = Color.Lerp(Color.red, Color.green, life / maxLife);

    }
    public void Dead()
    {
        GameObject.Find("Handler").GetComponent<ButtonHandler>().Dead();
    }
}
