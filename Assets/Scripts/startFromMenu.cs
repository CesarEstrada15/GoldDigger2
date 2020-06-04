using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startFromMenu : MonoBehaviour
{
    public GameObject controls; 
    // Start is called before the first frame update
    void Start()
    {
        controls.SetActive(false);
    }
    public void startGame()
    {
        controls.SetActive(true);
        StartCoroutine("LoadYourAsyncScene");
    }
    public void startutorial()
    {
        SceneManager.LoadScene(2);
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
}
