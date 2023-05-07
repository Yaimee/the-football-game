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
                var deltaPosition = _rigidbody.transform.position - player.transform.position;
                deltaPosition.y = 0;
                var forward = deltaPosition.normalized;
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
