using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRumble : MonoBehaviour
{
    public bool isRumbling = true;
    void Update()
    {
        if (isRumbling)
        {
            //rumbling as if on an elevator
            transform.localPosition = new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), transform.position.z);
        }
    }
}
