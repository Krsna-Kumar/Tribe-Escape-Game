using System.Collections.Generic;
using UnityEngine;

public class PlayerStack : MonoBehaviour
{
    public Transform handTransform; // The transform of the player's hand
    public float maxDistance = 1f; // The maximum distance for collecting objects
    public float collectingSpeed = 5f; // The speed at which objects will be collected
    public LayerMask collectableLayer; // The layer of objects that can be collected
    private PlayerMove playerMove; // Variable to find player
    public int maxStackCount; // max stack count
    private int currentStackCount; // current Stack Count

    [HideInInspector]
    public bool isStacking;

    private List<GameObject> collectedObjects = new List<GameObject>(); // List of collected objects

    private void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        // Check for nearby objects to collect
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxDistance);
        foreach (Collider collider in hitColliders)
        {
            if (collectedObjects.Contains(collider.gameObject)) continue; // Skip objects that have already been collected
            if ((collectableLayer.value & (1 << collider.gameObject.layer)) == 0) continue; // Skip objects that are not on the collectable layer
            if (currentStackCount >= maxStackCount) break; // Skip objects if we have reached the maximum stack count
            CollectObject(collider.gameObject);
            currentStackCount++;
        }

        if (collectedObjects.Count > 0)
        {
            isStacking = true;
        }
        else
        {
            isStacking = false;
        }

        // ********   D   R   O   P   *********

        if (playerMove.pickedLost && collectedObjects.Count > 0)
        {
            GameObject topObject = collectedObjects[collectedObjects.Count - 1]; // Get the top object in the collected objects list
            // Remove the top object from the list

            topObject.transform.parent = null; // Unparent the top object
            collectedObjects.RemoveAt(collectedObjects.Count - 1);

            Collider collider = topObject.GetComponent<Collider>();
            if (collider != null) collider.enabled = true;

            Rigidbody rigidbody = topObject.GetComponent<Rigidbody>();
            if (rigidbody != null) rigidbody.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        // Move collected objects to player's hand and stack them

        Vector3 stackOffset = Vector3.zero;
        foreach (GameObject collectedObject in collectedObjects)
        {
            collectedObject.transform.position = Vector3.Lerp(collectedObject.transform.position, handTransform.position + stackOffset,
                Time.deltaTime * collectingSpeed);
            stackOffset += Vector3.up * 1; // Add the height of the object to the stack offset

            // Rotate object in the direction the player is facing
            Vector3 lookDirection = transform.up;
            lookDirection.y = 90f; // Zero out the y component to avoid tilting the object
        }
    }

    private void CollectObject(GameObject obj)
    {
        if(playerMove.holdStrength >= 0)
        {
            collectedObjects.Add(obj);

            // Make object a child of player's hand
            obj.transform.parent = handTransform;

            // Move object to player's hand
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);

            // Disable collider and rigidbody so the object no longer interacts with the environment
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null) collider.enabled = false;

            Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
            if (rigidbody != null) rigidbody.isKinematic = true;
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drop"))
        {
            print("Into Drop");
        }
    }
}