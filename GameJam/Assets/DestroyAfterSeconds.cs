using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float seconds = 5;
    public bool fadeOut = true;
    public void Start()
    {
        StartCoroutine(DestroySoon(seconds));
    }

    IEnumerator DestroySoon(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
