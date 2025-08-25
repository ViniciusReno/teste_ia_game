using System.Collections.Generic;
using UnityEngine;
using Interaction;

/// <summary>
/// Interactable quest board that hands out quests to the player.
/// Quests cycle through a predefined list each time the board is used.
/// </summary>
public class QuestBoard : Interactable
{
    [SerializeField]
    [Tooltip("Queue of quests offered by this board.")]
    private List<QuestSO> questQueue = new List<QuestSO>();

    private int currentIndex;

    private void Start()
    {
        // Provide a default cycle of quests if none were assigned via the inspector.
        if (questQueue.Count == 0)
        {
            questQueue.Add(CreateQuest(10));
            questQueue.Add(CreateQuest(15));
            questQueue.Add(CreateQuest(20));
        }
    }

    private QuestSO CreateQuest(int wood)
    {
        var quest = ScriptableObject.CreateInstance<QuestSO>();
        quest.woodRequired = wood;
        quest.goldReward = 0;
        return quest;
    }

    /// <summary>
    /// Gives the player the next quest if no quest is currently active.
    /// </summary>
    public override void Interact()
    {
        if (QuestManager.Instance.ActiveQuest != null)
        {
            return;
        }

        var quest = questQueue[currentIndex];
        currentIndex = (currentIndex + 1) % questQueue.Count;
        QuestManager.Instance.AssignQuest(quest);
    }
}
