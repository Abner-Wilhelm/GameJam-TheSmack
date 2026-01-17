using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomInfo", menuName = "ScriptableObjs/RoomInfo", order = 1)]
public class RoomInfo : ScriptableObject
{
    public string roomName;
    public int roomLevel;
    public string ruleToAdd;
    public RuleBreakCondition ruleCondition;
    public RuleBreakException ruleException;

    public string GetRoomDisplayName()
    {
        return roomName;
    }

    public void AddRule()
    {
        if (!string.IsNullOrEmpty(ruleToAdd))
        {
            ClipboardController.Instance.StartCoroutine(ClipboardController.Instance.lookAtClipBoard(ruleToAdd));
            if (ruleCondition) RuleManager.Instance.AddRuleCondition(ruleCondition);
            if(ruleException) RuleManager.Instance.AddRuleException(ruleException);
        }
    }
}


