using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public Button playButton;

    // Start is called before the first frame update

    public void OnClick(){

        playButton.gameObject.SetActive(false);
        Invoke("StartGame", 3f);


    }
    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");
    }

}
