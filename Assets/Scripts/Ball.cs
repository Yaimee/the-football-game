using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody _rigidbody;
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

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
