using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneChecker : MonoBehaviour
{
    public float checkInterval = 0.5f; // Wie oft geprüft werden soll (in Sekunden)
    private Dictionary<int, List<Scene>> loadedScenes = new Dictionary<int, List<Scene>>();

    void Start()
    {
        StartCoroutine(CheckForDuplicateScenes());
    }

    IEnumerator CheckForDuplicateScenes()
    {
        WaitForSeconds wait = new WaitForSeconds(checkInterval);

        while (true)
        {
            // Dictionary zurücksetzen
            loadedScenes.Clear();

            // Alle geladenen Scenes durchgehen
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                int buildIndex = scene.buildIndex;

                // Scene zur Liste für diesen buildIndex hinzufügen
                if (!loadedScenes.ContainsKey(buildIndex))
                {
                    loadedScenes[buildIndex] = new List<Scene>();
                }
                loadedScenes[buildIndex].Add(scene);
            }

            // Prüfen auf Duplikate
            foreach (var kvp in loadedScenes)
            {
                if (kvp.Value.Count > 1)
                {
                    Debug.LogWarning($"Found {kvp.Value.Count} instances of scene with build index {kvp.Key}!");

                    // Behalte die erste Scene, entlade alle anderen
                    for (int i = 1; i < kvp.Value.Count; i++)
                    {
                        Scene duplicateScene = kvp.Value[i];
                        Debug.Log($"Unloading duplicate scene: {duplicateScene.name} (Build Index: {duplicateScene.buildIndex})");
                        SceneManager.UnloadSceneAsync(duplicateScene);
                    }
                }
            }

            yield return wait;
        }
    }

    // Optional: Debug-Informationen
    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            GUILayout.Label("Loaded Scenes:");

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                GUILayout.Label($"- {scene.name} (Build Index: {scene.buildIndex})");
            }

            GUILayout.EndArea();
        }
    }
}