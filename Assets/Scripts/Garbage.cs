using UnityEngine;

public class Garbage : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetTrigger"))
        {
            _rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MagnetTrigger"))
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}