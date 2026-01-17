using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class PlayerInteraction : MonoBehaviour
{
    public const int LEFT_MOUSE_INPUT = 0;
    public const int RIGHT_MOUSE_INPUT = 1;

    public RoomEntity lastLookedAtObj;

    public GameObject approvedSprite;
    public GameObject deniedSprite;

    public float interactDistance = 3f;
    void Update()
    {


        Ray ray = new Ray(transform.position, transform.forward); // on Camera
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {

            if (hit.collider.TryGetComponent(out RoomEntity entity))
            {
                entity.ShowLevelDisplay(true);

                if (lastLookedAtObj && lastLookedAtObj != entity)
                    lastLookedAtObj.ShowLevelDisplay(false);

                lastLookedAtObj = entity;

                if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.TryGetComponent(out IInteractable i0))
                {
                    if (!entity.hasBeenInteractedWith)
                        SpawnSprite(this.transform.position, this.transform.forward, PlayerInteraction.LEFT_MOUSE_INPUT);
                    i0.Interact(PlayerInteraction.LEFT_MOUSE_INPUT);

                }
                else if (Input.GetKeyDown(KeyCode.Mouse1) && hit.collider.TryGetComponent(out IInteractable i1))
                {
                    if (!entity.hasBeenInteractedWith)
                        SpawnSprite(this.transform.position, this.transform.forward, PlayerInteraction.RIGHT_MOUSE_INPUT);
                    i1.Interact(PlayerInteraction.RIGHT_MOUSE_INPUT);

                }

            }
            else
            {

                if (lastLookedAtObj)
                {
                    lastLookedAtObj.ShowLevelDisplay(false);
                    lastLookedAtObj = null;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.TryGetComponent(out IInteractable i0))
                {
                    if (!entity.hasBeenInteractedWith)
                        SpawnSprite(this.transform.position, this.transform.forward, PlayerInteraction.LEFT_MOUSE_INPUT);
                    i0.Interact(PlayerInteraction.LEFT_MOUSE_INPUT);
                    
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1) && hit.collider.TryGetComponent(out IInteractable i1))
                {
                    if (!entity.hasBeenInteractedWith)
                        SpawnSprite(this.transform.position, this.transform.forward, PlayerInteraction.RIGHT_MOUSE_INPUT);
                    i1.Interact(PlayerInteraction.RIGHT_MOUSE_INPUT);
                   
                }
            }
        }
        else
        {
            if (lastLookedAtObj)
            {
                lastLookedAtObj.ShowLevelDisplay(false);
                lastLookedAtObj = null;
            }
        }
    }

    private void SpawnSprite(Vector3 position, Vector3 forward, int input)
    {
        GameObject spriteToSpawn = input == PlayerInteraction.LEFT_MOUSE_INPUT ? approvedSprite : deniedSprite;
        Instantiate(spriteToSpawn, position + forward * 1.5f, Quaternion.LookRotation(forward));
    }
}
