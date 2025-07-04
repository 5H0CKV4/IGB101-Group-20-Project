using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    // Pick up and level completion Logic
    public int currentPickups = 0;
    public int maxPickups = 5;
    public bool levelComplete = false;

    public Text pickupText;

    private void UpdateGUI(){
        pickupText.text = "Pickups:" + currentPickups + "/" + maxPickups;
    }

    //Audio Proximity  Logic
    public AudioSource[] audioSources;
    public float audioProximity = 5.0f;

    //Loop for playing audio proximity events - AudioSource based
    private void PlayAudioSamples()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] != null)
            {
                if (Vector3.Distance(player.transform.position, audioSources[i].transform.position) <= audioProximity)
                {
                    if (!audioSources[i].isPlaying)
                    {
                        audioSources[i].Play();
                    }
                }
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelCompleteCheck();
        UpdateGUI();
        PlayAudioSamples();
    }
    private void LevelCompleteCheck()
    {
        if (currentPickups >= maxPickups)
            levelComplete = true;
        else
            levelComplete = false;
    }
}
