using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    [SerializeField]
    private Collider staticObjects;

    [HideInInspector]
    public bool isGrounded;


    private void OnTriggerEnter(Collider other)
    {
        if(other == staticObjects)
        {
            isGrounded = true;
            Debug.Log("Is Grounded");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other == staticObjects)
        {
            isGrounded = false;
            print("Is NOT grounded");
        }
    }
}
