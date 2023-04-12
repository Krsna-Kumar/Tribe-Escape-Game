using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManagerss : MonoBehaviour
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

    public void LoadNextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
