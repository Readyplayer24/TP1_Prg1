using UnityEngine;

public class RunPowerUp : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.8f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplyPermanentSpeedBoost(speedMultiplier);

                Destroy(gameObject);
            }
        }
    }
}