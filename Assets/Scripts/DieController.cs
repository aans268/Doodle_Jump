using UnityEngine;
using System.Collections;

public class DieController : MonoBehaviour
{
    private float fallSpeed = 10f; // Vitesse de chute
    private bool isFalling = false; // Indique si le personnage est en train de chuter
    public GameObject deathMenuCanvas; // Canvas pour le menu de mort
    private float chuteDuration = 2f; // Durée de la chute avant d'afficher le menu
    private float lowestVisibleY;
    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        // Assurez-vous que le menu de mort est désactivé au début
        if (deathMenuCanvas != null)
        {
            deathMenuCanvas.SetActive(false);
        }
    }

    void Update()
    {
        lowestVisibleY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        float playerBottomY = transform.position.y - playerCollider.bounds.extents.y;

        // Détecter si le personnage sort de l'écran par le bas
        if (playerBottomY < lowestVisibleY && !isFalling) // Seuil pour détecter la chute
        {
            isFalling = true;
            StartCoroutine(HandleDeathSequence());
        }

        // Continuer la chute du personnage s'il est marqué comme étant en train de chuter
        if (isFalling)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }

    // Coroutine pour gérer la transition vers le menu de mort
    private IEnumerator HandleDeathSequence()
    {
        // Continuer la chute pendant une courte durée
        yield return new WaitForSeconds(chuteDuration);

        // Afficher le menu de mort
        if (deathMenuCanvas != null)
        {
            deathMenuCanvas.SetActive(true);
        }

        // Continuer la chute ou faire disparaître le personnage
        StartCoroutine(FadeAndRemovePlayer());
    }

    // Coroutine pour faire disparaître progressivement le personnage
    private IEnumerator FadeAndRemovePlayer()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            float fadeDuration = 2f; // Durée de la disparition
            Color originalColor = sprite.color;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                sprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
        }

        // Désactiver le personnage une fois la transition terminée
        gameObject.SetActive(false);
    }
}
