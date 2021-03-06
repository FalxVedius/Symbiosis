﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterControler : MonoBehaviour
{
    public KeyCode left, right, jump;
    [SerializeField] float speed, maxSpeed, jumpForce, jumpCoolDown, jumpCoolDownConstant, stopForce;
    [SerializeField] Rigidbody2D RB2D;
    [SerializeField] Animator Anim;
    [SerializeField]bool willFlip = false;

    public bool facingRight = true;
    bool isGrounded = true;
    public bool isActive = false;
   public bool isWalking = false;
    bool soundPlaying = false;

    public enum CurrentCharacter { Indi, Jebsee};
    public CurrentCharacter currentCreature;

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
            //Plays Respective Creatures Walk Sound
            if(isActive && currentCreature == CurrentCharacter.Indi && !isWalking && isGrounded)
            {
                AudioManager.instance.PlaySound("Indi_WalkCycle");
                isWalking = true;
            }
            else if (isActive && !isWalking && currentCreature == CurrentCharacter.Jebsee && isGrounded)
            {
                AudioManager.instance.PlaySound("Jebsee_WalkCycle");
                isWalking = true;
            }

            //Check Max Speed
            if (Mathf.Abs(RB2D.velocity.x) <= maxSpeed)
            {
                //Add force Left
                RB2D.velocity = new Vector2(RB2D.velocity.x - speed, RB2D.velocity.y);
                Anim.SetTrigger("Run");
            }
            if (facingRight == true)
            {
                facingRight = false;
                if (Anim != null && willFlip == true)
                {
                    Anim.SetBool("IsRight", facingRight);
                }
                
            }
        }

        //Going right
        if (Input.GetKey(right))
        {
            //Play Respective Creatures Walk Sound
            if (isActive && currentCreature == CurrentCharacter.Indi && !isWalking && isGrounded)
            {
                AudioManager.instance.PlaySound("Indi_WalkCycle");
                isWalking = true;
            }
            else if (isActive && !isWalking && currentCreature == CurrentCharacter.Jebsee && isGrounded)
            {
                AudioManager.instance.PlaySound("Jebsee_WalkCycle");
                isWalking = true;
            }

            //Check Max Speed
            if (Mathf.Abs(RB2D.velocity.x) <= maxSpeed)
            {
                //Add force Right
                RB2D.velocity = new Vector2(RB2D.velocity.x + speed, RB2D.velocity.y);
                Anim.SetBool("Run", true);
            }
            if (facingRight == false)
            {
                facingRight = true;
                if (Anim != null && willFlip == true)
                {
                    Anim.SetBool("IsRight", facingRight);
                }
            }
        }

        //Stopping
        if (!Input.GetKey(right) && !Input.GetKey(left) && stopForce != 0)
        {
            RB2D.velocity = new Vector2(RB2D.velocity.x / stopForce, RB2D.velocity.y);

            //Stop playing respective creatures walking sound
            if (isWalking && currentCreature == CurrentCharacter.Indi)
            {
                AudioManager.instance.StopSound("Indi_WalkCycle");
            }

            if (isWalking && currentCreature == CurrentCharacter.Jebsee)
            {
                AudioManager.instance.StopSound("Jebsee_WalkCycle");
            }
            Anim.SetBool("Run", false);
            isWalking = false;
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

            Anim.SetBool("Jump", true);

            //Plays respective creatures jump sound
            if (isActive && currentCreature == CurrentCharacter.Indi)
            {
                if (isWalking)
                {
                    AudioManager.instance.StopSound("Indi_WalkCycle");
                }
                AudioManager.instance.PlaySound("Indi_Jump");
            }
            else if (isActive && currentCreature == CurrentCharacter.Jebsee)
            {
                if(isWalking)
                {
                    AudioManager.instance.StopSound("Jebsee_WalkCycle");
                }
                AudioManager.instance.PlaySound("Jebsee_Jump");
            }
        }

        

        //Jump cooldown
        if (isGrounded == false && RB2D.velocity.y <= 0.0005 && RB2D.velocity.y >= -0.0005)
        {
                //Stop playing respective creatures walking sound
                if (currentCreature == CurrentCharacter.Indi && !soundPlaying)
                {
                    AudioManager.instance.PlaySound("Indi_Land");
                 soundPlaying = true;
                }

                if (currentCreature == CurrentCharacter.Jebsee && !soundPlaying)
                {
                    AudioManager.instance.PlaySound("Jebsee_Landing");
                 soundPlaying = true;
                }

            jumpCoolDown -= Time.deltaTime;
            if (jumpCoolDown <= 0)
            {
                soundPlaying = false;
                isGrounded = true;
                isWalking = false;
                Anim.SetBool("Jump", false);
            }
        }
    }

    public void SetIsActive(bool set)
    {
        isActive = set;

        if(isActive == false)
        {
            if (isWalking && currentCreature == CurrentCharacter.Indi)
            {
                AudioManager.instance.StopSound("Indi_WalkCycle");
            }

            if (isWalking && currentCreature == CurrentCharacter.Jebsee)
            {
                AudioManager.instance.StopSound("Jebsee_WalkCycle");
            }
            isWalking = false;
        }
    }
}
