using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleBreakCondition : ScriptableObject
{
    public string ruleName;
    public List<RuleBreakException> possibleExceptions;
}

public enum RuleConditionType
{
    MonsterUnproffesionality,
    UnrealmlyItem,
    BrokenItem,
    UnbalancedItem
}
