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

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector2(horizontal, vertical);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
        } else
        {
            sprint = false;
        }
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        isMoving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", isMoving);

        if (horizontal != 0 || vertical != 0) 
        {
            lastMotionVector = new Vector2(horizontal, vertical).normalized;

            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }

    private void Move()
    {
        if (sprint)
        {
            rb.velocity = motionVector * (2 * speed);
        }
        else
        {
            rb.velocity = motionVector * speed;
        }
    }
}
