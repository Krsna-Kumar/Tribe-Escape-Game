using UnityEngine;

public class EnemyVehicle : MonoBehaviour
{
    private GameObject target;
    public float speed = 5f;
    private float stoppingDistance = 5f;

    private PlayerCollision playerCol;

    private Rigidbody vehicleRb;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        vehicleRb = GetComponent<Rigidbody>();
        speed = Random.Range(speed, speed + 1);

        playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollision>();
    }

    private void FixedUpdate()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (target != null )
        {
            if (!playerCol.isSafe)
            {
                Vector3 direction = target.transform.position - transform.position;
                float distance = direction.magnitude;
                if (distance > stoppingDistance)
                {
                    Vector3 velocity = direction.normalized * speed;
                    vehicleRb.velocity = velocity;
                }
                else
                {
                    vehicleRb.velocity = Vector3.zero;
                }



                transform.LookAt(target.transform.position);
            }

            else
            {
                vehicleRb.velocity = Vector3.zero;
            }

        
        }
    }
}