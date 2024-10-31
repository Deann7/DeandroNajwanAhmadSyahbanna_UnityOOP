using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;  
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

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

        if (moveDirection != Vector2.zero)
        {
            Vector2 targetVelocity = moveDirection * moveVelocity;
            rb.velocity = Vector2.MoveTowards(currentVelocity, targetVelocity, moveFriction.magnitude * Time.fixedDeltaTime);
        }
        else
        {
        
            rb.velocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, stopFriction.magnitude * Time.fixedDeltaTime);
        }
    }

    private Vector2 GetFriction()
    {
        if (moveDirection != Vector2.zero)
        {
            return moveFriction;
        }
        else
        {
            return stopFriction;
        }
    }

    private void MoveBound()
    {
       
    }

    private void FixedUpdate()
    {
        Move();
    
    }

    public bool IsMoving()
    {
        if (moveDirection != Vector2.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
