using UnityEngine;

/// <summary>
/// ScriptableObject representing a wood delivery quest.
/// Stores the amount of wood required and the reward given on completion.
/// </summary>
[CreateAssetMenu(menuName = "Quests/Quest")]
public class QuestSO : ScriptableObject
{
    [Tooltip("How much wood the player must deliver to finish the quest.")]
    public int woodRequired = 10;

    [Tooltip("Gold rewarded for completing the quest.")]
    public int goldReward = 0;
}
