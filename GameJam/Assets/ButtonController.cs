using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public MeshRenderer[] renderers;

    public Material flashMaterial;
    public Material baseMaterial;

    public bool isFlashing = true;
    public float timer = 0;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        if (isFlashing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                int randomRange = Random.Range(1, 25);
                for (int i = 0; i < 10; i++)
                {
                    int number = Random.Range(0, renderers.Length);
                    StartCoroutine(FlashgRenderers(renderers[number]));
                }
                timer = Random.Range(0.5f, 2);
            }
        }
        else
        {
            Debug.Log("Not Flashing");
        }
    }

    IEnumerator FlashgRenderers(MeshRenderer renderer)
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1));
        int randomFlashRange = Random.Range(1, 5);
        for (int i = 0; i < randomFlashRange; ++i)
        {
            renderer.material = flashMaterial;
            yield return new WaitForSeconds(Random.Range(0.1f, 1));
            renderer.material = baseMaterial;
            yield return new WaitForSeconds(Random.Range(0.1f, 1));
        }

        renderer.material = baseMaterial;

    }
}
