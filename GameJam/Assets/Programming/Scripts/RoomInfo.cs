using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomInfo", menuName = "ScriptableObjs/RoomInfo", order = 1)]
public class RoomInfo : ScriptableObject
{
    public string roomName;
    public int roomLevel;
    public string ruleToAdd;

}


