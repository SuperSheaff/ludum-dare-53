using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonPlant : MonoBehaviour
{

    public GameObject MelonPrefab;
    public float timeToSpawn = 30f; // The time in seconds before a new melon can spawn
    private bool hasMelon = true; // True if the plant has a melon available to be taken

    private float timer = 0f;

    private Melon currentMelon;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the initial melon
        SpawnMelon();
    }

    // Update is called once per frame
    void Update()
    {
        // If the plant does not currently have a melon available and enough time has passed, spawn a new melon
        if (!hasMelon && timer >= timeToSpawn)
        {
            SpawnMelon();
            hasMelon = true;
            timer = 0f;
        }
        else
        {
            // Increment the timer
            timer += Time.deltaTime;
        }
    }

    public void SpawnMelon()
    {
        if (currentMelon == null)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x - 14f, transform.position.y + 4f, transform.position.z);

            GameObject newMelon = Instantiate(MelonPrefab, spawnPosition + Vector3.up, Quaternion.identity);
            currentMelon = newMelon.GetComponent<Melon>();
            currentMelon.SetMelonPlant(this);
        }
    }

    // Called when a melon is taken from the plant
    public void MelonTaken()
    {
        currentMelon = null;
        hasMelon = false;
    }
}
