using UnityEngine;

public class PlayerDrop : MonoBehaviour
{
    public int requiredLog;

    public GameObject boatPrefab;
    public GameObject realPlayer;
    public Transform boatSeat;
    public GameObject SafeZone;

    [HideInInspector]
    public int requiredLogText;

    [HideInInspector]
    public bool onBoat;

    [HideInInspector]
    public bool missionCompleted = false;

    private int currentLogDetected;

    private void Start()
    {
        currentLogDetected = 0;
        requiredLogText = requiredLog;
        boatPrefab.SetActive(false);
        SafeZone.SetActive(false);
    }

    private void Update()
    {
        if (currentLogDetected == requiredLog)
        {
            missionCompleted = true;
            boatPrefab.SetActive(true);
            onBoat = true;
            realPlayer.transform.position = boatSeat.position;
            realPlayer.transform.parent = boatSeat.transform;

            Invoke("EnableSafeZone", 0f);

            ///Do whatever it takes
        }
    }

    private void EnableSafeZone()
    {
        SafeZone.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable"))
        {
            currentLogDetected++;
            requiredLogText--;
        }
    }
}