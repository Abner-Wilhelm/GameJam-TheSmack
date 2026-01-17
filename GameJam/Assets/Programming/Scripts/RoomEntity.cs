using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RuntimeMaterialInstancer))]
public class RoomEntity : MonoBehaviour, IInteractable
{
    public RuleObject ruleObject;
    public GameObject levelDisplay;

    public Color highlightColor = Color.yellow;
    public Color interactedColor = Color.black;

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
            mat.SetColor("_OutlineColor", interactedColor);
        }
        else
        {
            mat.SetColor("_OutlineColor", highlightColor);
        }

        myRoom?.IsCleared();
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
