using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameObject levelReference = null;
    public GameObject player;
    public TrailRenderer playerTrail;
    public Camera mainCam;
    public CinemachineVirtualCamera vCam;
    public TransitionText transitionText;
    public Timer timer;
    public GameObject pauseMenu;
    public GameObject curtain;
    public Transform levelAnchorPre;
    public Transform levelAnchorCur;
    public Transform levelAnchorNex;
    public int curLevel = 1;
    public int levelCount;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        levelCount = SceneManager.sceneCountInBuildSettings-2;
        LoadScene(curLevel+1);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) StartCoroutine(NextLevel());
    }

    public IEnumerator NextLevel()
    {
        if (curLevel == 6)
        {
            StartCoroutine(ToWinScene());
            yield break;
        }
        StartCoroutine(transitionText.FadeIn(curLevel));
        timer.Pause();
        // disable player control
        LeanTween.move(levelReference, levelAnchorNex, 3f);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Animator>().speed = 0;
        player.GetComponentInChildren<TrailRenderer>().enabled = false;

        vCam.Follow = null;
        player.transform.SetParent(levelReference.transform);
        
        curLevel++;
        LoadScene(curLevel+1);
        yield return new WaitForSeconds(3f);
        vCam.Follow = levelAnchorCur;
        player.transform.position = levelReference.transform.position;
        player.transform.SetParent(levelReference.transform);
        UnloadScene(curLevel);
    }

    public void LoadScene(int index)
    {
        if (index > levelCount) return;
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void UnloadScene(int index)
    {
        if (index == 0 || index == levelCount) return;
        SceneManager.UnloadSceneAsync(index);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // find reference for new level
        FindLevelReference();
        // update info of new level
        Level level = levelReference.GetComponent<Level>();
        if(GetComponent<AudioSource>().clip!=level.clip)
        {
            GetComponent<AudioSource>().clip = level.clip;
            GetComponent<AudioSource>().Play();
        }
        mainCam.backgroundColor = level.backgroundColor;
        timer.SetTimeLimit(level.timeLimit);
        timer.ResetTimer();

        player.GetComponent<PlayerMovement>().currentLayer = 0;
        player.GetComponent<SpriteRenderer>().sortingOrder = 2;
        player.transform.GetChild(0).GetComponent<TrailRenderer>().sortingOrder = 2;
        PauseMenu.Instance.ResetVolume();

        pauseMenu.transform.position = levelReference.GetComponent<Level>().pauseMenuAnchor.position;
        if(curLevel!=1)
        {
            // move new level to center
            levelReference.transform.position = levelAnchorPre.position;
            LeanTween.move(levelReference, levelAnchorCur, 5f);
            StartCoroutine(OnTransitionComplete());
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator OnTransitionComplete()
    {
        // give back player control
        yield return new WaitForSeconds(5f);
        player.transform.SetParent(transform);
        vCam.Follow = player.transform;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Animator>().speed = 1;
        player.GetComponentInChildren<TrailRenderer>().enabled = true;
        timer.Resume();
    }

    public void ToMainMenu()
    {
        StartCoroutine(ToMainMenuCoroutine());
    }

    IEnumerator ToMainMenuCoroutine()
    {
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 1f, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(0);
    }

    public void ReloadLevel()
    {
        StartCoroutine(ReloadLevelCoroutine());
    }

    IEnumerator ReloadLevelCoroutine()
    {
        bool npcTalked = levelReference.GetComponent<Level>().npc.talked;
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 1f, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync(curLevel + 1);
        SceneManager.LoadSceneAsync(curLevel+1, LoadSceneMode.Additive);
        GetComponent<GameStateManager>().ResetGameState();
        timer.ResetTimer();
        timer.Resume();
        playerTrail.enabled = false;
        player.transform.position = levelAnchorCur.position;
        playerTrail.enabled = true;
        player.GetComponent<Animator>().SetTrigger("Respawn");

        player.GetComponent<PlayerMovement>().currentLayer = 0;
        player.GetComponent<SpriteRenderer>().sortingOrder = 2;
        player.transform.GetChild(0).GetComponent<TrailRenderer>().sortingOrder = 2;

        Hook[] hookList = GameObject.FindObjectsOfType<Hook>();
        foreach(Hook h in hookList)
            h.UnUse();

        yield return new WaitForSeconds(0.1f);
        FindLevelReference();
        levelReference.GetComponent<Level>().npc.talked = npcTalked;
        if (npcTalked) levelReference.GetComponent<Level>().npc.DisableTalk();

        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 0f, 1f);
    }

    private void FindLevelReference()
    {
        foreach (GameObject levelRef in GameObject.FindGameObjectsWithTag("Level Reference"))
        {
            if (levelRef.name == "Level " + curLevel)
            {
                levelReference = levelRef;
                break;
            }
        }
    }

    IEnumerator ToWinScene()
    {
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 1f, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(8);
    }
}
