using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieController : MonoBehaviour
{
    public Button RestartButton; 
    public Button MenuButton; 



    private Image buttonImageRestart; 
    private Image buttonImageMenu; 

    public Sprite restartButtonOnClick; 
    public Sprite menuButtonOnClick; 



    void Start()
    {
        
        if (RestartButton != null)
        {
            buttonImageRestart = RestartButton.GetComponent<Image>();
        }

        if (MenuButton != null)
        {
            buttonImageMenu = MenuButton.GetComponent<Image>();
        }

    }


    public void OnClickRestart(){

        if (buttonImageRestart != null && restartButtonOnClick != null)
        {
            buttonImageRestart.sprite = restartButtonOnClick;
            Invoke("StartGame", 2f);
        }
    }

    public void OnClickMenu(){

        if (buttonImageMenu != null && menuButtonOnClick != null)
        {
            buttonImageMenu.sprite = menuButtonOnClick;

            Invoke("ReturnMenu", 1f);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
