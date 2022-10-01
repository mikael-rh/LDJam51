using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class EnemySpawner : MonoBehaviour
{
    [PrefabOnly]
    public NavMeshAgent sampleEnemy; // TODO: Allow for different enemies

    [SerializeField] 
    private Camera playerCamera;

    [SerializeField]
    private float minDistanceFromPlayer;

    [SerializeField]
    Bounds levelBounds; // level bounds in worldspace

    //  TODO:  Use triangulation to spawn on mesh?
    // Requires balancing based on triangle area etc. Relatively cheap to compute, 
    // but requires a lot of boilerplate
    
    //private void Awake()
    //{
    //    //NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
    //}

    public void SpawnEnemies(int count)
    {
        NavMeshHit hit;
        Vector3 min = levelBounds.min;
        Vector3 max = levelBounds.max;
        int spawned = 0;
        while (spawned < count)
        {
            Vector3 p = new Vector3(
                Random.Range(min.x, max.x), 
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
            );
            if (NavMesh.SamplePosition(p, out hit, 1.0f, NavMesh.AllAreas))
            {
                // TODO: Enemy Pooling?
                Instantiate(sampleEnemy, hit.position + Vector3.up * sampleEnemy.baseOffset, Quaternion.identity);

                spawned++;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(levelBounds.center, levelBounds.size);
    }

    [ContextMenu("Spawn 5")]
    private void Spawn5()
    {
        SpawnEnemies(5);
    }
}