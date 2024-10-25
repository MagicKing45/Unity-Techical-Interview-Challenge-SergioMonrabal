using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float jumpHeight = 1f;
    public float dashStrength = 1f;
    public float rotationSensibility = 5;
    private Rigidbody rb;
    private bool jumping = false;
    private float jumpDelay = 0;
    private float dashDelay = 0;

    public float smoothTime = 0.1f;
    private float currentMouseX = 0f;
    private float targetMouseX = 0f;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        CheckMovement();
    }
    private void Update()
    {
        CheckRotation();
    }
    void CheckMovement() 
    {
        dashDelay += Time.deltaTime;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal == 0 & moveVertical == 0)
        {
            animator.SetBool("Moving", false);
        }
        else 
        {
            animator.SetBool("Moving", true);
        }
        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * speed * Time.fixedDeltaTime;
        if (!jumping)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("Jumping", true);
                jumping = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpHeight * 5, rb.velocity.z);
                jumpDelay = 0;
            }
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse1) && dashDelay > 1f && jumpDelay > 0.1f)
            {
                rb.AddForce(transform.forward * 10f * dashStrength, ForceMode.Impulse);
                dashDelay = 0;
            }
            rb.MovePosition(rb.position + movement);
            jumpDelay += Time.deltaTime;
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1f) && jumpDelay > 0.25f)
            {
                animator.SetBool("Jumping", false);
                jumping = false;
            }
        }
    }
    void CheckRotation() 
    {
        targetMouseX += Input.GetAxis("Mouse X") * rotationSensibility;
        currentMouseX = Mathf.Lerp(currentMouseX, targetMouseX, smoothTime);
        transform.rotation = Quaternion.Euler(0f, currentMouseX, 0f);
    }
}
