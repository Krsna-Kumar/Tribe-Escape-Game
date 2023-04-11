using UnityEngine;

public class SpawnTribal : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public int numberToSpawn;

    public int maxPosX, minPosX;
    public int maxPosZ, minPosZ;

    private void Start()
    {
        SpawnRandomTribal();
    }

    private void SpawnRandomTribal()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            //int specificTribalIndex = 0 ;
            int randomTribalIndex = Random.Range(0, itemPrefabs.Length);

            Vector3 randomSpawnPosition = new Vector3(Random.Range(minPosX, maxPosX), 0, Random.Range(minPosZ, maxPosZ));

            Instantiate(itemPrefabs[randomTribalIndex], transform.position + randomSpawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
}