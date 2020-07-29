using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BasicCharacterControler>() && !isActivated)
        {
            Debug.Log("Activate Lever");
            isActivated = true;
            AudioManager.instance.PlaySound("Obj_LeverActivate");
        }
    }
}
