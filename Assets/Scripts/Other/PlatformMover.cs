using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float speed;
    public float amplitude;
    Vector3 centerPos;
    float direction = -1;
    void Start()
    {
        centerPos = GetComponent<Transform>().position;
    }
    void Update()
    {
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
        if (Vector3.Distance(centerPos, transform.position) > amplitude)
        {
            direction = -direction;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.transform.SetParent(transform);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
