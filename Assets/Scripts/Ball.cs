using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float bounceForce = 10.0f;
    public Vector3 bounceDirection = Vector3.up;
    private Vector3 spawnPoint;
    public float respawnWaitTime = 2f;
    public Vector3 bounceObjectNormal = Vector3.up;


    public void Respawn()
    {
        Invoke("ToStartPos", 2f);
    }

    public void ToStartPos()
    {
        transform.position = spawnPoint;
        _rigidbody.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player is not null)
            {
                // By subtracting the two vectors, we get a delta that 
                // 'points' from player towards this object, basically
                // The direction we need to move forwards
                var deltaPosition = _rigidbody.transform.position - player.transform.position;
                
                // Make sure y is flattened since we do not
                // move up or down
                deltaPosition.y = 0;
                
                // Make it a unit direction (magnitude of 1)
                var forward = deltaPosition.normalized;
                
                // Add an impulse forward, using the speed of the player
                _rigidbody.AddForce(forward * player.Speed, ForceMode.Impulse);
            }
        }
        
        if (other.CompareTag("Bounce"))
        {
            Debug.Log("Bounce Triggered");
            Vector3 direction = _rigidbody.velocity.normalized;
            Vector3 point = other.ClosestPointOnBounds(transform.position);

            RaycastHit hit;
            if (Physics.Raycast(point, direction, out hit))
            {
                Vector3 reflection = Vector3.Reflect(direction, hit.normal);
                _rigidbody.velocity = reflection * bounceForce;
            }
            /*RaycastHit hit;
            if (Physics.Raycast(transform.position, _rigidbody.velocity.normalized, out hit))
            {
                Vector3 reflection = Vector3.Reflect(_rigidbody.velocity, hit.normal);
                _rigidbody.velocity = reflection.normalized * bounceForce;
            }*/
        }

    }

    private void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
