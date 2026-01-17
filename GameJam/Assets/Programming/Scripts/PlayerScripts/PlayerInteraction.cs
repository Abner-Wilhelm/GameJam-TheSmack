using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable
{
    /// <summary>
    /// Method for item interaction
    /// </summary>
    /// <param name="input">0 is left mouse button, 1 is right mouse button</param>
    public void Interact(int input = PlayerInteraction.LEFT_MOUSE_INPUT);
}
public class PlayerInteraction : MonoBehaviour
{
    public const int LEFT_MOUSE_INPUT = 0;
    public const int RIGHT_MOUSE_INPUT = 1;



    public float interactDistance = 3f;
    /*void Update()
    {
        if (UIManager.Instance.restrictPlayerMovement) return;

        Ray ray = new Ray(transform.position, transform.forward); // on Camera
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {

            if (hit.collider.TryGetComponent(out Food food))
            {

                food.foodCanvas.UpdateFoodDisplay(food.foodData);
                food.foodCanvas.DisplayFoodUI(true);


                if (lastLookedAtFood && lastLookedAtFood != food)
                    lastLookedAtFood.foodCanvas.DisplayFoodUI(false);

                lastLookedAtFood = food;


                if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.TryGetComponent(out IInteractable i0))
                    i0.Interact(PlayerInteraction.LEFT_MOUSE_INPUT);
                else if (Input.GetKeyDown(KeyCode.Mouse1) && hit.collider.TryGetComponent(out IInteractable i1))
                    i1.Interact(PlayerInteraction.RIGHT_MOUSE_INPUT);
            }
            else
            {
              
                if (lastLookedAtFood)
                {
                    lastLookedAtFood.foodCanvas.DisplayFoodUI(false);
                    lastLookedAtFood = null;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider.TryGetComponent(out IInteractable i0))
                    i0.Interact(PlayerInteraction.LEFT_MOUSE_INPUT);
                else if (Input.GetKeyDown(KeyCode.Mouse1) && hit.collider.TryGetComponent(out IInteractable i1))
                    i1.Interact(PlayerInteraction.RIGHT_MOUSE_INPUT);
            }
        }
        else
        {

            if (lastLookedAtFood)
            {
                lastLookedAtFood.foodCanvas.DisplayFoodUI(false);
                lastLookedAtFood = null;
            }
        }
    }*/
}
