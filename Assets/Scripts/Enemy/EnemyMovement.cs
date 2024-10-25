using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum State 
{
    Wander,
    Seek,
    Shoot
}
public class EnemyMovement : MonoBehaviour
{
    public float wanderRadius = 5f;
    public float seeRadius = 5f;
    public float wanderTimer = 6f;
    private float timer = 5;
    private NavMeshAgent agent;
    public State state;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        state = State.Wander;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        //See if player is visible for the agent
        RaycastHit hit;
        Physics.Raycast(this.transform.position, player.transform.position - this.transform.position, out hit);
        //Set states
        if (Vector3.Distance(transform.position, player.transform.position) < seeRadius && hit.collider.tag == "Player")
        {
            state = State.Seek;
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < wanderRadius)
            {
                if (hit.collider.tag == "Player" && GetComponent<EnemyAttack>().shootCooldown <= GetComponent<EnemyAttack>().shootTimer)
                {
                    state = State.Shoot;
                }
                else
                {
                    state = State.Wander;
                }
            }
            else
            {
                state = State.Wander;
            }
        }
        //State Machine
        switch (state)
        {
            case State.Wander:
                Wander();
                break;
            case State.Seek:
                Seek(player.transform);
                break;
            case State.Shoot:
                Shoot(player);
                break;
        }
    }
    void Wander() 
    {
        if (timer >= wanderTimer || agent.remainingDistance <= 0.5f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
    void Seek(Transform playerTransform) 
    {
        agent.SetDestination(playerTransform.position);
    }
    void Shoot(GameObject objective) 
    {
        agent.SetDestination(player.transform.position);
        GetComponent<EnemyAttack>().Shoot(objective);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}
