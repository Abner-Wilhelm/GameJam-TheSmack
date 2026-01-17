using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAbovePlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 20, 0);

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }
    }
}
