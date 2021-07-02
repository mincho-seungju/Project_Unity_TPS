using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    float moveX;
    float moveZ;

    
    Animator anim;
    Rigidbody rigid;

    Vector3 moveVector;
    Vector3 dodgeVector;

    int equipWeaponIndex = -1;

    bool isRun;
    bool isJump;
    bool isDodge;
    bool getItem;
    bool swap1;
    bool swap2;
    bool swap3;

    GameObject nearObject;
    GameObject equipWeapon;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        isRun = Input.GetButton("Walk");

        moveVector = new Vector3(moveX, 0, moveZ).normalized;
        if(isDodge) {
            moveVector = dodgeVector;
        }
        if(Input.GetKey(KeyCode.LeftShift)) {
            isRun = true;
            anim.SetBool("isRun", isRun);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) {
            isRun = false;
            anim.SetBool("isRun", isRun);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            getItem = true;
        }
        if(Input.GetKeyUp(KeyCode.E)) {
            getItem = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            swap1 = true;
        }
        if(Input.GetKeyUp(KeyCode.Alpha1)) {
            swap1 = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            swap2 = true;
        }
        if(Input.GetKeyUp(KeyCode.Alpha2)) {
            swap2 = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            swap3 = true;
        }
        if(Input.GetKeyUp(KeyCode.Alpha3)) {
            swap3 = false;
        }
        Swap();
        

        if(getItem && nearObject != null && !isDodge) {
            if(nearObject.tag == "weapon") {
                item item = nearObject.GetComponent<item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
        transform.position += moveVector * (isRun? speed : speed / 2) * Time.deltaTime;
        transform.LookAt(transform.position + moveVector);
        anim.SetBool("isWalk", moveVector != Vector3.zero);
        Jump();
        Dodge();
    }
    void OnCollisionEnter(Collision collider) {
        // Debug.Log(collider.gameObject);
        if(collider.gameObject.tag == "Floor") {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
    void Jump()
    {
        if(!isJump && Input.GetKeyDown(KeyCode.Space) && !isDodge) {
            rigid.AddForce(Vector3.up * 7f, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }
    void Dodge() 
    { 
        if(!isJump && moveVector != Vector3.zero && Input.GetKeyDown(KeyCode.G) && !isDodge) {
            dodgeVector = moveVector;
            speed *= 2;
            rigid.AddForce(transform.forward * 5, ForceMode.Impulse);
            anim.SetTrigger("doDodge");
            isDodge = true;
            // 시간차 함수
            Invoke("DodgeOut", 1.5f);
        }
    }
    void Swap() 
    {   
        if(swap1 && (!hasWeapons[0] || equipWeaponIndex == 0)) {
            return;
        }
        if(swap2 && (!hasWeapons[1] || equipWeaponIndex == 1)) {
            return;
        }
        if(swap3 && (!hasWeapons[2] || equipWeaponIndex == 2)) {
            return;
        }
        int weaponIndex = -1;
        if(swap1) weaponIndex = 0;
        if(swap2) weaponIndex = 1;
        if(swap3) weaponIndex = 2;

        if((swap1 || swap2 || swap3) && !isJump && !isDodge) {
            if(equipWeapon != null) {
                equipWeapon.SetActive(false);
            }
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);

            anim.SetTrigger("doSwap");
        }
    }
    void DodgeOut()     
    {
        speed *= 0.5f;
        isDodge = false;
    }
    void OnTriggerStay(Collider other) 
    {
        if(other.tag == "weapon") {
            nearObject = other.gameObject;
        }
    }   
    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "weapon") {
            nearObject = null;
        }
    }
}
