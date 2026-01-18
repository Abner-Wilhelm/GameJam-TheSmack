using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapEnlarger : MonoBehaviour
{
    public RectTransform miniMap;

    private Vector3 minimapStartingScale;
    public Vector3 enlargedScale;

    public Vector2 miniMapStartPos;
    public Vector2 miniMapEndPos;

    public Camera minimapCamera;
    private float originalOrthographicSize;
    public float enlargedOrthographicSize = 30f;

    public GameObject minimapObj;

    public static MinimapEnlarger Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Range(0.01f, 1f)]
    public float lerpSpeed = 0.1f;

    private void Start()
    {
        minimapStartingScale = miniMap.localScale;
        originalOrthographicSize = minimapCamera.orthographicSize;
        minimapObj.SetActive(false);
    }

    private void Update()
    {
        if(!PlayerInteraction.Instance.canTab) return;
        bool enlarge = Input.GetKey(KeyCode.Tab);

        Vector2 targetPos = enlarge ? miniMapEndPos : miniMapStartPos;
        Vector3 targetScale = enlarge ? enlargedScale : minimapStartingScale;
        float targetOrthographicSize = enlarge ? enlargedOrthographicSize : originalOrthographicSize;

        miniMap.anchoredPosition = Vector2.Lerp(miniMap.anchoredPosition, targetPos, lerpSpeed);
        miniMap.localScale = Vector3.Lerp(miniMap.localScale, targetScale, lerpSpeed);
        minimapCamera.orthographicSize = Mathf.Lerp(minimapCamera.orthographicSize, targetOrthographicSize, lerpSpeed);
    }
}
