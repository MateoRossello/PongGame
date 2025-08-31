using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private KeyCode moveUpKey;
    [SerializeField] private KeyCode moveDownKey;
    [SerializeField] private float yBound = 3.75f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float verticalInput = 0f;

        if (Input.GetKey(moveUpKey))
        {
            verticalInput = 1f;
        }
        else if(Input.GetKey(moveDownKey))
        {
            verticalInput = -1f;
        }

        Vector2 targetPosition = rb.position + speed * Time.deltaTime * new Vector2(0, verticalInput);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -yBound, yBound);
        rb.MovePosition(targetPosition);
    }

    public void Restart()
    {
        Vector2 startPosition = new(transform.position.x, 0f);
        transform.position = startPosition;
    }
}
