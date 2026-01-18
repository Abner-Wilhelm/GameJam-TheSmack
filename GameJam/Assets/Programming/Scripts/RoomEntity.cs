using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RuntimeMaterialInstancer))]
public class RoomEntity : MonoBehaviour, IInteractable
{
    public List<RuleBreakCondition> ruleObject = new List<RuleBreakCondition>();
    public GameObject levelDisplay;

    public Color highlightColor = Color.green;
    public Color notInteractedColor = Color.yellow;
    public Color normalOutline = Color.black;

    public bool hasBeenInteractedWith = false;

    public Room myRoom;

    public bool overrideMaterial = false;
    public int overrideMaterialIndex = -1;

    public virtual void Choice(bool isCorrectChoice)
    {
        if (isCorrectChoice)
        {
            Debug.Log("Confetti! You followed the rules!");
        }
        else
        {
            ScoreManager.Instance.mistakes++;
        }
    }

    public void Interact(int inputType)
    {
        if (hasBeenInteractedWith) return;
        if (inputType == PlayerInteraction.LEFT_MOUSE_INPUT)
        {
            hasBeenInteractedWith = true;
            ChangeMaterialOutline();
            if (RuleManager.Instance.CompareRuleConditions(ruleObject))
            {
                Choice(true);
                
            }
            else
            {
                Choice(false);
            }
        }
        else if(inputType == PlayerInteraction.RIGHT_MOUSE_INPUT)
        {
            hasBeenInteractedWith = true;
            ChangeMaterialOutline();
            if (!RuleManager.Instance.CompareRuleConditions(ruleObject))
            {
                Choice(true);
            }
            else
            {
                Choice(false);
            }
        }
    }

    public bool FollowingAllRules()
    {
        RuleManager.Instance.CompareRuleConditions(ruleObject);
        return true;
    }

    public void ChangeMaterialOutline()
    {
        Material mat;
        if (overrideMaterial)
        {
            mat = GetComponent<Renderer>().materials[overrideMaterialIndex];
        }
        else
        {
            mat = GetComponent<Renderer>().material;
        }

        if(hasBeenInteractedWith)
        {
            mat.SetColor("_OutlineColor", normalOutline);
        }
        else
        {
            mat.SetColor("_OutlineColor", notInteractedColor);
        }

        myRoom?.IsCleared();
    }

    public virtual void isBeingLookedAt(bool isBeingLookedAt)
    {
        if (hasBeenInteractedWith) return;
        Material mat;
        if (!overrideMaterial)
        {
            mat = GetComponent<Renderer>().material;
        }
        else
        {
            mat = GetComponent<Renderer>().materials[overrideMaterialIndex];
        }
        if (isBeingLookedAt && mat != null)
        {
            mat.SetColor("_OutlineColor", highlightColor);
        }
        else
        {
            ChangeMaterialOutline();
        }
    }

    private void Start()
    {
       if(levelDisplay) levelDisplay.SetActive(false);
        ChangeMaterialOutline();
    }

    public void ShowLevelDisplay(bool show)
    {
        if(levelDisplay) levelDisplay.SetActive(show);
    }

    internal void Initialize(Room room)
    {
        myRoom = room;
    }
}
