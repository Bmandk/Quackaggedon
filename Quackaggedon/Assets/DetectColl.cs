using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DetectColl : MonoBehaviour
{
    public GameObject toSpawn;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void OnParticleCollision(GameObject other)
    {
     //   var pos = new Vector3(other.transform.position.x, other.transform.position.y, 0);



        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                Vector3 pos = collisionEvents[i].intersection;
                pos = new Vector3(pos.x, pos.y, 0);
                Instantiate(toSpawn, pos, Quaternion.identity);
                //Vector3 force = collisionEvents[i].velocity * 10;
                //rb.AddForce(force);
            }
            i++;
        }
    }
}
