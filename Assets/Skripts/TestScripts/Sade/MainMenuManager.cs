using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main menu Objects")]
    [SerializeField] private GameObject _hideObjects;
   
    [Header("Scenes to Load")]
    [SerializeField] private string _persistentGameplay = "PersistentGameplay";
    [SerializeField] private string _levelScene = "Level1";

    private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();
   
    public void StartGame()
    {
            //Hide button and text
            HideMenu();

            //Start loading scenes 
            _scenesToLoad.Add(SceneManager.LoadSceneAsync(_persistentGameplay)); //Async: loads Scenes in background
            _scenesToLoad.Add(SceneManager.LoadSceneAsync(_levelScene, LoadSceneMode.Additive));   //Additive: allows loading multiple scenes

            //

    }

   private void HideMenu()
   {
        /*for (int i = 0; i < _hideObjects.Length; i++)
        {
            _hideObjects[i].SetActive(false);
        }*/
   }
}
