using UnityEngine;

public class Garbage : MonoBehaviour
{
    public int StorageAmount;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetTrigger") && transform.CompareTag(other.transform.root.GetComponentInChildren<Magnet>().GarbageVariant.ToString()))
        {
            _rb.constraints = RigidbodyConstraints.None;
            GameManager.CurrentTruckStorage += StorageAmount;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MagnetTrigger") && transform.CompareTag(other.transform.root.GetComponentInChildren<Magnet>().GarbageVariant.ToString()))
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            GameManager.CurrentTruckStorage -= StorageAmount;
        }
    }
}