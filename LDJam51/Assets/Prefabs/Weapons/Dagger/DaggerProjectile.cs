using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{

    const int POOL_SIZE = 100;
    private Projectile[] projectilePool;

    [SerializeField]
    private float projectileSpeed;

    private int nextFiredProjectile = 0;

    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        projectilePool = new Projectile[POOL_SIZE];
    }

    public void Fire(Vector3 direction)
    {
        Projectile projectile = projectilePool[nextFiredProjectile];
        projectile.Launch(transform.position, cameraTransform.forward, projectileSpeed);
        nextFiredProjectile = (nextFiredProjectile + 1) % POOL_SIZE;
    }
}
