using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Start()
    {
        // Définir la résolution au démarrage 10:16
        Screen.SetResolution(600, 1280, false);

    }
}
