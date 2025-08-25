using System.Collections;
using UnityEngine;
using Interaction;

/// <summary>
/// Represents a tree that the player can interact with.  Tracks remaining
/// interaction attempts and handles respawn behaviour when depleted.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Tree : Interactable
{
    [SerializeField]
    [Tooltip("Configuration data for this tree.")]
    private TreeSO data;

    private int remainingAttempts;
    private Collider treeCollider;
    private Renderer[] renderers;

    private void Awake()
    {
        treeCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
        if (data != null)
        {
            remainingAttempts = data.attempts;
        }
    }

    /// <inheritdoc />
    public override void Interact()
    {
        if (remainingAttempts <= 0)
        {
            return;
        }

        int attemptNumber = (data != null ? data.attempts : 0) - remainingAttempts + 1;
        MinigameManager.StartGame(attemptNumber);
        FXManager.Instance?.PlayLeaves(transform.position);
        remainingAttempts--;

        if (remainingAttempts <= 0)
        {
            SetEnabled(false);
            StartCoroutine(RespawnCoroutine());
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        var waitTime = data != null ? data.respawnTime : 0f;
        yield return new WaitForSeconds(waitTime);
        remainingAttempts = data != null ? data.attempts : 0;
        SetEnabled(true);
    }

    private void SetEnabled(bool value)
    {
        if (treeCollider != null)
        {
            treeCollider.enabled = value;
        }
        if (renderers != null)
        {
            foreach (var r in renderers)
            {
                r.enabled = value;
            }
        }
    }
}
