using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if(targetTransform == null)
        {
            return;
        }

        transform.position = targetTransform.position + offset;
        transform.LookAt(targetTransform);
    }
}
