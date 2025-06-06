using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject ammoPrefab;
    public Transform ammoSpawnPoint;
    public GameObject startButton, restartButton, quitButton, playAgainButton;
    public Text scoreText, ammoText;
    public int maxAmmo = 7;

    private int score = 0;
    private int ammoLeft;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        ammoLeft = maxAmmo;
        UpdateUI();
        Instantiate(ammoPrefab, ammoSpawnPoint.position, Quaternion.identity);
        Object.FindFirstObjectByType<TargetSpawner>().SpawnTargets();
        startButton.SetActive(false);
    }

    public void UseAmmo()
    {
        ammoLeft--;
        UpdateUI();

        if (ammoLeft > 0)
        {
            Instantiate(ammoPrefab, ammoSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            playAgainButton.SetActive(true);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        ammoText.text = "Ammo: " + ammoLeft;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayAgain()
    {
        score = 0;
        ammoLeft = maxAmmo;
        UpdateUI();
        playAgainButton.SetActive(false);

        foreach (var target in GameObject.FindGameObjectsWithTag("Target"))
            Destroy(target);

        Instantiate(ammoPrefab, ammoSpawnPoint.position, Quaternion.identity);
        FindFirstObjectByType<TargetSpawner>().SpawnTargets();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}