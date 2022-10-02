using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    [SerializeField]
    int[] spawnableEnemyIndices;

    [SerializeField]
    EnemyPoolConfig[] poolingConfigs;
    GameObject[][] pools;
    int[] nextInstance;

    [SerializeField]
    private Transform enemyTarget;

    public int spawnAmount = 1;
	public EnemySpawner spawner;
	public bool spawnOnStart;

	private void Start() {

        {
            nextInstance = new int[poolingConfigs.Length];
            pools = new GameObject[poolingConfigs.Length][];

            for (int i = 0; i < pools.Length; i++)
            {
                EnemyPoolConfig config = poolingConfigs[i];
                pools[i] = new GameObject[config.poolAmount];
                for (int j = 0; j < config.poolAmount; j++)
                {
                    pools[i][j] = Instantiate(config.protoype);
                    pools[i][j].GetComponent<Enemy>().target = enemyTarget;
                    pools[i][j].SetActive(false);
                }
            }
        }

		FindObjectOfType<GameInterval>().PerformMany(Spawn);
		if (spawnOnStart) Spawn();
	}

	private void Spawn() {
		for (int i = 0; i < spawnAmount; i++) {
            int enemyTypeIndex = spawnableEnemyIndices[Random.Range(0, spawnableEnemyIndices.Length)];
            GameObject[] pool = pools[enemyTypeIndex];
            GameObject instance = pool[nextInstance[enemyTypeIndex]];
            nextInstance[enemyTypeIndex] = (nextInstance[enemyTypeIndex] + 1) % pool.Length;

            // TODO: fix this
            if (enemyTarget != null)
            {
                bool success = spawner.SpawnEnemyAtRandom(instance, enemyTarget.position);
                if (!success)
                {
                    Debug.Log($"Failed to spawn {instance.name}");
                }
            } else
            {
                Debug.Log("hit annoying bug...");
            }
        }
	}

    public void ManualSpawn(EnemyPoolConfig type, Vector3 position, Quaternion rotation)
    {
        int i = 0;
        foreach(EnemyPoolConfig config in poolingConfigs)
        {
            if (config == type)
            {
                int enemyTypeIndex = i;
                GameObject[] pool = pools[enemyTypeIndex];
                GameObject instance = pool[nextInstance[enemyTypeIndex]];
                nextInstance[enemyTypeIndex] = (nextInstance[enemyTypeIndex] + 1) % pool.Length;

                instance.SetActive(true);
                instance.transform.position = position;
                instance.transform.rotation = rotation;
            }
            i += 1;
        }
    }
}
