using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stick : MonoBehaviour
{
    [SerializeField]
    private Rigidbody parentRigidBody;

    [SerializeField]
    private float stickThreshold;

    [SerializeField]
    private Collider[] colliders;


    private void OnTriggerEnter(Collider other)
    {
        // HACK: The actual calculation should be based on the angle between the
        //       velocity and the local up vector for the knife ...
        if (parentRigidBody.velocity.sqrMagnitude >= stickThreshold)
        {
            parentRigidBody.transform.parent = other.transform;
            parentRigidBody.isKinematic = true;
            
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }

    public void Unstuck()
    {
        parentRigidBody.transform.parent = null;
        parentRigidBody.isKinematic = false;

        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
