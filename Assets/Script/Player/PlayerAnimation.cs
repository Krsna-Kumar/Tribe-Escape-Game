using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Variables

    private Animator playerAnim;
    private int holdLayerIndex;

    private  PlayerMove player;
    //For Single Hand Log
    private PlayerPickup playerPickup;
    private PlayerDrop playerDrop;
    //For Stackings Mechanics
    private PlayerStack playerStack;

    float targetLayerValue = 1;
    #endregion

    #region Private Methods

    
    void Awake()
    {
        player = GetComponentInParent<PlayerMove>();
        //Single Stack
        playerPickup = GetComponentInParent<PlayerPickup>();
        //Stacking Mechanics
        playerStack = GetComponentInParent<PlayerStack>();
        playerDrop = FindObjectOfType<PlayerDrop>();
        playerAnim = GetComponent<Animator>();
        holdLayerIndex = playerAnim.GetLayerIndex("UpperBody");
    }

    private void Update()
    {
        playerAnim.SetBool("isRunning", player.isRunning);

        playerAnim.SetBool("onBoat", playerDrop.onBoat);

        //holding
        /*if (playerStack.isStacking)
        {
            playerAnim.SetLayerWeight(holdLayerIndex, targetLayerValue);
        }
        else
        {
            playerAnim.SetLayerWeight(holdLayerIndex, 0);
        }*/
        
        if (playerPickup.isHolding)
        {
            playerAnim.SetLayerWeight(holdLayerIndex, targetLayerValue);
        }
        else
        {
            playerAnim.SetLayerWeight(holdLayerIndex, 0);
        }
    }

    #endregion
}
