using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hover an object its initial position
/// </summary>
public class Hover : MonoBehaviour
{
    [SerializeField]
    private Vector3 floatOffset;
    [SerializeField]
    private float floatRate;

    private float floatT = 0;
    private float floatDir = 1;
    private Vector3 fromPostion;
    private Vector3 toPosition;

    private void Start()
    {
        fromPostion = transform.position;
        toPosition = fromPostion + floatOffset;
    }

    void FixedUpdate()
    {
        floatT += floatDir * floatRate * Time.fixedDeltaTime;
        floatT = Mathf.Clamp(floatT, 0, 1);
        transform.position = Vector3.Slerp(fromPostion, toPosition, floatT);

        if (floatT == 0 || floatT == 1)
        {
            floatDir *= -1;
        }
    }
}
