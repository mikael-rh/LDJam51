using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncViews : MonoBehaviour
{
    [SerializeField]
    PlayerState playerState;

    [SerializeField]
    Scrollbar healthBar;
    [SerializeField]
    Scrollbar staminaBar;
    [SerializeField]
    Image intervalBar;

    [SerializeField]
    GameObject goalIndicator;
    Renderer goalIndicatorRenderer;
    [SerializeField]
    GameObject uiGoalRightIndicator;
    [SerializeField]
    GameObject uiGoalLeftIndicator;

    private GameInterval gameInterval;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(playerState != null);
        Debug.Assert(healthBar != null);
        Debug.Assert(staminaBar != null);
        Debug.Assert(intervalBar != null);
        Debug.Assert(goalIndicator != null);

        goalIndicatorRenderer = goalIndicator.GetComponentInChildren<Renderer>();
        gameInterval = FindObjectOfType<GameInterval>();
    }

    // Update is called once per frame
    void Update()
    {
        // HACK: forcing sync each frame for each UI element is bad. Should utilize the setter to update all values when needed ...
        healthBar.size = playerState.HealthFloat;
        staminaBar.size = playerState.StaminaFloat;
        intervalBar.fillAmount = gameInterval.Progress;

        if (playerState.LookingForGoal)
        {
            goalIndicator.SetActive(true);

            Vector3 goalDir2D = goalIndicator.transform.position - transform.position;
            goalDir2D.y = 0;
            goalDir2D.Normalize();

            float left = Vector3.Angle(-transform.right, goalDir2D);
            float right = Vector3.Angle(transform.right, goalDir2D);
            bool goalIndicate = goalIndicatorRenderer.isVisible == false;
            uiGoalLeftIndicator.SetActive(goalIndicate && (left < right));
            uiGoalRightIndicator.SetActive(goalIndicate && (right <= left));
        } else
        {
            uiGoalLeftIndicator.SetActive(false);
            uiGoalRightIndicator.SetActive(false);
            goalIndicator.SetActive(false);
        }
    }
}
