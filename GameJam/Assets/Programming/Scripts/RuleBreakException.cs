using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleBreakException", menuName = "ScriptableObjs/RuleBreakException", order = 3)]
public class RuleBreakException : ScriptableObject
{
    public string exceptionName;
    public RuleExceptionType exceptionType;
}

public enum RuleExceptionType
{
    ALBofFive,
    WednesdayOnly,
}
