using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleObject", menuName = "ScriptableObjs/RuleObject", order = 1)]
public class RuleObject : ScriptableObject
{
    public enum RuleType
    {
        FollowingRules,
        BreakingRules
    }

    public RuleType ruleType;
}
