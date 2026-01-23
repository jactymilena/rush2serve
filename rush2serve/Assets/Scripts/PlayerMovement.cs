using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed = 4f;

    private Rigidbody2D playerRigidbody;
    private Vector2 targetPosition;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        MoveToTarget();
    }

    void MoveToTarget()
    {
        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, targetPosition);

        if (distance > 0.1f)
        {
            Vector2 movement = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
            playerRigidbody.MovePosition(movement);
        }
        else
        {
            playerRigidbody.linearVelocity = Vector2.zero;
        }
    }
}
