using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
   public static ScoreManager Instance;

    public List<RoomEntity> allEntities = new List<RoomEntity>();

    public int mistakes = 0;
    public int missedEntities = 0;

    public int mistakeWorth = 100;
    public int missedEntityWorth = 50;

    public int StartingScore = 1000;

    public Image performanceReview;

    public Vector2 reviewPositionOffScreen;
    public Vector2 reviewPositionOnScreen;

    public TextMeshProUGUI entitiesNotInspected;
    public TextMeshProUGUI InaccurateInspections;
    public TextMeshProUGUI finalScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RoomEntity[] entities = FindObjectsOfType<RoomEntity>();
        allEntities.AddRange(entities);
    }

    public void CalculateScore()
    {
        FindMissingEntities();
        int score = StartingScore - (mistakes * mistakeWorth) - (missedEntities * missedEntityWorth);

        entitiesNotInspected.text = $"Entities Not Inspected: {missedEntities}\nPrice per mistake: ${missedEntityWorth}\nHow bad you messed up: ${missedEntities * missedEntityWorth * -1}";
        InaccurateInspections.text = $"Inaccurate Inspections: {mistakes}\nPrice per mistake: ${mistakeWorth}\nHow bad you messed up: ${mistakes * mistakeWorth * -1}";
        finalScore.text = $"Your Earnings: ${score}";

    }

    public void GameEnd()
    {
        CalculateScore();
        StartCoroutine(LerpPerformanceReviewIn());
    }

    IEnumerator LerpPerformanceReviewIn()
    {
        float duration = 3f; // Duration of the lerp effect
        float elapsed = 0.0f;
        Vector2 originalPosition = performanceReview.rectTransform.anchoredPosition;
        Vector2 targetPosition = reviewPositionOnScreen;

        while (elapsed < duration)
        {
            performanceReview.rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        performanceReview.rectTransform.anchoredPosition = targetPosition;
    }



    private void FindMissingEntities()
    {
        foreach (RoomEntity entity in allEntities)
        {
            if (entity == null) continue;
            if (!entity.hasBeenInteractedWith)
            {
                missedEntities++;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEnd();
            Collider thisCollider = GetComponent<Collider>();
            thisCollider.enabled = false;
        }
    }
}

