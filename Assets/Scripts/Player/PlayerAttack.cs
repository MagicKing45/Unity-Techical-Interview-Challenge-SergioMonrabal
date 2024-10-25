using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttacker
{
    public int damage = 5;
    public float attackCooldown = 2;
    private float attackTimer = 5;
    public GameObject bloodParticles;
    Animator animator;
    AudioSource audioSource;
    public void Attack(IDamageable enemy)
    {
        if (enemy != null && attackTimer >= attackCooldown)
        {
            audioSource.PlayOneShot(audioSource.clip);
            GameObject particles = Instantiate(bloodParticles, transform.position + transform.forward + transform.up, Quaternion.identity);
            Destroy(particles, 0.5f);
            animator.SetBool("Attack", true);
            enemy.TakeDamage(damage);
            attackTimer = 0;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack"))
        {
            animator.SetBool("Attack", false);
        }
        attackTimer += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attack(other.gameObject.GetComponent<IDamageable>());
        }
    }
}
