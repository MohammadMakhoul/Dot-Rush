using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text gameOverText;
    public TMP_Text finalScoreText;

    public GameObject restartButton;
    public GameObject target;
    public Target targetScript;
    public float timeLeft = 30f;

    private bool gameOver = false;

    public AudioClip gameOverSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameOver)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame();
        }

        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
    }

    void EndGame()
    {
        gameOver = true;

        audioSource.PlayOneShot(gameOverSound);

        target.SetActive(false);

        gameOverText.gameObject.SetActive(true);

        finalScoreText.text = "Final Score: " + targetScript.score;
        finalScoreText.gameObject.SetActive(true);

        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}