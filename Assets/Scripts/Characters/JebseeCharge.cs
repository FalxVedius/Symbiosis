﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JebseeCharge : MonoBehaviour
{
    BasicCharacterControler jebseeController;

    enum DashDirection { Left, Right, NoDirection}
    private DashDirection dashDirection;

    Rigidbody2D body;

    public float dashSpeed;
    public float dashDuration;
    public float dashTimer;

    public bool isCharging;

    // Start is called before the first frame update
    void Start()
    {
        jebseeController = gameObject.GetComponent<BasicCharacterControler>();
        body = GetComponent<Rigidbody2D>();
        dashDirection = DashDirection.NoDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (jebseeController.isActive)
        {
            if (Input.GetKey(jebseeController.left))
            {
                dashDirection = DashDirection.Left;
            }

            if (Input.GetKey(jebseeController.right))
            {
                dashDirection = DashDirection.Right;
            }

            if(dashDirection != DashDirection.NoDirection)
            {
                if(dashTimer >= dashDuration)
                {
                    dashDirection = DashDirection.NoDirection;
                    dashTimer = 0;
                    body.velocity = Vector2.zero;
                    isCharging = false;
                }
                else
                {
                    
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isCharging = true;
                    }

                    if (isCharging)
                    {
                        dashTimer += Time.deltaTime;
                        Charge();
                    }
                }
            }

            
        }
    }

    public void Charge()
    {
        if (dashDirection == DashDirection.Left)
        {
            body.velocity = Vector2.left * dashSpeed;
        }

        if (dashDirection == DashDirection.Right)
        {
            body.velocity = Vector2.right * dashSpeed;
        }

        
    }
}
