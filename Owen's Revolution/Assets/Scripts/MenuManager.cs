using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void MoveToScene()
    {
        Debug.Log("change");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MoveToScene2()
    {
        Debug.Log("change3");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void MoveToRestart(int sceneNumber)
    {
        Debug.Log("change2");
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
