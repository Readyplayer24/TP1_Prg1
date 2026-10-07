using UnityEngine;

public class VoidRespawn : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fallThreshold = -10f;

    private CharacterController characterController;
    private Rigidbody rb;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = spawnPoint.position;
            characterController.enabled = true;
        }
        else
        {
            transform.position = spawnPoint.position;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
