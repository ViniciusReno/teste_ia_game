using UnityEngine;

/// <summary>
/// Handles the progression of the village's well through various phases.
/// Spawns the correct prefab for the current phase and assigns quests
/// requiring the wood needed for the next upgrade.
/// </summary>
public class VillageProgress : MonoBehaviour
{
    [SerializeField]
    private VillageProgressSO progressData;

    [SerializeField]
    private Animator waterAnimator;

    private int currentPhaseIndex;
    private GameObject currentWell;

    private void Start()
    {
        SpawnPhase();
        GenerateQuestForCurrentPhase();
    }

    /// <summary>
    /// Amount of wood required for the active phase.
    /// </summary>
    public int CurrentPhaseWood =>
        progressData != null && currentPhaseIndex < progressData.phases.Length
            ? progressData.phases[currentPhaseIndex].woodRequired
            : 0;

    /// <summary>
    /// Whether all phases have been completed.
    /// </summary>
    public bool IsComplete => progressData == null || currentPhaseIndex >= progressData.phases.Length;

    /// <summary>
    /// Advances the well to the next phase.  When the final phase is completed
    /// the current well is removed and a water animation is triggered.
    /// A new quest is generated for any remaining phase.
    /// </summary>
    public void AdvancePhase()
    {
        if (IsComplete)
        {
            return;
        }

        currentPhaseIndex++;
        FXManager.Instance?.PlaySparkle(transform.position);

        if (IsComplete)
        {
            if (currentWell != null)
            {
                Destroy(currentWell);
            }

            waterAnimator?.SetTrigger("Fill");
            QuestManager.Instance.ClearQuest();
        }
        else
        {
            SpawnPhase();
            GenerateQuestForCurrentPhase();
        }
    }

    private void SpawnPhase()
    {
        if (progressData == null || currentPhaseIndex >= progressData.phases.Length)
        {
            return;
        }

        if (currentWell != null)
        {
            Destroy(currentWell);
        }

        var prefab = progressData.phases[currentPhaseIndex].wellPrefab;
        if (prefab != null)
        {
            currentWell = Instantiate(prefab, transform.position, transform.rotation, transform);
        }
    }

    private void GenerateQuestForCurrentPhase()
    {
        if (IsComplete)
        {
            return;
        }

        var quest = ScriptableObject.CreateInstance<QuestSO>();
        quest.woodRequired = CurrentPhaseWood;
        QuestManager.Instance.AssignQuest(quest);
    }
}
