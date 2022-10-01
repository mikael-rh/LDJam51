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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(playerState != null);
        Debug.Assert(healthBar != null);
        Debug.Assert(staminaBar != null);
    }

    // Update is called once per frame
    void Update()
    {
        // HACK: forcing sync each frame for each UI element is bad. Should utilize the setter to update all values when needed ...
        healthBar.size = playerState.HealthFloat;
        staminaBar.size = playerState.StaminaFloat;
    }
}
