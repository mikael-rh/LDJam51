using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeath : MonoBehaviour
{
    [SerializeField]
    private Behaviour[] disabledBehaviours;
    [SerializeField]
    private GameObject[] disabledObjects;

    [SerializeField]
    private GameObject deathHud;

    private bool hasTriggered = false;

    public void Trigger()
    {
        if (hasTriggered) return;

        foreach (Behaviour behaviour in disabledBehaviours)
        {
            behaviour.enabled = false;
        }

        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(false);
        }

        deathHud.SetActive(true);

        hasTriggered = true;
    }
}
