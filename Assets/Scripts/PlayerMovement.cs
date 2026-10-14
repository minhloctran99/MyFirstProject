using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    void Start()
    {

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
}