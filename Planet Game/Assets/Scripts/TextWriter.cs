﻿using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI uiText;
    private int characterIndex;
    private string textToWrite;
    private float timePerCharacter;
    private float timer;
    private bool invisibleCharacters;

    public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,bool invisibleCharacters)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        characterIndex = 0;
    }

    private void Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                //Display next character
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                
                uiText.text = text;

                if (characterIndex >= textToWrite.Length)
                {
                    //Entire string displayed
                    uiText = null;
                    return;
                }
            }
        }
    }

    public TextMeshProUGUI UIText => uiText;
}
