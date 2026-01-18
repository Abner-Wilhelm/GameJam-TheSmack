using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RulebreakCondition", menuName = "ScriptableObjs/RuleBreakCondition", order = 1)]
public class RuleBreakCondition : ScriptableObject
{
    public string ruleName;
    public RuleConditionType ruleConditionType;
    public List<RuleBreakException> possibleExceptions;
}

public enum RuleConditionType
{
    MonsterUnproffesionality,
    UnrealmlyItem,
    BrokenItem,
    UnbalancedItem
}
