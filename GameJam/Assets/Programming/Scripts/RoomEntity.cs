using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntity : MonoBehaviour, IInteractable
{
    public RuleObject ruleObject;
    public GameObject levelDisplay;

    public void Interact(int inputType)
    {
        if(inputType == PlayerInteraction.LEFT_MOUSE_INPUT)
        {
            if(ruleObject.ruleType == RuleObject.RuleType.FollowingRules)
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
            if (ruleObject.ruleType == RuleObject.RuleType.BreakingRules)
            {
                Debug.Log("Confetti! You followed the rules!");
            }
            else
            {
                Debug.Log("BOOOOOM");
            }
        }
    }

    private void Start()
    {
        levelDisplay.SetActive(false);
    }

    public void ShowLevelDisplay(bool show)
    {
        levelDisplay.SetActive(show);
    }
}
