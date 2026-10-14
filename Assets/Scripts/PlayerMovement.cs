using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new(horizontal, vertical);

        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }

        Vector3 movement = new(direction.x, direction.y, 0f);

        transform.position += moveSpeed * Time.deltaTime * movement;
    }

    void FixedUpdate()
    {
        Vector2 targetPosition =
            rb.position + (moveSpeed * Time.fixedDeltaTime * direction);

        rb.MovePosition(targetPosition);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("You hit: " + collision.gameObject.name + "!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Coin")
        {
            Debug.Log("You received a coin !");
        }
    }

}