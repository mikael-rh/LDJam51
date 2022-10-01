using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // HACK: initial state should be in a scriptable object
    [SerializeField]
    private int maxHealth;
    private int health;
    public float HealthFloat { get => (float)health / (float)maxHealth; }

    [SerializeField]
    private float staminaConsumptionRate;
    [SerializeField]
    private float staminaRegenRate;

    private float stamina;
    public float StaminaFloat { get => stamina; }
    private bool staminaDrainedThisFrame;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        stamina = 1;
    }

    private void Update()
    {
        if (staminaDrainedThisFrame == false)
        {
            stamina += Time.deltaTime * staminaRegenRate;
            stamina = Mathf.Clamp(stamina, 0, 1);
        }
        staminaDrainedThisFrame = false;
    }

    /// <summary>
    /// Drain stamina from the player
    /// </summary>
    /// <param name="deltaTime">the frame delta time</param>
    /// <returns>true if the player still is able to use stamina</returns>
    public bool DrainStamina(float deltaTime)
    {
        stamina -= deltaTime * staminaConsumptionRate;
        stamina = Mathf.Clamp(stamina, 0, 1);
        staminaDrainedThisFrame = true;
        return stamina != 0;
    }

    [ContextMenu("Ouch")]
    public void Debug()
    {
        health -= 10;
    }
}
