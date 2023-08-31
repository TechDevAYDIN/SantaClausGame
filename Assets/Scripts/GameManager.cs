using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager Singleton;
    public static GameManager singleton
    {
        get
        {
            if (Singleton == null)
            {
                Singleton = GameObject.FindObjectOfType<GameManager>();

                if (Singleton == null)
                {
                    GameObject container = new GameObject("GM");
                    Singleton = container.AddComponent<GameManager>();
                }
            }
            return Singleton;
        }
    }
    #endregion
    [HideInInspector]
    public List<Ring> AllRings = new List<Ring>();
    public List<Transform> AllWaypoints;
    public bool isAlive = true;
    public bool canSpawnMan = false;

    bool tutorialTimer = false;
    int collectedRings = 0;
    int currentLevel = 0;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI levelTextFail;
    [SerializeField] TextMeshProUGUI ringsText;


    [SerializeField] MainLevelDesigner levelDesigner;

    [SerializeField] GameObject startBTN;
    [SerializeField] GameObject LevelStartCanvas, LevelPassedCanvas, LevelFailedCanvas, tutorialHolder, LoadingScreen;
    [SerializeField] Image loadingBar;

    [SerializeField] PlayerController playerController;
    [SerializeField] MovementController movementController;
    void Awake()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        levelText.text = (currentLevel + 1).ToString();
        levelTextFail.text = "Level " + (currentLevel + 1).ToString();
        levelDesigner.LoadLevel(currentLevel);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialTimer)
        {
            if (isAlive)
            {
                if (!Input.GetMouseButton(0) && !tutorialHolder.activeSelf)
                {
                    tutorialHolder.SetActive(true);
                }
                else if (Input.GetMouseButton(0))
                {
                    tutorialHolder.SetActive(false);
                }
            }
            else
            {
                tutorialHolder.SetActive(false);
            }
        }
        else if(!tutorialTimer && tutorialHolder.activeSelf)
        {
            tutorialHolder.SetActive(false);
        }
    }
    public void AddWaypoint(Transform waypoint)
    {
        if (!AllWaypoints.Contains(waypoint))
        {
            AllWaypoints.Add(waypoint);
        }
    }
    public void AddRing(Ring ring)
    {
        if (!AllRings.Contains(ring))
        {
            AllRings.Add(ring);
        }
        ringsText.text = collectedRings + " of " + AllRings.Count;
    }
    public void CollectRing(Transform chimneyPos)
    {
        if (isAlive)
        {
            collectedRings++;
            //AudioManager.singleton.PlayRingSound();
            AudioManager.singleton.PlayHoSound();
            playerController.ThrowGift(chimneyPos);
            ringsText.text = collectedRings + " of " + AllRings.Count;
        }
    }


    public void GoalReached()
    {
        if (isAlive)
        {
            isAlive = false;
            currentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);            
            StartCoroutine(OpenCanvas("Pass"));
        }
    }
    public void Death()
    {
        if (isAlive)
        {
            isAlive = false;
            CameraShaker.Instance.ShakeCam(5f, .5f);
            playerController.Death();
            movementController.isAlive = false;
            Camera.main.GetComponent<CameraFollow>().Death();
            AudioManager.singleton.PlayCrushSound();
            StartCoroutine(OpenCanvas("Fail"));            
        }
    }
    public void FearTrigger()
    {
        playerController.TriggerFear();
    }

    IEnumerator OpenCanvas(string CanvasName)
    {
        yield return new WaitForSeconds(3);
        if (CanvasName == "Fail")
            LevelFailedCanvas.SetActive(true);
        else if (CanvasName == "Pass")
            LevelPassedCanvas.SetActive(true);
    }
    public void StartGame()
    {
        startBTN.SetActive(false);
        movementController.isAlive = true;
        playerController.Started();
        LevelStartCanvas.SetActive(true);
        if(currentLevel <= 6)
        {
            tutorialTimer = true;
        }        
        StartCoroutine(EndTutorial());        
    }
    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(10);
        tutorialTimer = false;
    }
    public void RestartGame()
    {
        StartCoroutine(LoadSceneNotSync());
        //SceneManager.LoadSceneAsync(1);
    }
    public IEnumerator LoadSceneNotSync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainGame");
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);
            //loadingBar.fillAmount = progress;
            yield return null;
        }

    }
}
