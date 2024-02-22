using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
