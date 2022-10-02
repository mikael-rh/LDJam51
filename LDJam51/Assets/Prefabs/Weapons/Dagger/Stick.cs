using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stick : MonoBehaviour
{
    [SerializeField]
    private Rigidbody parentRigidBody;

    [SerializeField]
    private float stickThreshold;


    private void OnTriggerEnter(Collider other)
    {
        // HACK: The actual calculation should be based on the angle between the
        //       velocity and the local up vector for the knife ...
        if (parentRigidBody.velocity.sqrMagnitude >= stickThreshold)
        {
            parentRigidBody.transform.parent = other.transform;
            parentRigidBody.isKinematic = true;
        }
    }

    public void Unstuck()
    {
        parentRigidBody.transform.parent = null;
        parentRigidBody.isKinematic = false;
    }
}
