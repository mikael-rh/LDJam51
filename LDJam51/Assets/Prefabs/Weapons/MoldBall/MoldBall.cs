using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldBall : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ps;

    private bool hasEmitted = false;

    private void OnCollisionEnter(Collision collision)
    {
        ps.Play();
        hasEmitted = true;

        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        while (true)
        {
            if (ps.isEmitting == false)
            {
                gameObject.SetActive(false);
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
