using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon };
    public Type type;
    public int value;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);    
    }
}
