using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEndPortal : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subText;
    [SerializeField] private GameObject menuButton;

    [Header("Timings")]
    [SerializeField] private float delayBeforeSubtext = 2.5f;
    [SerializeField] private float delayBeforeAutoReturn = 5f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(EndLevelSequence());
        }
    }

    private IEnumerator EndLevelSequence()
    {
        if (blackScreen != null) blackScreen.SetActive(true);
        if (titleText != null) titleText.gameObject.SetActive(true);

        yield return new WaitForSeconds(delayBeforeSubtext);

        if (subText != null) subText.gameObject.SetActive(true);
        if (menuButton != null) menuButton.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSeconds(delayBeforeAutoReturn);
        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}