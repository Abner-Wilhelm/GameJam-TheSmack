using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public GameObject clipboard;
    public Transform clipboardIdle;
    public Transform clipboardLookAt;

    private Rigidbody rb;
    private void Start()
    {
                rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        //Head bobbing effect
        if (rb != null)
        {
            if(rb.velocity.magnitude > 0.1f)
            {
                float bobbingAmount = 0.005f;
                float bobbingSpeed = 7f;
                clipboard.transform.position += new Vector3(0, Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmount, 0);
            }
        }


        if (Input.GetKey(KeyCode.Tab))
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
