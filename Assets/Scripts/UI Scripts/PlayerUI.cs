using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject winMenu;
    [SerializeField] CharacterSwap characterManager;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound("Music_Gameplay");

        characterManager = FindObjectOfType<CharacterSwap>();
    }

    public void LevelComplete()
    {
        winMenu.SetActive(true);

        characterManager.DisableAllCharacters();
    }

}
