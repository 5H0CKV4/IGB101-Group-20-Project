using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_ManhoC : MonoBehaviour
{
    public GameObject player;
    private PlayerScript playerScript;
    private CharacterController characterController;

    public GameObject exitObj;
    private LevelSwitch_ManhoC levelSwitch;

    public GameObject deathAreaObj;
    private FallDeathCheck fallDeathCheck;

    public Text pickupText;

    public int currentPickUps;
    public int maxPickUps;
    public bool levelComplete;

    public Transform spawnPosition;

    public AudioSource[] audioSources;
    public float audioProximity = 5.0f;

    public Animator headAnimator;


    [Header("Player_ManhoC")]
    public bool switchPlayerCharacter;
    public GameObject player_ManHoCHUI;
    public GameObject mainCamera;
    public GameObject cameraFreeLook;

    // Start is called before the first frame update
    void Start()
    {
        if (switchPlayerCharacter)
        {
            player_ManHoCHUI.SetActive(true);
            player.SetActive(false);
            cameraFreeLook.SetActive(true);
            mainCamera.GetComponent<ThirdPersonCamera>().enabled = false;
            mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
            player = player_ManHoCHUI;
        }
        player.transform.position = spawnPosition.position;

        characterController = player.GetComponent<CharacterController>();

        playerScript = player.GetComponent<PlayerScript>();
        playerScript.OnItemPickedUp += UpdateNumOfPickUp;


        levelSwitch = exitObj.GetComponent<LevelSwitch_ManhoC>();
        levelSwitch.OnPlayerEnterExit += SwitchScene;

        fallDeathCheck = deathAreaObj.GetComponent<FallDeathCheck>();
        fallDeathCheck.OnEnterFallDeathArea += RespawnPlayer;

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


    private void UpdateGUI()
    {
        pickupText.text = "Pickups: " + currentPickUps + "/" + maxPickUps;
    }

    private void PlayAudioSamples()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i] == null)
                return;

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
