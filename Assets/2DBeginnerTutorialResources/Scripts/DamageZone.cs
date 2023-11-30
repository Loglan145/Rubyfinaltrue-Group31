using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public AudioClip stab; 
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        

        if (controller != null)
        {
            if (controller.isInvincible == false)
            {
                controller.ChangeHealth(-1);
                audioSource.PlayOneShot(stab);
            }
            
            
        }
    }

}
