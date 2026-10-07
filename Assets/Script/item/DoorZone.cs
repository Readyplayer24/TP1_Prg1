using UnityEngine;

public class DoorZone : MonoBehaviour
{
    [SerializeField] private int requiredKeys = 2;
    private int currentKeysInserted = 0;

    [SerializeField] private Transform doorTransform;
    [SerializeField] private float openHeight = 5f;
    [SerializeField] private float openSpeed = 2f;

    private bool shouldOpen = false;
    private Vector3 targetPosition;

    private void Start()
    {
        if (doorTransform != null)
        {
            targetPosition = doorTransform.position + new Vector3(0, openHeight, 0);
        }
    }

    private void Update()
    {
        if (shouldOpen && doorTransform != null)
        {
            doorTransform.position = Vector3.Lerp(doorTransform.position, targetPosition, Time.deltaTime * openSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform holdPoint = other.transform.Find("HoldPoint");
            if (holdPoint != null)
            {
                KeyItem[] keysHolding = holdPoint.GetComponentsInChildren<KeyItem>();

                foreach (KeyItem key in keysHolding)
                {
                    currentKeysInserted++;
                    key.ConsumeKey();
                    Debug.Log($"Llave entregada. ({currentKeysInserted}/{requiredKeys})");

                    if (currentKeysInserted >= requiredKeys)
                    {
                        OpenDoor();
                        break;
                    }
                }
            }
        }
    }

    private void OpenDoor()
    {
        shouldOpen = true;
        Debug.Log("¡Las dos llaves insertadas! Abriendo puerta...");
    }
}