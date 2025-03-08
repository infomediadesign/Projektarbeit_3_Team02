using UnityEngine;
using UnityEngine.InputSystem;

public class InputCheck : MonoBehaviour
{
    private PlayerInput playerInput;
    public static bool controller;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            HandleControllerInput();
        }
        else
        {
            HandleKeyboardInput();
        }
    }

    void HandleControllerInput()
    {
        controller = true;
    }

    void HandleKeyboardInput()
    {
        controller = false;
    }
}
