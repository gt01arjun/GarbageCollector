using System.Collections;
using UnityEngine;

public class ParticleDisabler : MonoBehaviour
{
    public float delay;
    private ParticleSystem fx;

    private void Start()
    {
        fx = GetComponent<ParticleSystem>();
    }

    public IEnumerator disable()
    {
        yield return new WaitForSeconds(delay);
        ParticleSystem.EmissionModule em = fx.emission;
        em.enabled = false;
    }
}