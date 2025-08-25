using UnityEngine;
using UnityEngine.UI;
using Interaction;

/// <summary>
/// Handles player interactions with <see cref="Interactable"/> objects.
/// Displays a UI prompt when inside an interactable trigger and sends the
/// interaction when the player presses the "E" key.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    [Tooltip("UI GameObject that shows the interaction prompt.")]
    private GameObject promptUI;

    [SerializeField]
    [Tooltip("Text element used to show the interaction prompt.")]
    private Text promptText;

    private Interactable currentTarget;

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentTarget = interactable;
            if (promptText != null)
            {
                promptText.text = interactable.Prompt;
            }
            if (promptUI != null)
            {
                promptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentTarget)
        {
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
            if (promptText != null)
            {
                promptText.text = string.Empty;
            }
            currentTarget = null;
        }
    }

    private void Update()
    {
        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            currentTarget.Interact();
        }
    }
}
