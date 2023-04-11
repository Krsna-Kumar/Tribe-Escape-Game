using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Variables

    [SerializeField] private float moveSpeed;

    public Joystick joystick;

    public GameObject directionCicle;

    public LayerMask thisIsGround;

    public int holdStrength;

    private Rigidbody player_rb;

    private PlayerPickup playerPickup;

    private PlayerDrop playerDrop;

    public GameObject boatPaddle;

    [HideInInspector]
    public bool pickedLost;

    private float horizonrtalInput;
    private float verticalInput;

    [HideInInspector]
    public bool isRunning;


    #endregion


    #region Private Methods
    private void Start()
    {
        player_rb = GetComponent<Rigidbody>();
        playerPickup = GetComponent<PlayerPickup>();
        playerDrop = FindObjectOfType<PlayerDrop>();
        boatPaddle.SetActive(false);
    }

    private void Update()
    {
        if(holdStrength == 0)
        {
            pickedLost = true;
        }

        // Debug.Log(holdStrength);
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleBoat();
    }

    private void HandleMovement()
    {
        if (playerDrop.onBoat) return;

            horizonrtalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;

            player_rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, player_rb.velocity.y,
                joystick.Vertical * moveSpeed);

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(player_rb.velocity);

                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
        
    }

    private void HandleBoat()
    {
        if (!playerDrop.onBoat) return;

        horizonrtalInput = 0;
        verticalInput = 0;

        directionCicle.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hurt"))
        {
            holdStrength -= 1;

        }

        if (holdStrength <= -1)
        {
            holdStrength = 2;
        }




    }

    #endregion 
}
