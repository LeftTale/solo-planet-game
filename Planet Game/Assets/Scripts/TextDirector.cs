using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDirector : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private TextMeshProUGUI messageText;
    private TextMeshProUGUI deathMessage;
    private TextMeshProUGUI scannerMessage;
    private TextMeshProUGUI gameMessage;
    private TextMeshProUGUI statusMessage;
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
        "Avery was devoured by an alien lifeform",
        "Avery was struck by an asteroid and obliterated",
        "Avery fell onto Spikes. Avery Died."
    };

    private List<string> scannerText = new List<string>()
    {
        "Name:  \nType:  \nLength of year:  \nFun fact:  \nSize:  ",
        "Name:  \nType:  \nLength of year:  \nFun fact:  \nSize:  ",
        "Name:  \nType:  \nLength of year:  \nFun fact:  \nSize:  ",
        "Name:  \nType:  \nLength of year:  \nFun fact:  \nSize:  ",
        "Name:  \nType:  \nLength of year:  \nFun fact:  \nSize:  ",
        "Name:  \nType:  \nLength of year:  \nFun fact:  \nSize:  "
    };

    private List<string> gameText = new List<string>()
    {
        "Explore all planets in this solar system \nUse your mouse and left click to move between planets with your boost \nRemember Space is dangerous"
    };

    private List<string> statusText = new List<string>()
    {
        "Status: \nNormal",
        "Status: \nExtreme Temperatures",
        "Status: \nHigh Levels of Raditation",
        "Status: \nHigh Water Levels",
        "Status: \nLow Levels of Gravity"

    };

    private void Start()
    {
        //messageText = GameObject.Find("CutSceneText").GetComponent<Text>();
        deathMessage = GameObject.Find("DeathText").GetComponent<TextMeshProUGUI>();

        scannerMessage = GameObject.Find("ScannerText").GetComponent<TextMeshProUGUI>();

        gameMessage = GameObject.Find("GameText").GetComponent<TextMeshProUGUI>();

        statusMessage = GameObject.Find("StatusBarBG").transform.Find("StatusText").gameObject
            .GetComponent<TextMeshProUGUI>();
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

    public void SendGameText(int gameScenario)

    {
        textWriter.AddWriter(gameMessage, gameText[gameScenario],.03f,true );
    }
    public void Sendlvl1EndText()
    {
        textWriter.AddWriter(deathMessage, "Mission Goal: Find a habitable planet",.1f,true);

    }

    public void SendScannerMessage(int planetID)
    {
        textWriter.AddWriter(scannerMessage,scannerText[planetID],.05f,true);
    }

    public void SendStatusMessage(int statusNUM)
    {
        statusMessage.text = statusText[statusNUM];
    }


    public void TextVoid()
    {
        textWriter.AddWriter(messageText," ",.01f,true);
        textWriter.AddWriter(gameMessage," ",.01f,true);
    }
}

