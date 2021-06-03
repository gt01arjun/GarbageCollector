using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField]
    private float _magnetForce;
    [SerializeField]
    private Transform _collectionPoint;

    public enum GarbageType
    {
        PlasticGarbage,
        PaperGarbage,
        SteelGarbage
    };

    [SerializeField]
    private GarbageType _garbageType;

    void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 2.5f);

        if (cols.Length <= 0)
            return;

        foreach (Collider c in cols)
        {
            if (c.CompareTag(_garbageType.ToString()))
            {
                if (c)
                {
                    if (transform.CompareTag("MagneticSphere"))
                    {
                        c.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(c.transform.position, _collectionPoint.position, _magnetForce * Time.smoothDeltaTime));
                    }
                }
            }
        }
    }
}