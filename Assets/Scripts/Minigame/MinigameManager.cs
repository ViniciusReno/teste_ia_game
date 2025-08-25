using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the wood chopping timing minigame.
/// </summary>
public class MinigameManager : MonoBehaviour
{
    private const float GoodWidth = 0.20f;
    private const float PerfectWidth = 0.06f;
    private const float BaseSpeed = 1f;
    private const float SpeedStep = 0.5f;

    [SerializeField] private RectTransform indicator;
    [SerializeField] private RectTransform goodZone;
    [SerializeField] private RectTransform perfectZone;
    [SerializeField] private Text feedbackText;

    private float speed;
    private bool resolved;

    /// <summary>
    /// Launch the minigame.  The bar speed increases with each attempt.
    /// </summary>
    public static void StartGame(int attempt)
    {
        var prefab = Resources.Load<MinigameManager>("MinigameManager");
        if (prefab != null)
        {
            Instantiate(prefab).Initialize(attempt);
        }
    }

    private void Initialize(int attempt)
    {
        if (indicator == null)
        {
            BuildUI();
        }

        speed = BaseSpeed + (attempt - 1) * SpeedStep;

        goodZone.anchorMin = new Vector2(0.5f - GoodWidth / 2f, 0f);
        goodZone.anchorMax = new Vector2(0.5f + GoodWidth / 2f, 1f);

        perfectZone.anchorMin = new Vector2(0.5f - PerfectWidth / 2f, 0f);
        perfectZone.anchorMax = new Vector2(0.5f + PerfectWidth / 2f, 1f);
    }

    private void BuildUI()
    {
        var bar = new GameObject("Bar", typeof(RectTransform), typeof(Image));
        bar.transform.SetParent(transform, false);
        var barRect = bar.GetComponent<RectTransform>();
        barRect.anchorMin = new Vector2(0.1f, 0.45f);
        barRect.anchorMax = new Vector2(0.9f, 0.55f);

        goodZone = CreateZone("GoodZone", barRect);
        perfectZone = CreateZone("PerfectZone", barRect);

        indicator = new GameObject("Indicator", typeof(RectTransform), typeof(Image)).GetComponent<RectTransform>();
        indicator.SetParent(barRect, false);
        indicator.anchorMin = new Vector2(0f, 0f);
        indicator.anchorMax = new Vector2(0f, 1f);
        indicator.sizeDelta = Vector2.zero;

        feedbackText = new GameObject("Feedback", typeof(RectTransform), typeof(Text)).GetComponent<Text>();
        feedbackText.transform.SetParent(transform, false);
        feedbackText.alignment = TextAnchor.MiddleCenter;
        feedbackText.gameObject.SetActive(false);
    }

    private RectTransform CreateZone(string name, RectTransform parent)
    {
        var zone = new GameObject(name, typeof(RectTransform), typeof(Image)).GetComponent<RectTransform>();
        zone.SetParent(parent, false);
        zone.anchorMin = new Vector2(0.5f, 0f);
        zone.anchorMax = new Vector2(0.5f, 1f);
        zone.sizeDelta = Vector2.zero;
        return zone;
    }

    private void Update()
    {
        if (resolved || indicator == null)
        {
            return;
        }

        float t = Mathf.PingPong(Time.time * speed, 1f);
        indicator.anchorMin = new Vector2(t, 0f);
        indicator.anchorMax = new Vector2(t, 1f);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            Evaluate(t);
        }
    }

    private void Evaluate(float pos)
    {
        resolved = true;

        int gain = 1;
        if (pos >= 0.5f - PerfectWidth / 2f && pos <= 0.5f + PerfectWidth / 2f)
        {
            gain = 3;
        }
        else if (pos >= 0.5f - GoodWidth / 2f && pos <= 0.5f + GoodWidth / 2f)
        {
            gain = 2;
        }

        if (gain > 1)
        {
            AudioManager.Instance?.PlaySuccess();
        }
        else
        {
            AudioManager.Instance?.PlayError();
        }

        if (Inventory.Instance != null)
        {
            Inventory.Instance.AddWood(gain);
        }

        if (feedbackText != null)
        {
            feedbackText.text = "+" + gain;
            feedbackText.gameObject.SetActive(true);
        }

        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
