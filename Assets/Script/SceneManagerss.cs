using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManagerss : MonoBehaviour
{
    public Animator transitionAnim;

    public GameObject failedPanel;

    private  PlayerHealth playerHealth;

    [Space]
    public string nextSceneName;


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

    public IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextSceneName);
    }
}
