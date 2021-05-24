using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //public Button quitButton;
    //public Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("JJohnson_IndependentProject");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   // void OnClick()
    //{
        //playButton.onClick.AddListener(PlayGame);
        //quitButton.onClick.AddListener(QuitGame);
   // }
}
