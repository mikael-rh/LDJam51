using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyPoolConfig", order = 1)]
public class EnemyPoolConfig : ScriptableObject
{
    [PrefabOnly]
    public GameObject protoype;

    public int poolAmount;
}

