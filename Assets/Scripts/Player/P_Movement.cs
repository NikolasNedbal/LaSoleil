using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 2f;
    Rigidbody2D rb;
    Vector2 motionVector;
    public Vector2 lastMotionVector;
    private bool sprint;
    Animator animator;
    public bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector2(horizontal, vertical);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        }

        isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", isMoving);

        if (isMoving)
        {
            lastMotionVector = new Vector2(horizontal, vertical).normalized;
        }
        animator.SetFloat("lastHorizontal", lastMotionVector.x);
        animator.SetFloat("lastVertical", lastMotionVector.y);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    private void Move()
    {
        if (sprint)
        {
            rb.linearVelocity = motionVector * (2 * speed);
        }
        else
        {
            rb.linearVelocity = motionVector * speed;
        }
    }
}
