using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RuntimeMaterialInstancer))]
public class RoomEntity : MonoBehaviour, IInteractable
{
    public RuleObject ruleObject;
    public GameObject levelDisplay;

    public Color highlightColor = Color.green;
    public Color notInteractedColor = Color.yellow;
    public Color normalOutline = Color.black;

    public bool hasBeenInteractedWith = false;

    public Room myRoom;

    public void Interact(int inputType)
    {
        if(inputType == PlayerInteraction.LEFT_MOUSE_INPUT)
        {
            hasBeenInteractedWith = true;
            ChangeMaterialOutline();
            if (ruleObject.IsFollowingRules())
            {
                Debug.Log("Confetti! You followed the rules!");
                
            }
            else
            {
                Debug.Log("BOOOOOM");
            }
        }
        else if(inputType == PlayerInteraction.RIGHT_MOUSE_INPUT)
        {
            hasBeenInteractedWith = true;
            ChangeMaterialOutline();
            if (!ruleObject.IsFollowingRules())
            {
                Debug.Log("Confetti! You followed the rules!");
            }
            else
            {
                Debug.Log("BOOOOOM");
            }
        }
    }

    private void ChangeMaterialOutline()
    {
        Material mat = GetComponent<Renderer>().material;

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

    public void isBeingLookedAt(bool isBeingLookedAt)
    {
        if (hasBeenInteractedWith) return;
        Material mat = GetComponent<Renderer>().material;
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
