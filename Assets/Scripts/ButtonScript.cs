using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameManager gameManager;
    public Button playAgainButton;
    // Start is called before the first frame update
    void Start()
    {
        playAgainButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TaskOnClick()
    {
        Debug.Log("Button Clicked");
    }
}
