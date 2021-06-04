using UnityEngine;

public class MagnetSwitcher : MonoBehaviour
{
    public GameObject[] MagnetTypes;

    public string ThisMagnet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var magnet in MagnetTypes)
            {
                if (magnet.name == ThisMagnet)
                {
                    magnet.SetActive(true);
                }
                else
                {
                    magnet.SetActive(false);
                }
            }
        }
    }
}