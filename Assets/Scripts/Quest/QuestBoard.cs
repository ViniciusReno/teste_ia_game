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
    [Tooltip("Wood requirements for the initial quest cycle.")]
    private int[] initialSequence = { 10, 15, 20 };

    private readonly List<QuestSO> questQueue = new List<QuestSO>();

    private int currentIndex;

    public int CurrentIndex => currentIndex;

    private void Start()
    {
        questQueue.Clear();
        if (initialSequence != null)
        {
            foreach (var wood in initialSequence)
            {
                questQueue.Add(CreateQuest(wood));
            }
        }

        var data = SaveSystem.LoadGame();
        currentIndex = Mathf.Clamp(data.questIndex, 0, Mathf.Max(questQueue.Count - 1, 0));
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
        if (QuestManager.Instance.ActiveQuest != null || questQueue.Count == 0)
        {
            return;
        }

        var quest = questQueue[currentIndex];
        currentIndex = (currentIndex + 1) % questQueue.Count;
        QuestManager.Instance.AssignQuest(quest);
    }
}
