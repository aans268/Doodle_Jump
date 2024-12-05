using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        // Code spécifique à l'éditeur
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Code pour quitter le jeu en runtime
        Application.Quit();
#endif
    }
}
