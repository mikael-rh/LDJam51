using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    private bool lookingForGoal;
    public bool LookingForGoal { get => lookingForGoal; set => lookingForGoal = value; }

    [SerializeField]
    private OnPlayerDeath onDeath;

    [SerializeField]
    private int equippedWeaponIndex;
    public int EquippedWeaponIndex { set {
            weapons[equippedWeaponIndex].gameObject.SetActive(false);
            equippedWeaponIndex = value;
            weapons[equippedWeaponIndex].gameObject.SetActive(true);
        }
    }

    [SerializeField]
    private Weapon[] weapons;
    public Weapon EquippedWeapon { get => weapons[equippedWeaponIndex]; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(equippedWeaponIndex < weapons.Length);
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == equippedWeaponIndex);
        } 

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

    public void ApplyDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            onDeath.Trigger();
        }
    }

#if DEBUG
    [ContextMenu("Ouch")]
    public void Ouch()
    {
        ApplyDamage(100);
    }
#endif // DEBUG

}
