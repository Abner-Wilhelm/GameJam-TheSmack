using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public GameObject clipboard;
    public Transform clipboardIdle;
    public Transform clipboardLookAt;
    private void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            clipboard.transform.position = Vector3.Lerp(clipboard.transform.position, clipboardLookAt.position, 0.05f);
            clipboard.transform.rotation = Quaternion.Slerp(clipboard.transform.rotation, clipboardLookAt.rotation, 0.05f);
        }
        else
        {
            clipboard.transform.position = Vector3.Lerp(clipboard.transform.position, clipboardIdle.position, 0.05f);
            clipboard.transform.rotation = Quaternion.Slerp(clipboard.transform.rotation, clipboardIdle.rotation, 0.05f);
        }
    }
}
