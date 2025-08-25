using TMPro;
using UnityEngine;

/// <summary>
/// Updates a HUD text element to display the current wood count.
/// Listens to <see cref="Inventory.OnWoodChanged"/> to remain in sync
/// with the player's inventory.
/// </summary>
public class WoodCounterUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Text element used to display the player's wood count.")]
    private TMP_Text woodText;

    private void OnEnable()
    {
        if (Inventory.Instance != null)
        {
            Inventory.Instance.OnWoodChanged += HandleWoodChanged;
            HandleWoodChanged(Inventory.Instance.woodCount);
        }
    }

    private void OnDisable()
    {
        if (Inventory.Instance != null)
        {
            Inventory.Instance.OnWoodChanged -= HandleWoodChanged;
        }
    }

    private void HandleWoodChanged(int amount)
    {
        if (woodText != null)
        {
            woodText.text = amount.ToString();
        }
    }
}
