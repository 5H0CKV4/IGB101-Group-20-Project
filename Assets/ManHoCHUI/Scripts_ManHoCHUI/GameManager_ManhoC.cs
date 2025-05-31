using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager_ManhoC : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerScript;
    private CharacterController characterController;

    public GameObject exitObj;
    private LevelSwitch_ManhoC levelSwitch;

    public GameObject deathAreaObj;
    private FallDeathCheck fallDeathCheck;

    public GameObject tutTriggerBowMode;
    public GameObject tutTriggerDrawArrow;
    public TMP_Text tutText;

    public TMP_Text pickUpText;

    public int currentPickUps;
    public int maxPickUps;
    public bool levelComplete;

    public Transform spawnPosition;

    public AudioSource[] audioSources;
    public float audioProximity = 5.0f;

    public Animator headAnimator;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        playerScript.OnItemPickedUp += UpdateNumOfPickUp;
        characterController = player.GetComponent<CharacterController>();
        //playerScript.OnItemPickedUp += UpdateGUI;

        levelSwitch = exitObj.GetComponent<LevelSwitch_ManhoC>();
        levelSwitch.OnPlayerEnterExit += SwitchScene;

        fallDeathCheck = deathAreaObj.GetComponent<FallDeathCheck>();
        fallDeathCheck.OnEnterFallDeathArea += RespawnPlayer;



        player.transform.position = spawnPosition.position;

    }

    // Update is called once per frame
    void Update()
    {
        LevelCompletedCheck();
        UpdateGUI();
        PlayAudioSamples();
        
    }
    private void LevelCompletedCheck()
    {
        if (currentPickUps >= maxPickUps)
        {
            levelComplete = true;
            headAnimator.SetTrigger("LevelCompleted");
            
        } else {
            levelComplete = false;
        }
    }

    private void UpdateNumOfPickUp(object sender, EventArgs e)
    {
        currentPickUps = playerScript.numberOfPickUps;
        Debug.Log(currentPickUps);
    }

    private void SwitchScene(object sender, EventArgs e)
    {
        if (levelComplete)
        {
            SceneManager.LoadScene(levelSwitch.nextLevel);
        }
    }
    private void RespawnPlayer(object sender, EventArgs e)
    {

        characterController.enabled = false;
        player.transform.position = spawnPosition.position;
        Debug.Log("Player spawned at: " + spawnPosition.position);
        characterController.enabled = true;
    }

    private void UpdateGUITutTxtBowMode()
    {
        tutText.text = "Press 'Q' to enter Bow mode";
    }
    private void UpdateGUITutTxtDrawArrow()
    {
        tutText.text = "Hold 'Left mouse' to draw an arrow, release to shoot";
    }

    private void UpdateGUI()
    {
        pickUpText.text = "Pickups: " + currentPickUps + "/" + maxPickUps;
    }

    private void PlayAudioSamples()
    {
        for (int i = 0; i < audioSources.Length; i++)
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
