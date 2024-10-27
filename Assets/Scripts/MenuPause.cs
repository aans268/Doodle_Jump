using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton;
    // Update is called once per frame

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale= 1f;

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale= 1f;
    }

    public void Pause()
    {
        pauseButton.gameObject.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale= 0f;
    }
}
