using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomEntity[] roomEntities;

    private void Start()
    {
        foreach (var entity in roomEntities)
        {
            if (entity != null)
            {
                entity.Initialize(this);
            }
        }
    }

    public bool IsCleared()
    {
        foreach (var entity in roomEntities)
        {
            if (entity != null && !entity.hasBeenInteractedWith)
            {
                return false;
            }
        }
        return true;
    }
}
