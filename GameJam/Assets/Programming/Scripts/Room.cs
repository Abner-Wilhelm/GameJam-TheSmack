using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomEntity[] roomEntities;
    public RoomInfo roomInfo;

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

    private void OnTriggerEnter(Collider other)
    {
        RoomTitleCard.Instance.ShowTitleCard(roomInfo.GetRoomDisplayName());
        roomInfo.AddRule();
        GetComponent<BoxCollider>().enabled = false;
    }

}
