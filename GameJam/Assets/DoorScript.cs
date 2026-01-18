using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class DoorScript : MonoBehaviour, IInteractable
{
    [Header("Teleport Settings")]
    public Transform teleportTransform; // The target location to teleport the player to

    [Header("Fade Settings")]
    public bool useFadeTransition = true; // Enable or disable fade transitions
    public float fadeDuration = 0.5f; // Duration of the fade effect

    private bool isTeleporting = false; // Prevent multiple teleportations
    private PlayerMovement playerMovement; // Reference to the player's movement script
    private PlayerCam playerCam; // Reference to the player's camera script

    public RoomInfo targetRoomInfo;

    private void Start()
    {
        // Cache references to the player components
        playerMovement = PlayerMovement.Instance;
        playerCam = FindObjectOfType<PlayerCam>();

        if (teleportTransform == null)
        {
            Debug.LogError("Teleport Transform is not assigned on DoorScript!");
        }
    }

    public void Interact(int inputType)
    {
        if (!isTeleporting && teleportTransform != null)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private IEnumerator TeleportPlayer()
    {
        isTeleporting = true;

        FadeToBlackManager.Instance.FadeToBlack(true, 0.5f);
        MinimapEnlarger.Instance.minimapObj.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        SoundManager.Instance.sfxSource.PlayOneShot(SoundManager.Instance.transitionSound);
        TransitionCutscene.Instance.PlayCutscene();
        playerCam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        FadeToBlackManager.Instance.FadeToBlack(false, 0.5f);
       
      

        // Freeze the player and teleport
        if (playerMovement != null)
        {
            playerMovement.isFrozen = true; // Prevent player movement
            Rigidbody playerRb = playerMovement.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.isKinematic = true; // Disable physics
            }

            yield return new WaitForSeconds(0.1f); // Small delay to ensure freeze takes effect

            Debug.Log($"Teleporting player to: {teleportTransform.position}");
            playerMovement.transform.position = teleportTransform.position;

            yield return new WaitForSeconds(0.1f); // Small delay to ensure freeze takes effect

            if (playerRb != null)
            {
                playerRb.isKinematic = false; // Re-enable physics
            }
            playerMovement.isFrozen = false; // Allow player movement
        }

        yield return new WaitForSeconds(2.7f);

        FadeToBlackManager.Instance.FadeToBlack(true, 0.3f);
        yield return new WaitForSeconds(0.5f);

        playerCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;

        // Optional fade back in
        if (useFadeTransition && FadeToBlackManager.Instance != null)
        {
            FadeToBlackManager.Instance.FadeToBlack(false, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            MinimapEnlarger.Instance.minimapObj.SetActive(true);
        }

        RoomTitleCard.Instance.ShowTitleCard(targetRoomInfo);
        targetRoomInfo.AddRule();

        isTeleporting = false;
    }
}

