using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public MeshRenderer[] renderers;

    public Material flashMaterial;
    public Material baseMaterial;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }


}
