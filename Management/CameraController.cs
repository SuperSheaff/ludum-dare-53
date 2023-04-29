using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator ShakeTheCamera (float duration, float magnitude) 
    {

       float elapsed = 0.0f;

       while (elapsed < duration) 
       {
           float x = Random.Range(-1f, 1f) * magnitude;
           float y = Random.Range(-1f, 1f) * magnitude;

           this.transform.position = new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

           yield return null;
       }

       this.transform.position = new Vector3(0f, 0f, 0f);
    }

}
