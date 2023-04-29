using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
 

public class GameController : MonoBehaviour
{

    public Camera MainCamera;

    public GeneralAudioManager AudioManager;
    private Player player;

    private void Start() 
    {
        AudioManager    = GetComponent<GeneralAudioManager>();
        player          = GameObject.FindWithTag("Player").GetComponent<Player>();
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
