using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float spawnInterval = 2.5f;

    private void Start()
    {
        if (obstaclePrefab != null)
        {
            InvokeRepeating(nameof(SpawnObstacle), startDelay, spawnInterval);
        }
        else
        {
            Debug.LogWarning("Falta asignar el obstaclePrefab en el Spawner.");
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, transform.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.4f);
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }
}