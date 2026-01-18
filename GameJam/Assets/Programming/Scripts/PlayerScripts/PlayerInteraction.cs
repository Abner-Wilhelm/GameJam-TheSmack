using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInteraction : MonoBehaviour
{
    public const int LEFT_MOUSE_INPUT = 0;
    public const int RIGHT_MOUSE_INPUT = 1;

    public IInteractable lastLookedAtObj;

    public GameObject approvedSprite;
    public GameObject deniedSprite;

    public static PlayerInteraction Instance;
    public bool canTab = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float interactDistance = 3f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); // on Camera
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (lastLookedAtObj != null && lastLookedAtObj != interactable)
                {
                    // Reset the previous object if it's a RoomEntity
                    if (lastLookedAtObj is RoomEntity lastRoomEntity)
                    {
                        lastRoomEntity.ShowLevelDisplay(false);
                        lastRoomEntity.isBeingLookedAt(false);
                    }
                }

                // Update the current object
                if (interactable is RoomEntity roomEntity)
                {
                    roomEntity.ShowLevelDisplay(true);
                    roomEntity.isBeingLookedAt(true);
                }

                lastLookedAtObj = interactable;

                // Handle interaction input
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    HandleInteraction(interactable, LEFT_MOUSE_INPUT, hit);
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    HandleInteraction(interactable, RIGHT_MOUSE_INPUT, hit);
                }
            }
            else
            {
                ResetLastLookedAtObj();
            }
        }
        else
        {
            ResetLastLookedAtObj();
        }
    }

    private void HandleInteraction(IInteractable interactable, int inputType, RaycastHit hit)
    {
        if (interactable is RoomEntity roomEntity)
        {
            if (!roomEntity.hasBeenInteractedWith)
            {
                SpawnSprite(hit.point, hit.normal, inputType);
            }
        }

        interactable.Interact(inputType);
    }

    private void ResetLastLookedAtObj()
    {
        if (lastLookedAtObj != null && lastLookedAtObj is RoomEntity lastRoomEntity)
        {
            lastRoomEntity.ShowLevelDisplay(false);
            lastRoomEntity.isBeingLookedAt(false);
        }

        lastLookedAtObj = null;
    }

    private void SpawnSprite(Vector3 position, Vector3 forward, int input)
    {
        GameObject spriteToSpawn = input == LEFT_MOUSE_INPUT ? approvedSprite : deniedSprite;
        Instantiate(spriteToSpawn, position + forward * 1.5f, Quaternion.LookRotation(forward));
    }
}
