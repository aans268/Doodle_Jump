using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button playButton; // Bouton Play
    private Image buttonImage; // Image associée au bouton

    public Sprite playButtonOnClick; // Sprite à afficher après le clic

    void Start()
    {
        // Vérifier que le bouton est assigné
        if (playButton != null)
        {
            buttonImage = playButton.GetComponent<Image>();
        }
        
    }

    public void OnClick()
    {
        if (buttonImage != null && playButtonOnClick != null)
        {
            buttonImage.sprite = playButtonOnClick; // Changer le sprite
            Invoke("StartGame", 1f); // Lancer le jeu après 3 secondes
        }
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
