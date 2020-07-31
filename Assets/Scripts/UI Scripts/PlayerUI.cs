using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject finishMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] CharacterSwap characterManager;
    public string levelNum;
    public Text characterText;
    [SerializeField] Text levelNumText;
    [SerializeField] bool lastLevel;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound("Music_Gameplay");
        characterManager = FindObjectOfType<CharacterSwap>();
        levelNumText.text = "Escape Attempt # " + levelNum;
    }

    private void Update()
    {
        if(characterText != null && characterManager != null)
        characterText.text = "Current Character: " + characterManager.selectedCharacter.currentCreature;
    }

    public void LevelComplete()
    {
        if (!lastLevel)
        {
            finishMenu.SetActive(true);
        }
        else
        {
            winMenu.SetActive(true);
        }

        characterManager.DisableAllCharacters();
    }

}
