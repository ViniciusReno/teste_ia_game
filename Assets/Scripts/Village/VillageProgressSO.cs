using UnityEngine;

/// <summary>
/// Defines the sequence of well construction phases for the village.
/// Each phase requires a certain amount of wood and uses a specific prefab.
/// </summary>
[CreateAssetMenu(menuName = "Village/Village Progress")]
public class VillageProgressSO : ScriptableObject
{
    [System.Serializable]
    public class Phase
    {
        [Tooltip("Amount of wood required to reach this phase.")]
        public int woodRequired = 10;

        [Tooltip("Prefab representing the well during this phase.")]
        public GameObject wellPrefab;
    }

    [Tooltip("Ordered list of well construction phases.")]
    public Phase[] phases = new Phase[]
    {
        new Phase { woodRequired = 10 },
        new Phase { woodRequired = 20 },
        new Phase { woodRequired = 30 }
    };
}
