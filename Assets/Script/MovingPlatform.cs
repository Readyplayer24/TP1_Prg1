using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Puntos de Movimiento")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Configuración de Velocidad y Espera")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float waitTime = 2f;

    private Vector3 targetPosition;
    private bool isWaiting = false;

    private void Start()
    {
        if (pointA != null && pointB != null)
        {
            targetPosition = pointB.position;
        }
    }

    private void Update()
    {
        if (isWaiting || pointA == null || pointB == null) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            isWaiting = true;
            Invoke(nameof(SwitchDirection), waitTime);
        }
    }

    private void SwitchDirection()
    {
        targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        isWaiting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireCube(pointA.position, Vector3.one * 0.5f);
            Gizmos.DrawWireCube(pointB.position, Vector3.one * 0.5f);
        }
    }
}
