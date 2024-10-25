using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour,IDamageable
{
    public float maxLife = 8;
    public float life = 8;
    private Slider healthSlider;
    private Image sliderColor;
    public void TakeDamage(float damage)
    {
        life-=damage;
        if (life <= 0) 
        {
            if (gameObject.name == "Boss") 
            {
                GameObject.Find("Handler").GetComponent<ButtonHandler>().Win();
            }
            Destroy(this.gameObject);
        }
        UpdateLife();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        healthSlider.maxValue = maxLife;
        sliderColor = healthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        UpdateLife();
    }

    // Update is called once per frame
    public void UpdateLife()
    {
        healthSlider.value = life;
        sliderColor.color = Color.Lerp(Color.red, Color.green, life / maxLife);
    }
}
