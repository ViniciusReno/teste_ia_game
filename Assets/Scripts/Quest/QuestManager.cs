using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the currently active quest and updates a small UI display.
/// </summary>
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    /// <summary>Currently active quest. Null if no quest is running.</summary>
    public QuestSO ActiveQuest { get; private set; }

    public event Action<QuestSO> OnQuestAssigned;

    [SerializeField]
    [Tooltip("UI text element used to display current quest info.")]
    private Text questText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Assigns a quest as the current active quest and updates the UI.
    /// </summary>
    public void AssignQuest(QuestSO quest)
    {
        ActiveQuest = quest;
        OnQuestAssigned?.Invoke(quest);
        if (questText != null)
        {
            questText.text = $"Gather {quest.woodRequired} wood";
        }
    }

    /// <summary>
    /// Clears the active quest and resets the UI display.
    /// </summary>
    public void ClearQuest()
    {
        ActiveQuest = null;
        if (questText != null)
        {
            questText.text = string.Empty;
        }
    }
}
