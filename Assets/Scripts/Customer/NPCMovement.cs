using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    private Vector3 lastPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Check if NPC is moving based on velocity magnitude
        bool isMoving = agent.velocity.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isMoving);

        if (isMoving)
        {
            // Get local velocity to determine direction
            Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
            float moveX = localVelocity.x;

            // Ensure tiny values don't cause issues
            if (Mathf.Abs(moveX) < 0.01f)
            {
                moveX = 0;
            }

            animator.SetFloat("moveX", moveX);
        }
    }
}
