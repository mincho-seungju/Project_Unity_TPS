using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;
    float moveX;
    float moveZ;
    Rigidbody rigid;

    Vector3 moveVector;
    Vector3 dodgeVector;

    bool isRun;
    bool isJump;
    bool isDodge;
    Animator anim;
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
        transform.position += moveVector * (isRun? 10f : 5f) * Time.deltaTime;
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
    void DodgeOut()     
    {
        speed *= 0.5f;
        isDodge = false;
    }
    
}
