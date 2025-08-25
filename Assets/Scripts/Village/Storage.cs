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

    public override void Interact()
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
    }
}
