using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{

    public PlayerDrop playerDrop;
    public GameObject player;
    public Transform seatPoint;
    public Vector3 seatOffset;
    public float vehicleSpeed;
    public float turningSpeed;
    public Joystick joystick;
    
    private Rigidbody vehicleRb;


    [SerializeField] private Image healthBarSprite;
    [SerializeField] private Transform HealthLook;
    [SerializeField] private float reduceSpeed = 2;

    [HideInInspector] public float _currentHealth;

    [HideInInspector] public bool isDead = false;


    private float horizontalInput;
    private float verticalInput;


    private void Start()
    {
        vehicleRb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        EnterVehicle();

        ControlVehicle();
    }

    private void EnterVehicle()
    { 

        if (playerDrop.onBoat)
        {
            player.transform.position = seatPoint.position;
            player.transform.rotation = transform.rotation;
        }

        else
        {
            return;
        }
    }

    private void ControlVehicle()
    {

        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;

        vehicleRb.velocity = new Vector3(horizontalInput * vehicleSpeed, vehicleRb.velocity.y,
               verticalInput * vehicleSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vehicleRb.velocity), turningSpeed * Time.deltaTime);
        }
        else
        {
            return;
        }


    }
}
