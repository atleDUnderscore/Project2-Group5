using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        if (col.tag == "Player" && SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Level1");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Level2");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Level3");
        }
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void ToPlay()
    {
        SceneManager.LoadScene("Level1");
    }

     public void ToPlay2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void ToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
