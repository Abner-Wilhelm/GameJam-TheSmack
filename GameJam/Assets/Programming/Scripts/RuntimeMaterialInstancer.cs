using System.Collections.Generic;
using UnityEngine;

public class RuntimeMaterialInstancer : MonoBehaviour
{
    [Tooltip("If true, also instantiates materials for all child Renderers.")]
    public bool includeChildren = true;

    private readonly List<Material> _runtimeMats = new List<Material>();
    private Renderer[] _renderers;

    private void Awake()
    {
        _renderers = includeChildren ? GetComponentsInChildren<Renderer>(true) : GetComponents<Renderer>();
        InstantiatePerRendererMaterials();
    }

    private void InstantiatePerRendererMaterials()
    {
        _runtimeMats.Clear();

        foreach (var r in _renderers)
        {
            if (r == null) continue;

            var mats = r.materials;

            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i] != null)
                    _runtimeMats.Add(mats[i]);
            }

            r.materials = mats;
        }
    }

    private void OnDisable()
    {
        Cleanup();
    }

    private void OnDestroy()
    {
        Cleanup();
    }

    private void Cleanup()
    {
        for (int i = 0; i < _runtimeMats.Count; i++)
        {
            if (_runtimeMats[i] != null)
                Destroy(_runtimeMats[i]);
        }

        _runtimeMats.Clear();
    }
}
