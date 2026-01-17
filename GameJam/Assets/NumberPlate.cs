using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NumberPlate : MonoBehaviour
{
    public TextMeshProUGUI numberPlate;
    public Image upDownSprite;
    public Sprite upArrow, downArrow;

    private float currentFloor = 136;
    private float speed = 1f;
    private float time = 1f;
    private bool goingUp = true;

    public bool elevatorStopped = false;

    private void Update()
    {
        if (numberPlate != null && !elevatorStopped)
        {
            RunFloorNumberSwitcher();
            if (time >= 0f)
            {
                time -= Time.deltaTime;
            }
            else if (time < 0f)
            {
                RandomizeFactors();
            }
        }
    }

    private void RandomizeFactors()
    {
        RandomizeTime();
        RandomizeDirection();
        RandomizeSpeed();

    }

    private void RandomizeSpeed()
    {
        speed = Random.Range(0.5f, 20f);
    }

    private void RandomizeTime()
    {
        time += Random.Range(0.5f, 3f);
    }

    private void RandomizeDirection()
    {
        float rng = Random.Range(0.0f, 1.0f);
        if (rng < 0.5f)
        {
            goingUp = true;
        }
        else
        {
            goingUp = false;
        }
    }

    
    private void RunFloorNumberSwitcher()
    {
        if (goingUp)
        {
            upDownSprite.sprite = upArrow;
            currentFloor += (speed * Time.deltaTime);
        }
        else
        {
            currentFloor -= (speed * Time.deltaTime);
            upDownSprite.sprite = downArrow;
        }

        int currentFloorInt = (int)currentFloor;
        currentFloorInt = Mathf.Clamp(currentFloorInt, -99, 999);
        numberPlate.text = currentFloorInt.ToString();
    }
}
