using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttacker
{
    public int damage = 5;
    public float attackCooldown = 5;
    public float shootCooldown = 1.2f;
    public float distanceSpawn = 1f;
    [HideInInspector]
    public float shootTimer = 0;
    private float attackTimer = 5;
    public float bulletSpeed;
    public GameObject bullet;
    private Animator animator;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Attack(collision.gameObject.GetComponent<IDamageable>());
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        attackTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack"))
        {
            animator.SetBool("Attack", false);
        }
    }
    public void Attack(IDamageable enemy)
    {
        if (enemy != null && attackTimer >= attackCooldown)
        {
            animator.SetBool("Attack", true);
            enemy.TakeDamage(damage);
            attackTimer = 0;
        }
    }
    public void Shoot(GameObject player) 
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle <= 10f)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + transform.forward * distanceSpawn + transform.up, Quaternion.identity);
            newBullet.transform.LookAt(player.transform.position + 0.5f * Vector3.up);
            newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletSpeed;
            shootTimer = 0;
        }
    }
}
