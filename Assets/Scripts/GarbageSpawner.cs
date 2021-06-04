using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    public static int NumberOfGarbages;

    public GameObject[] spawnLocations;

    public GameObject[] garbages;

    private void Start()
    {
        NumberOfGarbages = 21;

        for (int i = 1; i <= NumberOfGarbages; i++)
        {
            var g = Random.Range(0, garbages.Length);
            var p = Random.Range(0, spawnLocations.Length);

            Instantiate(garbages[g], spawnLocations[p].transform.position, garbages[g].transform.rotation);
        }
    }
}