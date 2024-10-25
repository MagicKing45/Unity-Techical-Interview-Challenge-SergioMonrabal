using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour, IAttacker
{
    public float bulletDamage = 5f;
    public float destroyTime = 20;
    public float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime) 
        {
            Destroy(gameObject);
        }
    }
    public void Attack(IDamageable enemy)
    {
        enemy.TakeDamage(bulletDamage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null) 
        {
            Attack(collision.gameObject.GetComponent<IDamageable>());
        }
        Destroy(gameObject);
    }
}
