using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    private void Start()
    {
        mainMenuCamera.Priority = 20;
        PlayerMovement.Instance.isFrozen = true;
        SoundManager.Instance.musicSource.clip = SoundManager.Instance.elevatorNoises;
        SoundManager.Instance.musicSource.Play();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
       

        yield return null;
    }
}
