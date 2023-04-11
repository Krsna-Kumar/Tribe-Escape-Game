using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    #region Variables

    [SerializeField] private float maxHealth = 3;
    private float currentHealth;

    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private SkinnedMeshRenderer playerMr;
    private Material originalColor;
    public Material desireColor;

    public PlayerDrop playerDrop;

    private bool isHitted;

    [HideInInspector]
    public bool isSafe = false;

    #endregion Variables

    private void Start()
    {
        currentHealth = maxHealth;
        playerHealth.UpdateHealthBar(maxHealth, currentHealth);
        playerHealth._currentHealth = currentHealth;

        // Blood Red material
        originalColor = playerMr.material;
    }

    private void Update()
    {
        TrackHealth();
    }

    #region player collision

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerDrop.onBoat)
        {
            if (collision.gameObject.CompareTag("Hurt") && currentHealth > 0)
            {
                isHitted = true;
                currentHealth -= 1;
                FlashStart();
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Hurt") && currentHealth > 0)
            {
                isHitted = true;
                currentHealth -= 10;
                FlashStart();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Safe"))
        {
            Debug.Log("You're in Safe zone");
            isSafe = true;
        }
    }

    #endregion player collision

    #region Tracking Health

    private void TrackHealth()
    {
        if (isHitted)
        {
            playerHealth.UpdateHealthBar(maxHealth, currentHealth);
        }
        if (currentHealth == 0)
        {
            playerHealth.isDead = true;
        }
    }

    private void FlashStart()
    {
        playerMr.material = desireColor;
        Invoke("StopFlash", 0.2f);
    }

    private void StopFlash()
    {
        playerMr.material = originalColor;
    }

    #endregion Tracking Health
}