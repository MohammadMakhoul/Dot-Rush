using UnityEngine;
using TMPro;
using System.Collections;

public class Target : MonoBehaviour
{
    // Movement
    public float xRange = 7f;
    public float yRange = 4f;

    // Random size
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    // UI
    public TMP_Text scoreText;
    public TMP_Text plusOneText;

    // Score
    public int score = 0;

    // Audio
    public AudioClip clickSound;
    private AudioSource audioSource;

    // Particle Effect
    public ParticleSystem clickParticles;

    // Target color
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get components from Target
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initial score
        scoreText.text = "Score: " + score;
    }

    private void OnMouseDown()
    {
        // Play click sound
        audioSource.PlayOneShot(clickSound);

        // Increase score
        score++;
        scoreText.text = "Score: " + score;

        // Show particles at current position
        clickParticles.transform.position = transform.position;
        clickParticles.Play();

        // Show +1
        StartCoroutine(ShowPlusOne(transform.position));

        // Generate a new random position
        float randomX = Random.Range(-xRange, xRange);
        float randomY = Random.Range(-yRange, yRange);

        // Move Target
        transform.position = new Vector3(randomX, randomY, 0);

        // Random size
        float randomSize = Random.Range(minSize, maxSize);

        transform.localScale = new Vector3(
            randomSize,
            randomSize,
            1
        );

        // Random color
        spriteRenderer.color = new Color(
            Random.value,
            Random.value,
            Random.value
        );
    }

    private IEnumerator ShowPlusOne(Vector3 targetPosition)
    {
        plusOneText.gameObject.SetActive(true);

        // Convert Target position from World Space to Screen Space
        Vector2 screenPosition =
            Camera.main.WorldToScreenPoint(targetPosition);

        // Convert Screen Space to Canvas local position
        RectTransform canvasRect =
            plusOneText.canvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            plusOneText.canvas.worldCamera,
            out Vector2 localPosition
        );

        RectTransform textRect =
            plusOneText.GetComponent<RectTransform>();

        textRect.anchoredPosition = localPosition;

        Vector2 startPosition = textRect.anchoredPosition;
        Vector2 endPosition = startPosition + new Vector2(0, 100);

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            textRect.anchoredPosition = Vector2.Lerp(
                startPosition,
                endPosition,
                elapsed / duration
            );

            yield return null;
        }

        plusOneText.gameObject.SetActive(false);
    }
}