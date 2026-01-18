using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MangledHandController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject hand;

    public static MangledHandController Instance;

    private Vector3 startPosition;
    private Quaternion startRot;

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
        startPosition = hand.transform.localPosition;
        startRot = hand.transform.localRotation;

    }

    private void Update()
    {
        
    }

    public void Stamp()
    {
        StartCoroutine(StampCoroutine());
    }

    IEnumerator StampCoroutine()
    {
        
        Quaternion startRotation = hand.transform.localRotation;
        Quaternion midRotation = Quaternion.Euler(-120f, hand.transform.localRotation.eulerAngles.y, hand.transform.localRotation.eulerAngles.z);
        Quaternion endRotation = Quaternion.Euler(-20f, hand.transform.localRotation.eulerAngles.y, hand.transform.localRotation.eulerAngles.z);
        float duration = 0.1f;
        float elapsedTime = 0f;
        //First half
        while (elapsedTime < duration)
        {
            hand.transform.localRotation = Quaternion.Slerp(startRotation, midRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hand.transform.localRotation = midRotation;
        elapsedTime = 0f;
        //Second half
        while (elapsedTime < duration)
        {
            hand.transform.localRotation = Quaternion.Slerp(midRotation, endRotation, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hand.transform.localRotation = endRotation;
        //return to start
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            hand.transform.localRotation = Quaternion.Slerp(endRotation, startRot, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        hand.transform.localRotation = startRot;
        hand.transform.localPosition = startPosition;
    }

    }
