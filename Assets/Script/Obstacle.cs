using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float speed = 25f;
    [SerializeField] private float pushForce = 8f;
    [SerializeField] private float pushDuration = 0.2f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController controller = collision.gameObject.GetComponent<CharacterController>();

            if (controller != null)
            {
                Vector3 pushDirection = transform.forward;
                StartCoroutine(ApplyKnockback(controller, pushDirection));
            }

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, pushDuration);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplyKnockback(CharacterController controller, Vector3 direction)
    {
        float timer = 0f;
        while (timer < pushDuration)
        {
            controller.Move(direction * pushForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}