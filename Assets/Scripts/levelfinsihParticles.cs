using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelfinsihParticles : MonoBehaviour
{
    public int emojiNumber=0;
    [SerializeField] ParticleSystem one;
    [SerializeField] ParticleSystem two;
    [SerializeField] ParticleSystem three;

    [SerializeField] ParticleSystem dissapointed;
    [SerializeField] ParticleSystem happy;

    [SerializeField] Transform slot1;
    [SerializeField] Transform slot2;
    [SerializeField] Transform slot3;

    bool isItCalled = true;
    private void OnTriggerEnter(Collider other)
    {
        if (isItCalled)
        {
        ParticalProccess();

        }
        
        
    }

    private void ParticalProccess()
    {
        isItCalled = false;
        if (emojiNumber==0)
        {
            Instantiate(dissapointed, slot1.position, gameObject.transform.rotation);
            Instantiate(dissapointed, slot2.position, gameObject.transform.rotation);
            Instantiate(dissapointed, slot3.position, gameObject.transform.rotation);
        }
        if (emojiNumber==1)
        {
            one.Play();
            Instantiate(happy, slot1.position, gameObject.transform.rotation);
            Instantiate(dissapointed, slot2.position, gameObject.transform.rotation);
            Instantiate(dissapointed, slot3.position, gameObject.transform.rotation);
        }
        if (emojiNumber == 2)
        {
            one.Play();
            two.Play();
            Instantiate(happy, slot1.position, gameObject.transform.rotation);
            Instantiate(happy, slot2.position, gameObject.transform.rotation);
            Instantiate(dissapointed, slot3.position, gameObject.transform.rotation);
        }
        if (emojiNumber == 3)
        {
            one.Play();
            two.Play();
            three.Play();
            Instantiate(happy, slot1.position, gameObject.transform.rotation);
            Instantiate(happy, slot2.position, gameObject.transform.rotation);
            Instantiate(happy, slot3.position, gameObject.transform.rotation);
        }
    }
}
