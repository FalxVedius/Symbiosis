﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] BasicCharacterControler[] allCharacters;
    int index = 0;
    [SerializeField] KeyCode foward, back;

    private void Start()
    {
        //Set all characters to false
        for (int i = 0; i < allCharacters.Length; i++)
        {
            allCharacters[i].SetIsActive(false);
        }
        //Set active character on
        allCharacters[0].SetIsActive(true);
    }
    private void Update()
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
    }
}