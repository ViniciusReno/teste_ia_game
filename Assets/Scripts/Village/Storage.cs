using Interaction;
using UnityEngine;

/// <summary>
/// Storage building where the player can deliver wood.  When the active quest
/// requirement is met, wood is removed from the inventory and the village well
/// advances to the next phase.
/// </summary>
public class Storage : Interactable
{
    [SerializeField]
    private VillageProgress villageProgress;

    [SerializeField]
    [Tooltip("Panel shown when the player interacts, allowing them to deliver wood.")]
    private GameObject deliverAllPanel;

    private void Start()
    {
        if (deliverAllPanel != null)
        {
            deliverAllPanel.SetActive(false);
        }
    }

    public override void Interact()
    {
        if (deliverAllPanel != null)
        {
            deliverAllPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Delivers the required amount of wood for the active quest and advances the village.
    /// Intended to be called by the Deliver All button.
    /// </summary>
    public void DeliverAll()
    {
        var quest = QuestManager.Instance.ActiveQuest;
        if (quest == null)
        {
            return;
        }

        if (Inventory.Instance.woodCount >= quest.woodRequired)
        {
            Inventory.Instance.RemoveWood(quest.woodRequired);
            villageProgress.AdvancePhase();
        }

        if (deliverAllPanel != null)
        {
            deliverAllPanel.SetActive(false);
        }
    }
}
