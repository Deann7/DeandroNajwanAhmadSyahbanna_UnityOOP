using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;  
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    private Vector2 minBounds = new Vector2(-8.5f, -4.9f);
    private Vector2 maxBounds = new Vector2(8.5f, 4.5f);

    public Vector2 moveDirection;
    public Vector2 moveVelocity;
    public Vector2 moveFriction;
    public Vector2 stopFriction;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * (maxSpeed / timeToFullSpeed);
        moveFriction = (-2) * (maxSpeed / new Vector2(Mathf.Pow(timeToFullSpeed.x, 2), Mathf.Pow(timeToFullSpeed.y, 2)));
        stopFriction = (-2) * (maxSpeed / new Vector2(Mathf.Pow(timeToStop.x, 2), Mathf.Pow(timeToStop.y, 2)));
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Vector2 currentVelocity = rb.velocity;
        Vector2 friction = GetFriction();

        if (moveDirection != Vector2.zero)
        {
            Vector2 targetVelocity = moveDirection * moveVelocity;
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, friction.magnitude * Time.fixedDeltaTime);
            currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxSpeed.x, maxSpeed.x);
            currentVelocity.y = Mathf.Clamp(currentVelocity.y, -maxSpeed.y, maxSpeed.y);
        }
        else
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, friction.magnitude * Time.fixedDeltaTime);
            if (Mathf.Abs(currentVelocity.x) < stopClamp.x) currentVelocity.x = 0;
            if (Mathf.Abs(currentVelocity.y) < stopClamp.y) currentVelocity.y = 0;
        }

        rb.velocity = currentVelocity;
    }

    private Vector2 GetFriction()
    {
        return moveDirection != Vector2.zero ? moveFriction : stopFriction;
    }

    private void MoveBound()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);
        transform.position = position;
    }

    private void FixedUpdate()
    {
        Move();
        MoveBound();
    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}
