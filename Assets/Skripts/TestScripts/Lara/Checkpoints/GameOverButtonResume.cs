using UnityEngine;
using UnityEngine.UI;

public class GameOverButtonResume : MonoBehaviour
{
    [SerializeField] private Button respawnButton;

    private void Start()
    {
        // Set up respawn button listener
        if (respawnButton)
        {
            respawnButton.onClick.AddListener(RespawnAtCheckpoint);
        }
    }

    private void RespawnAtCheckpoint()
    {
        // Call the CheckpointManager to handle respawning
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.RespawnPlayer();
        }
        else
        {
            Debug.LogWarning("CheckpointManager not found. Make sure it's set up correctly in your persistent objects.");
        }
    }
}