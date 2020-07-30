using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    Rigidbody2D body2D;

    bool isSliding;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Land();
    }

    public void Sliding()
    {
        if (body2D.velocity.y <= 0.0005 && body2D.velocity.y >= -0.0005)
        {
            if (body2D.velocity.x >= 0.0005 && !isSliding)
            {
                AudioManager.instance.PlaySound("Obj_CrateSlide");
                isSliding = true;
            }
            else if (body2D.velocity.x == 0)
            {
                isSliding = false;
                AudioManager.instance.StopSound("Obj_CrateSlide");
            }
        }
        else
        {
            isSliding = false;
            AudioManager.instance.StopSound("Obj_CrateSlide");
        }

        Debug.Log(body2D.velocity.x);
    }

    public void Land()
    {
        if(body2D.velocity.y >= 0.0005)
        {
            isGrounded = false;
        }

        if (body2D.velocity.y <= 0.0005 && body2D.velocity.y >= -0.0005 && !isGrounded)
        {
            AudioManager.instance.PlaySound("Obj_CrateLand");
            isGrounded = true;
        }
    }

}
