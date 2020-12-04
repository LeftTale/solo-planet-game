using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextDirector : MonoBehaviour
{
    public TextWriter textWriter;
    private TextMeshProUGUI messageText;
    private TextMeshProUGUI deathMessage;
    private TextMeshProUGUI scannerMessage;
    private TextMeshProUGUI gameMessage;
    private TextMeshProUGUI statusMessage;
    private TextMeshProUGUI scoreText;
    public PlayerMovement playerMovement;
    
    //                                                           //   
    //Multiple Lists containing the text used throughout the game//
    //                                                           //       
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
        "Avery fell onto Spikes. Avery Died.",
        " "
    };

    private List<string> scannerText = new List<string>()
    {
        "PlanetID: E9435  \nGeography: Green  \nIntelligent life: Somewhat  \nCulture: Mostly Awful \nVerdict: Uninhabitable ",
        "PlanetID: R6734  \nGeography: Lava and Fire  \nIntelligent life: I hope not \nCulture: Hotheaded \nVerdict: Uninhabitable ",
        "PlanetID: N1322  \nGeography: Radiated Wasteland \nIntelligent life: Possibly \nCulture: Mostly mutants \nVerdict: Uninhabitable ",
        "PlanetID: M5733  \nGeography: Tons of water \nIntelligent life: Fish? \nCulture: Aquamen \nVerdict: Uninhabitable ",
        "PlanetID: F5884  \nGeography: Rock \nIntelligent life: Doubt it \nCulture: Its a rock \nVerdict: Uninhabitable ",
        "PlanetID: X1113  \nGeography: Spongy \nIntelligent life: Unlikely \nCulture: Active \nVerdict: Uninhabitable ",
        "PlanetID: V0034  \nGeography: Ice and snow  \nIntelligent life: Obnoxiously \nCulture: Snowmen \nVerdict: Uninhabitable  ",
        "PlanetID: LN420  \nGeography: Sunny and warm \nIntelligent life: Bearable \nCulture: Relaxing \nVerdict: Inhabitable"
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
        "Status: \nLow Levels of Gravity",
        "Status: \nSuprising level of Buoyancy",
        "Status: \nSub Zero Temperature"

    };

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        deathMessage = GameObject.Find("DeathText").GetComponent<TextMeshProUGUI>();
        if (sceneName != "IntroLevel")
        {
            scannerMessage = GameObject.Find("ScannerText").GetComponent<TextMeshProUGUI>();

            gameMessage = GameObject.Find("GameText").GetComponent<TextMeshProUGUI>();

            statusMessage = GameObject.Find("StatusBarBG").transform.Find("StatusText").gameObject
                .GetComponent<TextMeshProUGUI>();
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            messageText = GameObject.Find("CutSceneText").GetComponent<TextMeshProUGUI>(); 
        }
    }

    private void Update()
    {
        if(scoreText != null)
            scoreText.text = "Score: " + GameManager.Score;
    }

    //Text used in opening cutscene
    public void SendText(int textCount)
    {
        textWriter.AddWriter(messageText, displayText[textCount], .1f, true);
    }

    //Text used upon player death
    public void SendDeathText(int deathScenario)
    {
        if (deathScenario == 5)
        {
            deathMessage.text = null;
        }
        else
        {
            textWriter.AddWriter(deathMessage,loseText[deathScenario],.03f,true);

        }
    }

    //Text used in second level opening
    public void SendGameText(int gameScenario)

    {
        textWriter.AddWriter(gameMessage, gameText[gameScenario],.03f,true );
    }
    
    public void Sendlvl1EndText()
    {
        textWriter.AddWriter(deathMessage, "Mission Goal: Find a habitable planet",.1f,true);

    }
    //Text used for the scanner 
    public void SendScannerMessage(int planetID)
    {
        textWriter.AddWriter(scannerMessage,scannerText[planetID],.05f,true);
    }
    //Text used for the status
    public void SendStatusMessage(int statusNUM)
    {
        statusMessage.text = statusText[statusNUM];
    }

    //Clears text
    public void TextVoid()
    {
        textWriter.AddWriter(messageText," ",.01f,true);
    }

    public void GameVoid()
    {
        textWriter.AddWriter(gameMessage," ",.01f,true);
    }

   

}

