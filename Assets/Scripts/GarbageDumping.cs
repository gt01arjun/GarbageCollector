using UnityEngine;

public class GarbageDumping : MonoBehaviour
{
    public string DumpingAreaName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(DumpingAreaName))
        {
            GameManager.Score += other.GetComponent<Garbage>().StorageAmount;
            Destroy(other.gameObject);
            Debug.Log(GameManager.Score);
        }
    }
}