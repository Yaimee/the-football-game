using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Outbound : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("OutOfBounds"))
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                player.Die();
                ball.Respawn();
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
