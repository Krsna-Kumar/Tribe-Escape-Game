using UnityEngine;

public class TBoatSpawner : MonoBehaviour
{
    public GameObject enemyBoatPrefab;
    public Transform[] boatSpawnPos;

    public PlayerDrop playerDrop;

    private bool hasInstantiated = false;

    private void FixedUpdate()
    {
        if (playerDrop.onBoat && !hasInstantiated)
        {
            for (int i = 0; i < boatSpawnPos.Length; i++)
            {
                Instantiate(enemyBoatPrefab, boatSpawnPos[i].position, boatSpawnPos[i].rotation);
                hasInstantiated = true;
            }
        }
    }
}