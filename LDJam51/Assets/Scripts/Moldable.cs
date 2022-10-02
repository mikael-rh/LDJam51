using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moldable : MonoBehaviour
{
    [SerializeField]
    private EnemyState state;

    [SerializeField]
    private int moldDamage;

    // HACK: we have no other particle that can send messages currently
    private void OnParticleCollision(GameObject other)
    {
        state.ApplyDamage(moldDamage);
    }
}
