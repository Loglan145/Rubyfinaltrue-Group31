using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public GameObject pickupParticlePrefab;
    public AudioClip collectedClip;
    public bool IsSpeed = false;//this allows you to change the pickup to a speed pickup

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();


        
        if (IsSpeed == true) //this checks whether or not the collectible is a speed collectible
        {
            if (controller != null)
            {
                controller.ChangeSpeed(+1); //this increases the players speed by one
                controller.PlaySound(collectedClip);
                Instantiate(pickupParticlePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else //if the collectible isn't a speed collectible it refers to this
        {
            if (controller != null)
            {
                if (controller.health < controller.maxHealth)
                {
                    controller.ChangeHealth(1);
                    controller.PlaySound(collectedClip);
                    Instantiate(pickupParticlePrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);

                }
            }
        }
    }  
    
    
        
    

    
    
    
        
    
}
