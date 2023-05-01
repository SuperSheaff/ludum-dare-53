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
    public GameObject CreaturesContainer;
    public GeneralAudioManager AudioManager;

    public Transform creatureStartSpawn;
    public Transform playerStartSpawn;

    public Text DeadCreaturesText;
    public Text DeliveredCreaturesText;

    private int deadCreatures;
    private int deliveredCreatures;

    private Player player;

    private GameObject creature;
    private DeliverySpot deliverySpot;

    private void Start() 
    {
        AudioManager        = GetComponent<GeneralAudioManager>();
        player              = GameObject.FindWithTag("Player").GetComponent<Player>();
        deliverySpot        = GameObject.FindWithTag("Delivery").GetComponent<DeliverySpot>();

        AudioManager.PlaySound("music");
        startGame();
    }
    
    // Update is called once per frame
    void Update()
    {

        updateDeadCreaturesText();
        updateDeliveredCreaturesText();

        if (deliveredCreatures >= 10)
        {
            WinGame();
        }

         if (deadCreatures >= 3)
        {
            LoseGame();
        }

        if (CreaturesContainer.transform.childCount <= 0)
        {
            Instantiate(CreaturePrefab, creatureStartSpawn.position, creatureStartSpawn.rotation, CreaturesContainer.transform);
        }
    }

    

    private void startGame()
    {
        deadCreatures       = 0;
        deliveredCreatures  = 0;

        player.transform.position = playerStartSpawn.position;
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

    public void WinGame() 
    {
        Debug.Log("Game Won");
    }

    public void LoseGame() 
    {
        Debug.Log("Game Over");
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
}
