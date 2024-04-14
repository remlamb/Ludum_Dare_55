using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private float timer;
    [SerializeField] private float timeBeforeSwitch;

    [SerializeField] private int nextScene;

    [SerializeField] private GameObject ScoreUI;

    [SerializeField] private float SetupTime;
    [SerializeField] private float TotalGame;

    [SerializeField] private GameObject VictoryPanel;
    [SerializeField] private AudioSource VictorySound;

    [SerializeField] private float timeBeforeTransition;
    [SerializeField] private bool isInterlude;

    [SerializeField] private bool isLastLvl;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        if (!isInterlude)
        {
            ScoreUI.SetActive(false);
        }
        VictoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= timeBeforeSwitch)
        {
            if (!isInterlude)
            {
                if (!VictorySound.isPlaying)
                {
                    VictorySound.Play();
                }
                VictoryPanel.SetActive(true);

                GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Bird");
                foreach (GameObject projectile in projectiles)
                {
                    Destroy(projectile);
                }
            }
            if(!isLastLvl)
            {
                StartCoroutine(LoadNextLvl());
            }
            else
            {
                SceneManager.LoadScene("BadEnding");
            }
        }

        if (timer >= SetupTime)
        {
            if (!isInterlude)
            {
                ScoreUI.gameObject.SetActive(true);
                ScoreUI.gameObject.GetComponent<TextMeshProUGUI>().text = "Time: " + (TotalGame - ((int)timer - SetupTime)).ToString() + "s";
            }
        }
    }

    public void LoadLvl1()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void LoadLvl2()
    {
        SceneManager.LoadScene("Lvl2");
    }

    public void LoadLvl3()
    {
        SceneManager.LoadScene("Lvl3");
    }

    public void LoadInterlude1()
    {
        SceneManager.LoadScene("Interlude1");
    }

    public void LoadInterlude2()
    {
        SceneManager.LoadScene("Interlude2");
    }

    public void LoadInterlude3()
    {
        SceneManager.LoadScene("Interlude3");
    }

    IEnumerator LoadNextLvl()
    {
        yield return new WaitForSeconds(timeBeforeTransition);
        switch (nextScene)
        {
            case 0:
                break;
            case 1:
                LoadInterlude1();
                break;
            case 2:
                LoadLvl1();
                break;
            case 3:
                LoadInterlude2();
                break;
            case 4:
                LoadLvl2();
                break;
            case 5:
                LoadInterlude3();
                break;
            case 6:
                LoadLvl3();
                break;
            default:
                break;
        }
    }

}