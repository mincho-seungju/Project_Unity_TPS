using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter(Collision collider) 
    {
        if(collider.gameObject.tag == "Floor") {
            Destroy(gameObject, 3);
        }
        if(collider.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }
}
