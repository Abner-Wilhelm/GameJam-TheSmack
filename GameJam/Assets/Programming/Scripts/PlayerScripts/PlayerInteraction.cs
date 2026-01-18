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

    public GameObject confettiVFX;

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
                MangledHandController.Instance.Stamp();
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

    //Trying to spawn the sprite as close to the surface as possible without clipping through it
    private void SpawnSprite(Vector3 position, Vector3 forward, int input)
    {
        GameObject spriteToSpawn = input == LEFT_MOUSE_INPUT ? approvedSprite : deniedSprite;
        GameObject spriteInstance = Instantiate(spriteToSpawn, position + forward * 0.01f, Quaternion.LookRotation(-forward));
    }

    internal void SpawnConfetti(Vector3 vector3)
    {
        GameObject confettiInstance = Instantiate(confettiVFX, vector3, Quaternion.identity);
    }
}
