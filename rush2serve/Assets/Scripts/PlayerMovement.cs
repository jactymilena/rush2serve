using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed = 4f;

    private NavMeshAgent navMeshAgent;
    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = transform.position;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = speed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            navMeshAgent.SetDestination(targetPosition);
        }
    }
}
