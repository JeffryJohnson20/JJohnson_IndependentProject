using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    public void EndGame ()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Application.Quit();
            Debug.Log("Application has quit");
        }
    }

    public void Restart ()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            SceneManager.LoadScene("JJohnson_IndependentProject");
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu Screen");
    }
}
