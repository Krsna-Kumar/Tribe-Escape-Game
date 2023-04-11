using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject failedPanel;

    private  PlayerHealth playerHealth;


    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        failedPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerHealth.isDead)
        {
            failedPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
