using UnityEngine;

public class DieController : MonoBehaviour
{
    private float fallSpeed = 10f; // Vitesse de chute
    private Vector3 initialPosition;

    void Start()
    {
        // Enregistrer la position initiale ou configurer l'objet si nécessaire
        initialPosition = transform.position;
    }

    void Update()
    {
        // Faire tomber le personnage
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Optionnel : vérifier si le personnage a atteint un seuil et gérer la fin de la chute
        if (transform.position.y < -10f) // Par exemple, si le personnage tombe en dessous de -10
        {
            Debug.Log("Le joueur a chuté !");
            // Recharge la scène de jeu ou affiche un écran de Game Over
            // SceneManager.LoadScene("GameOver");
        }
    }
}
