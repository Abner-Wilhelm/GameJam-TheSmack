using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : MonoBehaviour
{
    public static ClipboardController Instance;

    public GameObject clipboard;
    public Transform clipboardIdle;
    public Transform clipboardLookAt;

    private bool coroutineRunning = false;

    private Rigidbody rb;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
                rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (coroutineRunning) return;
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

    public IEnumerator lookAtClipBoard(string ruletoAdd)
    {
        coroutineRunning = true;
        float elapsedTime = 0f;
        float duration = 0.75f;
        Vector3 startingPos = clipboard.transform.position;
        Quaternion startingRot = clipboard.transform.rotation;
        while (elapsedTime < duration)
        {
            clipboard.transform.position = Vector3.Lerp(clipboard.transform.position, clipboardLookAt.position, (elapsedTime / duration));
            clipboard.transform.rotation = Quaternion.Slerp(clipboard.transform.rotation, clipboardLookAt.rotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        clipboard.transform.position = clipboardLookAt.position;
        clipboard.transform.rotation = clipboardLookAt.rotation;

        RuleManager.Instance.AddToClipboardRules(ruletoAdd);
        yield return new WaitForSeconds(2f);
        StartCoroutine(returnToIdle());
    }

    IEnumerator returnToIdle()
    {
        float elapsedTime = 0f;
        float duration = 0.75f;
        Vector3 startingPos = clipboard.transform.position;
        Quaternion startingRot = clipboard.transform.rotation;
        while (elapsedTime < duration)
        {
            clipboard.transform.position = Vector3.Lerp(clipboard.transform.position, clipboardIdle.position, (elapsedTime / duration));
            clipboard.transform.rotation = Quaternion.Slerp(clipboard.transform.rotation, clipboardIdle.rotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        clipboard.transform.position = clipboardIdle.position;
        clipboard.transform.rotation = clipboardIdle.rotation;
        coroutineRunning = false;
    }
}
