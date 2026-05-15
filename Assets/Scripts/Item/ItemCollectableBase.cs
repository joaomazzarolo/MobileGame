using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public AudioSource audioSource;

    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null);
        if (audioSource != null) audioSource.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
            Debug.Log("chegou base");
        }
    }

    protected virtual void Collect()
    {
        Debug.Log("chegou collect base");
        OnCollect();
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        Debug.Log("chegou oncollect base");
        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }

}
