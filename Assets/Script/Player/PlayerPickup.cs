using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform pickupPoint;

    public Transform sphereOrigin;

    public float sphereRadius;
    public LayerMask layerMask;

    private GameObject pickedObject = null;
    private Vector3 prevPosition;
    private Vector3 desirePos;

    public int maxHoldStrength;

    private PlayerMove playerMove;
    private PlayerDrop playerDrop;

    public GameObject theArrow;

    [HideInInspector]
    public bool isHolding;

    public GameObject missionText;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerDrop = FindObjectOfType<PlayerDrop>();

        missionText.SetActive(false);

        theArrow.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;
        bool isHit = Physics.SphereCast(sphereOrigin.position, sphereRadius, Vector3.forward, out hit, 5f, layerMask);
        if (isHit && !isHolding && playerMove.holdStrength > 0)
        {
            isHolding = true;
            pickedObject = hit.collider.gameObject;
            pickedObject.GetComponent<Rigidbody>().isKinematic = true;
            pickedObject.GetComponent<Collider>().isTrigger = true;
            //pickedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            pickedObject.GetComponent<Transform>();
            prevPosition = pickedObject.transform.position;

            pickedObject.transform.position = Vector3.Lerp(pickedObject.transform.position, pickupPoint.transform.position, 0.005f * Time.deltaTime);

            /*pickedObject.transform.position = Vector3.Lerp(prevPosition, pickupPoint.position, 0.005f*
                Time.deltaTime);**/

            pickedObject.transform.parent = pickupPoint.transform;

            pickedObject.transform.localPosition = new Vector3(0, 0.3f, 0);
            pickedObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

            //enabling arrow
            theArrow.SetActive(true);
        }

        // BEFORE LEVEL COMPLETED

        if (playerMove.pickedLost && pickedObject != null && playerMove.holdStrength <= 0)
        {
            isHolding = false;

            pickedObject.GetComponent<Rigidbody>().isKinematic = false;

            // Apply force to object based on player's movement

            Vector3 movement = pickedObject.transform.position;
            float forceMagnitude = Mathf.Clamp(0, 0, movement.magnitude * 5000);
            pickedObject.GetComponent<Rigidbody>().AddForce(movement.normalized * forceMagnitude * 50);

            pickedObject.GetComponent<Collider>().isTrigger = false;

            pickedObject.transform.parent = null;

            playerMove.holdStrength = 2;

            pickedObject = null;

            theArrow.SetActive(false);
        }

        // *******  MISSION COMPLETED  *******


        if (playerDrop.missionCompleted)
        {
            if (!isHolding) return;

            isHolding = false;

            pickedObject.layer = 0;

            pickedObject.GetComponent<Rigidbody>().isKinematic = false;

            // Apply force to object based on player's movement

            Vector3 movement = pickedObject.transform.position;
            float forceMagnitude = Mathf.Clamp(0, 0, movement.magnitude * 5000);
            pickedObject.GetComponent<Rigidbody>().AddForce(movement.normalized * forceMagnitude * 50);

            pickedObject.GetComponent<Collider>().isTrigger = false;

            pickedObject.transform.parent = null;

            pickedObject = null;

            theArrow.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drop"))
        {
            playerMove.holdStrength = 0;

            // missionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Drop"))
        {
            //missionText.SetActive(false);
        }
    }
}