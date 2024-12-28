using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public GameObject uiElement;  // Reference to the UI element you want to hide/show

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event when the object is disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is one of the specified scenes
        if (scene.name == "Arc 1" || scene.name == "Arc 2" || scene.name == "Arc 3" || scene.name == "EndStory" || scene.name == "Main Menu" || scene.name == "MainStory")
        {
            // Enable the UI element if it's one of the scenes
            uiElement.SetActive(false);
        }
        else
        {
            // Disable the UI element if it's not one of the specified scenes
            uiElement.SetActive(true);
        }
    }

    private void Start()
    {
        // Initial check when the game starts, in case the scene is already one of the specified scenes
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
}
