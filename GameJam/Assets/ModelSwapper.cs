using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwapper : MonoBehaviour
{
    public GameObject frame1;
    public GameObject frame2;
    public float timebetweenanims;
    private float timer;
    private bool on = true;

    private void Start()
    {
        timer = timebetweenanims;
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            on = !on;

            frame1.GetComponent<MeshRenderer>().enabled = on;
            frame2.GetComponent<MeshRenderer>().enabled = !on;

            timer = timebetweenanims;
        }
    }


}
