using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDirector : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private TextMeshProUGUI messageText;
    private TextMeshProUGUI deathMessage;
    private int textCount;

    private List<string> displayText = new List<string>()
        {
        "Amateur Astronaut Avery decided they would spend their morning browsing Switter", 
        "They then came to the conclusion that they did not want to live on this planet anymore"
        };

    private List<string> loseText = new List<string>()
    {
        "Avery failed the mission...Before it started",
        "Avery took a wrong turn and is now lost in space",
        "Avery was devoured by an alien lifeform"
    };
    
   

    private void Start()
    {
        //messageText = GameObject.Find("CutSceneText").GetComponent<Text>();
        deathMessage = GameObject.Find("DeathText").GetComponent<TextMeshProUGUI>();
    }


    public void SendText()
    {
        textWriter.AddWriter(messageText, displayText[textCount], .1f, true);
        textCount++;
    }

    public void SendDeathText(int deathScenario)
    {
        textWriter.AddWriter(deathMessage,loseText[deathScenario],.1f,true);
    }

    public void Sendlvl1EndText()
    {
        textWriter.AddWriter(deathMessage, "Mission Goal: Find a habitable planet",.1f,true);

    }
    


    public void TextVoid()
    {
        textWriter.AddWriter(messageText," ",.01f,true);
    }
}

