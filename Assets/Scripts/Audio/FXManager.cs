using UnityEngine;

/// <summary>
/// Simple visual effects manager spawning particle systems for game events.
/// </summary>
public class FXManager : MonoBehaviour
{
    public static FXManager Instance { get; private set; }

    [SerializeField] private ParticleSystem leavesPrefab;
    [SerializeField] private ParticleSystem sparklePrefab;

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

    public void PlayLeaves(Vector3 position)
    {
        Spawn(leavesPrefab, position);
    }

    public void PlaySparkle(Vector3 position)
    {
        Spawn(sparklePrefab, position);
    }

    private void Spawn(ParticleSystem prefab, Vector3 position)
    {
        if (prefab == null)
        {
            return;
        }

        var ps = Instantiate(prefab, position, Quaternion.identity);
        var main = ps.main;
        Destroy(ps.gameObject, main.duration + main.startLifetime.constantMax);
    }
}
