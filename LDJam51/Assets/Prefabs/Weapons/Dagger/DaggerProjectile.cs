using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{

    const int POOL_SIZE = 32;
    private Projectile[] projectilePool;

    [PrefabOnly]
    [SerializeField]
    private GameObject projectilePrototype;

    [SerializeField]
    private float projectileSpeed;

    private int nextFiredProjectile = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(projectilePrototype.GetComponent<Projectile>() != null);

        projectilePrototype.transform.localScale = transform.lossyScale;
        // set projectile scale to the lossy global scale of the dagger to match
        projectilePool = new Projectile[POOL_SIZE];
        for (int i = 0; i < POOL_SIZE; i++)
        {
            projectilePool[i] = Instantiate(projectilePrototype).GetComponent<Projectile>();
            projectilePool[i].gameObject.SetActive(false);
        }
    }

    public void Fire(Vector3 direction)
    {
        Projectile projectile = projectilePool[nextFiredProjectile];
        projectile.gameObject.SetActive(true);
        projectile.Launch(transform.position, transform.rotation, direction, projectileSpeed);
        nextFiredProjectile = (nextFiredProjectile + 1) % POOL_SIZE;
    }
}
