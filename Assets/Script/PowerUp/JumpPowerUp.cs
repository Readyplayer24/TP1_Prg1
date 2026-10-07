using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplyJumpBoost(boostMultiplier);

                Destroy(gameObject);
            }
        }
    }
}