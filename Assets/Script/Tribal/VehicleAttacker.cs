using UnityEngine;

public class VehicleAttacker : MonoBehaviour
{
    public GameObject BigStone;
    public Transform handPos;

    public float throwSpeed;
    public float throwInterval;

    private PlayerCollision playerCol;

    private void Start()
    {
        playerCol = GameObject.Find("Player").GetComponent<PlayerCollision>();

        if (!playerCol.isSafe)
        {
            throwInterval = Random.Range(1.5f, 3f);
            InvokeRepeating("ThrowStones", 0f, throwInterval);
        }
        else
        {
            return;
        }

       
    }

    private void ThrowStones()
    {
        Rigidbody rb = Instantiate(BigStone, handPos.position, Quaternion.identity).GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * throwSpeed, ForceMode.Impulse);
        rb.AddForce(transform.up * throwSpeed / 5, ForceMode.Impulse);
        rb.AddTorque(Vector3.right * 5f);

        Destroy(rb.gameObject, 5f);
    }
}