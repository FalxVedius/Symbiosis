using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverSwitch : MonoBehaviour
{
    bool isActivated;
    Animator leverAnim;

    [Header("Insert Function")]
    public UnityEvent onActivation;
    
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
            ActivateLever();
        }
    }

    public void ActivateLever()
    {
        if (!isActivated)
        {
            onActivation.Invoke();

            leverAnim.SetBool("isActivated", true);
            isActivated = true;
            AudioManager.instance.PlaySound("Obj_LeverActivate");
        }
    }

}
