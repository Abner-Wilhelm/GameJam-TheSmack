using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleObject", menuName = "ScriptableObjs/RuleObject", order = 1)]
public class RuleObject : ScriptableObject
{
    public bool IsFollowingRules()
    {
        return RuleManager.Instance.CompareRuleConditions(ruleConditions); //Returns true if none of the ruleConditions are in activeRuleConditions
    }
    public List<RuleBreakCondition> ruleConditions;
}

/*public enum RuleBreakCondition
{
    UnprofessionalBehavior,
    UnrealmlyItem,
    BrokenItem,
    NotProperLevel
}
*/

