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

    public string GetRoomDisplayName()
    {
       return ("Level " + roomLevel + ": " + roomName);
    }

    public void AddRule()
    {
        if (!string.IsNullOrEmpty(ruleToAdd))
        {
            RuleManager.Instance.AddToClipboardRules(ruleToAdd);
            RuleManager.Instance.AddRuleCondition(ruleCondition);
        }
    }
}


