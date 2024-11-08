using UnityEngine;

public class Portal : MonoBehaviour
{
   [SerializeField] private float moveSpeed = -2f;
   [SerializeField] private float rotationSpeed = 1f;
   [SerializeField] private Vector2 areaBounds = new Vector2(6f, 6f);
   
   private Vector2 currDir;
   private Vector2 targetPos;
   private Rigidbody2D rb;

   void Start() // for starting the portal's position and direction
   {
       rb = GetComponent<Rigidbody2D>();
       currDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
       rb.velocity = currDir * moveSpeed;
       ChangePosition();
   }

   void Update() // for updating the portal's position
   {
       transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
       
       Vector2 position = transform.position;
       // checking boundaries for x coordinate
       if (position.x > areaBounds.x || position.x < -areaBounds.x)
       {
           currDir.x = -currDir.x;
           position.x = Mathf.Clamp(position.x, -areaBounds.x, areaBounds.x);
       }
        // checking boundaries for y coordinate
       if (position.y > areaBounds.y || position.y < -areaBounds.y)
       {
           currDir.y = -currDir.y;
           position.y = Mathf.Clamp(position.y, -areaBounds.y, areaBounds.y);
       }
       transform.position = position;
       rb.velocity = currDir * moveSpeed;
       
       if (Vector2.Distance(transform.position, targetPos) < 0.1f)
       {
           ChangePosition();
       }
   }

   void OnTriggerEnter2D(Collider2D other) // for checking if the player is near the portal
   {
       if (other.CompareTag("Player"))
       {
           Debug.Log("Next to the next Level");
           rb.velocity = Vector2.zero;
           GetComponent<Collider2D>().enabled = false;

           if (GameManager.Instance != null && GameManager.Instance.LevelManager != null)
           {
               GameManager.Instance.LevelManager.LoadScene("Main");
           }
           else
           {
               Debug.LogWarning("GameManager atau LevelManager belum dimasukin!");
           }
       }
   }

   void ChangePosition() // for changing the portal's position
   {
       targetPos = new Vector2(
           Random.Range(-areaBounds.x, areaBounds.x),
           Random.Range(-areaBounds.y, areaBounds.y)
       );
       currDir = (targetPos - (Vector2)transform.position).normalized;
       rb.velocity = currDir * moveSpeed;
   }
}