using UnityEngine;

public class CheckpointRespawn : MonoBehaviour
{
    [Header("Configuración de Caída")]
    [SerializeField] private float fallThreshold = -10f;
    [SerializeField] private float checkInterval = 0.5f;

    private Vector3 lastGroundedPosition;
    private Quaternion lastGroundedRotation;
    private CharacterController characterController;
    private float timer;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        SaveCurrentPosition();
    }

    private void Update()
    {
        if (characterController != null && characterController.isGrounded)
        {
            timer += Time.deltaTime;
            if (timer >= checkInterval)
            {
                SaveCurrentPosition();
                timer = 0f;
            }
        }
        if (transform.position.y < fallThreshold)
        {
            RespawnToLastCheckpoint();
        }
    }

    public void SaveCurrentPosition()
    {
        lastGroundedPosition = transform.position;
        lastGroundedRotation = transform.rotation;
    }

    public void RespawnToLastCheckpoint()
    {
        if (characterController != null)
        {
            characterController.enabled = false;
            transform.position = lastGroundedPosition;
            transform.rotation = lastGroundedRotation;
            characterController.enabled = true;
        }
        else
        {
            transform.position = lastGroundedPosition;
            transform.rotation = lastGroundedRotation;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-50f, fallThreshold, -50f), new Vector3(50f, fallThreshold, 50f));
    }
}