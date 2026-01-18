using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Video;

public class MainMenuManager : MonoBehaviour
{
   public CinemachineVirtualCamera mainMenuCamera;
   public GameObject mainMenuUI;

    public RectTransform tutorialMenuBringDownThing;
    public Vector2 tutorialMenuStartPos;
    public Vector2 tutorialMenuEndPos;

    public ButtonController elevatorButtons;
    public NumberPlate numberPlateController;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public List<RoomInfo> roomsToReset = new List<RoomInfo>();

    public VideoPlayer VideoPlayer;


    private void Start()
    {
        mainMenuCamera.Priority = 20;
        PlayerMovement.Instance.isFrozen = true;
        SoundManager.Instance.musicSource.clip = SoundManager.Instance.elevatorNoises;
        SoundManager.Instance.musicSource.Play();
        ResetRooms();
    }

    private void ResetRooms()
    {
      foreach(RoomInfo room in roomsToReset)
        {
            room.hasBeenAdded = false;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        MinimapEnlarger.Instance.minimapObj.SetActive(true);
        PlayerMovement.Instance.isFrozen = false;
        PlayerCam.Instance.LockCursor();
        mainMenuCamera.Priority = 0;
        mainMenuUI.SetActive(false);

        float elapsedTime = 0f;
        float duration = 2f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            leftDoor.transform.localPosition = new Vector3(Mathf.Lerp(leftDoor.transform.localPosition.x, -1f, t), leftDoor.transform.localPosition.y, leftDoor.transform.localPosition.z);
            rightDoor.transform.localPosition = new Vector3(Mathf.Lerp(rightDoor.transform.localPosition.x, 1f, t), rightDoor.transform.localPosition.y, rightDoor.transform.localPosition.z);
            yield return null;
        }

        VideoPlayer.Play();

        elevatorButtons.isFlashing = false;
        numberPlateController.elevatorStopped = true;

        
    }
}
