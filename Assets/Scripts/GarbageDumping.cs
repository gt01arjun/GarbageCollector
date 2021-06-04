using UnityEngine;

public class GarbageDumping : MonoBehaviour
{
    public string DumpingAreaName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(DumpingAreaName))
        {
            GameManager.Score += other.GetComponent<Garbage>().StorageAmount;
            GameManager.CurrentTruckStorage -= other.GetComponent<Garbage>().StorageAmount;
            GarbageSpawner.NumberOfGarbages--;
            Destroy(other.gameObject);
        }
    }
}