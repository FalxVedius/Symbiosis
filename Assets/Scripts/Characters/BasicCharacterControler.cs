using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterControler : MonoBehaviour
{
    [SerializeField] KeyCode left, right, jump;
    [SerializeField] float speed, maxSpeed, jumpForce, jumpCoolDown, jumpCoolDownConstant, stopForce;
    [SerializeField] Rigidbody2D RB2D;

    bool isGrounded = true;
    bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            Move();
            Jump();
        }
    }

    public void Move()
    {
        //Going left
        if (Input.GetKey(left))
        {
            //Check Max Speed
            if (Mathf.Abs(RB2D.velocity.x) <= maxSpeed)
            {
                //Add force Left
                RB2D.velocity = new Vector2(RB2D.velocity.x - speed, RB2D.velocity.y);
            }
        }

        //Going right
        if (Input.GetKey(right))
        {
            //Check Max Speed
            if (Mathf.Abs(RB2D.velocity.x) <= maxSpeed)
            {
                //Add force Right
                RB2D.velocity = new Vector2(RB2D.velocity.x + speed, RB2D.velocity.y);
            }
        }

        //Stopping
        if (!Input.GetKey(right) && !Input.GetKey(left) && stopForce != 0)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x / stopForce, RB2D.velocity.y);
        }
    }

    public void Jump()
    {
        //Jumping
        if (Input.GetKeyDown(jump) && isGrounded)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x, RB2D.velocity.y + jumpForce);
            jumpCoolDown = jumpCoolDownConstant;
            isGrounded = false;
        }

        //Jump cooldown
        if (isGrounded == false && RB2D.velocity.y <= 0.0005 && RB2D.velocity.y >= -0.0005)
        {
            jumpCoolDown -= Time.deltaTime;
            if (jumpCoolDown <= 0)
            {
                isGrounded = true;
            }
        }
    }

    public void SetIsActive(bool set)
    {
        isActive = set;
    }
}
