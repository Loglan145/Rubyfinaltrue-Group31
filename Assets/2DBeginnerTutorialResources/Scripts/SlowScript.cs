using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) //this triggers the slow effect when the player enters the pool
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeSpeed(-2); //this changes the players speed
        
        }
    }
    void OnTriggerExit2D(Collider2D other) //this deactivates the slow effect when the player exits
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeSpeed(+2); //this changes the players speed
        }
    }

}
