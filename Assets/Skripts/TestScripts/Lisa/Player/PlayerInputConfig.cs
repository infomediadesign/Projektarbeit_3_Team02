using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player/InputActions")]
public class PlayerInputConfig : ScriptableObject
{
    public InputAction walk;
    public InputAction jump;
    public InputAction block;
    public InputAction roll;
    public InputAction counter;
    public InputAction airCounter;
}
