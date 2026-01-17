using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.ProBuilder.MeshOperations;

public class RuleManager : MonoBehaviour
{
    public static RuleManager Instance;
    public TextMeshProUGUI clipboardText;
    private int ruleIndex = 1;
    private AudioSource audioSource;

    private List<RuleBreakCondition> activeRuleConditions = new List<RuleBreakCondition>();
    private List<RuleBreakException> activeRuleExceptions = new List<RuleBreakException>();

    private void Awake()
    {
        
    if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        clipboardText.text = "Rules to follow:\n";
        audioSource = GetComponent<AudioSource>();
    }

    public void AddRuleCondition(RuleBreakCondition ruleCondition)
    {
        activeRuleConditions.Add(ruleCondition);
    }

    public void AddToClipboardRules(string newRule)
    {
        StartCoroutine(AddNewRuleLetterByLetter(newRule));
        ruleIndex++;
    }

    IEnumerator AddNewRuleLetterByLetter(string newRule)
    {
        audioSource.Play();
        string ruleToAdd = ruleIndex.ToString() + ". " + newRule + "\n";
        foreach(char letter in ruleToAdd.ToCharArray())
        {
            clipboardText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    //Returns true if all conditions are met, false if any condition is broken
    internal bool CompareRuleConditions(List<RuleBreakCondition> ruleConditions)
    {
       if(ruleConditions == null || ruleConditions.Count == 0)
        {
            return true;
        }
        foreach(var condition in ruleConditions)
        {
            foreach(var exception in condition.possibleExceptions)
            {
                if(activeRuleConditions.Contains(condition) && !activeRuleExceptions.Contains(exception))
                {
                    return false;
                }
            }
            
        }
        return true;
    }

    internal void AddRuleException(RuleBreakException ruleException)
    {
        activeRuleExceptions.Add(ruleException);
    }
}
