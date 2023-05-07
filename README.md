Alt før "Features to attempt" færdigt.
Tilføjet at bolden bouncer på hegnet.
Har "built" projektet til et færdigt program med exe-fil. Det ligger i mappen "TheFootballGame"

Materiale:

 - https://cadacreate.medium.com/how-to-add-existing-unity-project-to-github-916ad75160e7
 - https://www.youtube.com/watch?v=u_n3NEi223E&ab_channel=BMo
 - https://www.youtube.com/watch?v=VomZe-_WWsE&ab_channel=AlfonsoSaballett
 - https://www.youtube.com/watch?v=CcKk0lziEMc
 - Programming with Unity
 - https://stackoverflow.com/questions/51931455/unity3d-bouncing-reflecting-raycast

ChatGPT:

I apologize for the confusion. To make the ball bounce back when it hits an object, you should use the reflection vector based on the object's surface normal. You can obtain the surface normal from the contact point information provided by the `OnTriggerEnter` method.

To achieve this, you'll need to replace the `OnTriggerEnter` method with the `OnTriggerStay` method and use the `Physics.Raycast` function to detect collisions and obtain contact point information.

Here's the modified script:

```csharp
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float bounceForce = 10.0f;

    private void OnTriggerStay(Collider other)
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
            RaycastHit hit;
            Ray ray = new Ray(_rigidbody.position, _rigidbody.velocity.normalized);

            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                Vector3 reflection = Vector3.Reflect(_rigidbody.velocity, hit.normal);
                _rigidbody.velocity = reflection.normalized * bounceForce;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
```

This script uses the `OnTriggerStay` method to continuously check for collisions while the ball is inside the trigger area. When the ball is in contact with a bounce object, a raycast is performed in the direction of the ball's velocity. If the raycast hits an object, the surface normal at the contact point is used to calculate the reflection vector, which is then used to update the ball's velocity, making it bounce back.

Remember to keep the Rigidbody, Sphere Collider, and "Bounce" tag settings the same as previously instructed. With these changes, the ball should now bounce back when hitting an object with the "Bounce" tag.


