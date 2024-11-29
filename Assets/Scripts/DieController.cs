using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieController : MonoBehaviour
{

    public void OnClickRestart(){

        Invoke("StartGame", 2f);
    }

    public void OnClickMenu(){

        Invoke("ReturnMenu", 1f);
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
