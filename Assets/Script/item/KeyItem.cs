using UnityEngine;
using UnityEngine.InputSystem;

public class KeyItem : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private Transform holdPoint;
    private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerTransform = other.transform;
            holdPoint = playerTransform.Find("HoldPoint");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame && transform.parent == null)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        if (holdPoint != null)
        {
            transform.SetParent(holdPoint);
            int currentKeys = holdPoint.childCount - 1;
            transform.localPosition = new Vector3(currentKeys * 0.3f, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 90f, 0);

            if (TryGetComponent<Collider>(out var col))
            {
                col.enabled = false;
            }
        }
    }

    public void ConsumeKey()
    {
        Destroy(gameObject);
    }
}