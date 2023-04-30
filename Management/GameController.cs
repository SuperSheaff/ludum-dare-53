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

    private Player player;
    private GameObject creature;

    private void Start() 
    {
        AudioManager    = GetComponent<GeneralAudioManager>();
        player          = GameObject.FindWithTag("Player").GetComponent<Player>();

        // creature = Resources.Load("Creature") as GameObject;
        // Instantiate(creature, transform.position, transform.rotation, transform);
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    private void startGame()
    {
        // StartNewDive();
    }

    public void WinGame() 
    {
    }
}
