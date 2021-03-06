﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public static CharacterSwap characterSwapInstance;
    [SerializeField] BasicCharacterControler[] allCharacters;

    public BasicCharacterControler selectedCharacter;

    int index = 0;
    [SerializeField] KeyCode foward, back;

    bool levelComplete;
    private void Awake()
    {
        allCharacters = BasicCharacterControler.FindObjectsOfType<BasicCharacterControler>();
        //Set all characters to false
        for (int i = 0; i < allCharacters.Length; i++)
        {
            allCharacters[i].SetIsActive(false);
        }
        //Set active character on
        allCharacters[0].SetIsActive(true);
        selectedCharacter = allCharacters[index];
    }
    private void Update()
    {
        if (characterSwapInstance != null && characterSwapInstance != this)
        {
            //Deletes the Script if another instance of the Character Swap already exist
            Destroy(this);
            return;
        }

        characterSwapInstance = this;

        Camera.main.transform.position = Vector2.Lerp(Camera.main.transform.position, allCharacters[index].transform.position, 0.125f);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);

        if (!levelComplete)
        {

                // Looking for input
                if (Input.GetKeyDown(foward))
                {
                    index++;
                    SwitchCharacter(index);
                    Debug.Log("Index = " + index);
                }

                if (Input.GetKeyDown(back))
                {
                    index--;
                    SwitchCharacter(index);
                }


            

        }
    }

    public void SwitchCharacter(int indx)
    {
        //put index back into the array if needed
        if (indx >= allCharacters.Length)
        {
            index = 0;
        }
        else if (indx < 0)
        {
            index = allCharacters.Length - 1;
        }

        //Set all characters to false
        for (int i = 0; i < allCharacters.Length; i++)
        {
            allCharacters[i].SetIsActive(false);
        }
        //Set active character on
        allCharacters[index].SetIsActive(true);

        selectedCharacter = allCharacters[index];
    }

    public void DisableAllCharacters()
    {
        levelComplete = true;

        for (int i = 0; i < allCharacters.Length; i++)
        {
            allCharacters[i].SetIsActive(false);
        }
    }
}
