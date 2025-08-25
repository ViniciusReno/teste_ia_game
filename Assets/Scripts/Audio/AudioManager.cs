using UnityEngine;

/// <summary>
/// Centralised audio handler for playing simple sound effects.
/// Holds references to pop sounds for pickups, successes and errors.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip successClip;
    [SerializeField] private AudioClip errorClip;

    private AudioSource source;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayPickup() => PlayClip(pickupClip);
    public void PlaySuccess() => PlayClip(successClip);
    public void PlayError() => PlayClip(errorClip);

    private void PlayClip(AudioClip clip)
    {
        if (clip != null && source != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
