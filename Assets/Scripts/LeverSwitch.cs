using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LeverSwitch : MonoBehaviour
{
    bool isActivated;

    Animator leverAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        leverAnim = gameObject.transform.GetComponentInParent<Animator>();
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
            leverAnim.SetBool("isActivated", true);
            isActivated = true;
            AudioManager.instance.PlaySound("Obj_LeverActivate");
        }
    }

}
