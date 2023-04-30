using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
 

public class GameController : MonoBehaviour
{

    public Camera MainCamera;
    public GameObject CreaturePrefab;
    public GeneralAudioManager AudioManager;

    public Text DeadCreaturesText;
    public Text DeliveredCreaturesText;

    private int deadCreatures;
    private int deliveredCreatures;

    private Player player;
    private GameObject creature;

    private void Start() 
    {
        AudioManager        = GetComponent<GeneralAudioManager>();
        player              = GameObject.FindWithTag("Player").GetComponent<Player>();

        startGame();
    }
    
    // Update is called once per frame
    void Update()
    {

        updateDeadCreaturesText();
        updateDeliveredCreaturesText();
    }

    

    private void startGame()
    {
        deadCreatures       = 0;
        deliveredCreatures  = 0;
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

    public void WinGame() 
    {
    }

    public void CreatureDied()
    {
        deadCreatures += 1;
    }

    public void CreatureDelivered()
    {
        deliveredCreatures += 1;
    }
}
