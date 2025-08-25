using UnityEngine;

/// <summary>
/// Configuration data for a tree, including how many attempts the
/// player has before the tree respawns and the time it takes to respawn.
/// </summary>
[CreateAssetMenu(menuName = "Trees/Tree Data")]
public class TreeSO : ScriptableObject
{
    [Tooltip("Number of times the tree can be interacted with before it respawns.")]
    public int attempts = 5;

    [Tooltip("Time in seconds before the tree respawns.")]
    public float respawnTime = 30f;
}
