using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("Interlude1");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LauchTuto()
    {
        SceneManager.LoadScene("Tuto");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
