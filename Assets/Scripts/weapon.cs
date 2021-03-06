using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if(type == Type.Melee) {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type == Type.Range) {
            StartCoroutine("Shot");
        }
    }
    IEnumerator Swing() 
    { 
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.5f);
        trailEffect.enabled = false; 
    }
    IEnumerator Shot() 
    {
        GameObject instanceBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instanceBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
        GameObject instanceBulletCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody bulletCaseRigid = instanceBulletCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletCaseRigid.AddForce(caseVec, ForceMode.Impulse);
        bulletCaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
