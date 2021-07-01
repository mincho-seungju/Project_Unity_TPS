using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    // enum 열거형 타입(타입이름 지정 필요)
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon };
    public Type type;
    public int value;

    // Update is called once per frame
    void Update()
    {
        
    }
}
