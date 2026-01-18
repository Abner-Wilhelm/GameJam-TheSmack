using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomTitleCard : MonoBehaviour
{
    public static RoomTitleCard Instance;
    public GameObject titleCard;
    public GameObject ALBText;

    public Vector2 titleCardCenterPosition;
    public Vector3 titleCardCenterScale;

    public Vector2 titleCardIdlePosition;
    public Vector3 titleCardIdleScale;

    public Vector2 titleCardTabPosition;

    private bool coroutineRunning = false;

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
        titleCard.SetActive(false);
        ALBText.SetActive(false);
    }

    private void Update()
    {
        if(!PlayerInteraction.Instance.canTab) return;
        if (Input.GetKey(KeyCode.Tab))
        {
            RectTransform rt = titleCard.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, titleCardTabPosition, 0.1f);
        }
        else
        {
            RectTransform rt = titleCard.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, titleCardIdlePosition, 0.1f);
        }
    }

    public void ShowTitleCard(RoomInfo roomInfo)
    {
        StartCoroutine(DisplayTitleCard(roomInfo));
    }

    //First displaythe center of the title card with the room name, then move it to the idle position
    IEnumerator DisplayTitleCard(RoomInfo roomInfo)
    {
        PlayerInteraction.Instance.canTab = false;
        titleCard.SetActive(true);
        ALBText.SetActive(false);
        titleCard.GetComponentInChildren<TextMeshProUGUI>().text = roomInfo.GetRoomDisplayName();



        RectTransform rt = titleCard.GetComponent<RectTransform>();

                rt.anchoredPosition = titleCardCenterPosition;
        rt.localScale = titleCardCenterScale;

        float elapsedTime = 0f;
        float moveDuration = 1.5f;
      


        //shake text effect
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            rt.anchoredPosition += new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, titleCardIdlePosition, (elapsedTime / moveDuration));
            rt.localScale = Vector3.Lerp(rt.localScale, titleCardIdleScale, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ALBText.SetActive(true);
        ALBText.GetComponent<TextMeshProUGUI>().text = "ALB: lvl. " + roomInfo.roomLevel.ToString();
        ALBText.GetComponent<TextMeshProUGUI>().color = Color.clear;
        //fade in ALB text
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            ALBText.GetComponent<TextMeshProUGUI>().color = Color.Lerp(ALBText.GetComponent<TextMeshProUGUI>().color, Color.white, elapsedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        PlayerInteraction.Instance.canTab = true;
    }
}
