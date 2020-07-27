using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    public bool indiHere;
    public bool jebseeHere;

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
        if (collision.gameObject.GetComponent<BasicCharacterControler>())
        {
            if (collision.gameObject.GetComponent<BasicCharacterControler>().currentCreature == BasicCharacterControler.CurrentCharacter.Indi)
            {
                indiHere = true;
            }
            else if (collision.gameObject.GetComponent<BasicCharacterControler>().currentCreature == BasicCharacterControler.CurrentCharacter.Jebsee)
            {
                jebseeHere = true;
            }

            if (indiHere && jebseeHere)
            {
                AudioManager.instance.PlaySound("Obj_Exit");
                AudioManager.instance.StopSound("Music_Gameplay");
                FindObjectOfType<PlayerUI>().LevelComplete();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BasicCharacterControler>())
        {
            if (collision.gameObject.GetComponent<BasicCharacterControler>().currentCreature == BasicCharacterControler.CurrentCharacter.Indi)
            {
                indiHere = false;
            }
            else if (collision.gameObject.GetComponent<BasicCharacterControler>().currentCreature == BasicCharacterControler.CurrentCharacter.Jebsee)
            {
                jebseeHere = false;
            }
        }
    }
}
