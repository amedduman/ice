using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollect : MonoBehaviour
{
    [SerializeField] ParticleSystem exp;
    levelfinsihParticles levelEnd;
    private void Start()
    {
        levelEnd = FindObjectOfType<levelfinsihParticles>();
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        exp.Play();
        levelEnd.emojiNumber++;
    }
}
