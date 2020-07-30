using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWalls : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<JebseeCharge>())
        {
            if (collision.gameObject.GetComponent<JebseeCharge>().isCharging)
            {
                Debug.Log("Broke Wall");
                AudioManager.instance.PlaySound("Obj_WallBreak");
                Destroy(gameObject);
            }
        }
    }
}
