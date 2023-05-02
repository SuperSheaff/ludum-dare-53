using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
 

public class GameController : MonoBehaviour
{

    public GameObject[] StorySlides;
    public GameObject GameWinScreen;
    public GameObject GameLoseNoneScreen;
    public GameObject GameLoseDeadScreen;

    public Camera MainCamera;
    public GameObject CreaturePrefab;
    public GameObject CreaturesContainer;
    public GeneralAudioManager AudioManager;

    public Transform creatureStartSpawn;
    public Transform playerStartSpawn;

    public Text DeadCreaturesText;
    public Text DeliveredCreaturesText;

    private int deadCreatures;
    private int deliveredCreatures;
    private int countingMsg = 1;

    private float timerStartTime;
    private bool isIntro;
    private bool hasWon;
    private bool hasLost;

    private Player player;
    private GameObject creature;
    private DeliverySpot deliverySpot;

    private void Start() 
    {
        AudioManager        = GetComponent<GeneralAudioManager>();
        player              = GameObject.FindWithTag("Player").GetComponent<Player>();
        deliverySpot        = GameObject.FindWithTag("Delivery").GetComponent<DeliverySpot>();

        isIntro = true;
        hasWon  = false;
        hasLost  = false;
        AudioManager.PlaySound("music");
        // StartGame();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (deliveredCreatures >= 10)
        {
            WinGame();
        }

        if (deadCreatures >= 3  && !hasLost)
        {
            LoseDeadGame();
        }

        if (CreaturesContainer.transform.childCount <= 0 && !isIntro && !hasWon)
        {
            AudioManager.PlaySound("loseNone");
            LoseNoneGame();
        }

        if (isIntro)
        {
            if (countingMsg > 24)
            {
                isIntro = false;
                StartGame();
            }
            if (Input.GetKeyDown("space"))
            {
                for (int i = 0; i < StorySlides.Length; i++) 
                {
                    StorySlides[i].SetActive(false);
                }

                StorySlides[countingMsg].SetActive(true);
                countingMsg++;
            }
        }

        updateDeadCreaturesText();
        updateDeliveredCreaturesText();
    }

    public void StartGame()
    {
        deadCreatures       = 0;
        deliveredCreatures  = 0;
        hasWon              = false;
        hasLost             = false;

        player.transform.position = playerStartSpawn.position;

        for (int i = 0; i < StorySlides.Length; i++) 
        {
            StorySlides[i].SetActive(false);
        }

        foreach (Transform child in CreaturesContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }

        ClearScreens();
        ResetTimer();
        deliverySpot.SetDelivered(0);
        Instantiate(CreaturePrefab, creatureStartSpawn.position, creatureStartSpawn.rotation, CreaturesContainer.transform);
    }

    private void updateDeadCreaturesText() 
    {
        string stashString = "Dead:" + deadCreatures;

        DeadCreaturesText.text  = stashString;
    }

    private void updateDeliveredCreaturesText() 
    {
        string stashString = "Delivered:" + deliveredCreatures;

        DeliveredCreaturesText.text  = stashString;
    }

    public void ClearScreens() 
    {
        GameWinScreen.SetActive(false);
        GameLoseDeadScreen.SetActive(false);
        GameLoseNoneScreen.SetActive(false);

        for (int i = 0; i < StorySlides.Length; i++) 
        {
            StorySlides[i].SetActive(false);
        }
    }
    

    public void WinGame() 
    {
        
        hasWon = true;
        ClearScreens();
        foreach (Transform child in CreaturesContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }

        GameWinScreen.SetActive(true);
    }

    public void LoseDeadGame() 
    {
        hasLost = true;

        ClearScreens();
        foreach (Transform child in CreaturesContainer.transform) { 
            GameObject.Destroy(child.gameObject);
        }

        GameLoseDeadScreen.SetActive(true);
    }

    public void LoseNoneGame() 
    {
        ClearScreens();
        foreach (Transform child in CreaturesContainer.transform) { 
            GameObject.Destroy(child.gameObject);
        }
        GameLoseNoneScreen.SetActive(true);
    }

    public void CreatureDied()
    {
        deadCreatures += 1;
    }

    public void CreatureDelivered()
    {
        deliveredCreatures += 1;

        deliverySpot.SetDelivered(deliveredCreatures);
    }

    public void ResetTimer()
    {
        timerStartTime = Time.time;
    }
}
