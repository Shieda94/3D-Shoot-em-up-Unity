using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BasicEnemy1 config;
    [SerializeField] private Transform target;

    private Rigidbody rb;

    private Vector3 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void UpdateMovement()
    {
        if (target == null)
            return;

        moveDirection = (target.position - transform.position).normalized;

        Vector3 targetVelocity = moveDirection * config.moveSpeed;

        Vector3 currentVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        Vector3 newVelocity = Vector3.MoveTowards(
            currentVelocity,
            targetVelocity,
            config.acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = new Vector3(
            newVelocity.x,
            rb.linearVelocity.y,
            newVelocity.z
        );
    }
}