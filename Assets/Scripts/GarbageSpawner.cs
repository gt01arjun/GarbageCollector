using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    public int numberOfGarbages;

    public GameObject[] spawnLocations;

    public GameObject[] garbages;

    private void Start()
    {
        for (int i = 1; i <= numberOfGarbages; i++)
        {
            var g = Random.Range(0, garbages.Length);
            var p = Random.Range(0, spawnLocations.Length);

            Instantiate(garbages[g], spawnLocations[p].transform.position, garbages[g].transform.rotation);
        }
    }
}