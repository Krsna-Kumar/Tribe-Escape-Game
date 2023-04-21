using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public Transform dropTarget;
    public float rotationSpeed;

    public PlayerStack playerStack;

    private void FixedUpdate()
    {
        if (playerStack.isStacking)
        {
            RotateArrow();
        }
        else
        {
            return;
        }
    }

    private void LateUpdate()
    {
        transform.position = playerStack.transform.position + new Vector3(0, 7, 0);
    }

    private void RotateArrow()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dropTarget.position - transform.position)
           , rotationSpeed * Time.deltaTime);
    }
}