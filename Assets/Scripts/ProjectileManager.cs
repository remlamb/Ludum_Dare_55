using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private GameObject[] projectileSpawners;

    [SerializeField] private GameObject enemy;

    private Transform currentSpawner;
    [SerializeField] private float timerBetweenSpawn;
    private GameObject playerGO;

    private float timer;
    [SerializeField]  private float timeBeforeMovement;


    private float timerSetup;
    [SerializeField] private float setupTime;

    [SerializeField] private GameObject marker;
    [SerializeField] private GameObject TextTimer;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip audioCursed;

    [SerializeField] private GameObject OnStartWarning;
    private bool is10sPlayer = false;

    private float warningTimer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        timerSetup = 0.0f;
        warningTimer = 0.0f;
        playerGO = GameObject.FindGameObjectWithTag("Player");

        OnStartWarning.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerSetup += Time.deltaTime;
        if (OnStartWarning.activeSelf)
        {
            warningTimer += Time.deltaTime;
        }

        if (warningTimer >= 1.4f)
        {
            OnStartWarning.SetActive(false);
            warningTimer = 0.0f;
        }

        if (timerSetup > setupTime)
        {
            if (timer >= timerBetweenSpawn)
            {
                source.clip = audioCursed;
                if (!source.isPlaying)
                {
                    source.Play();
                }

                StartCoroutine(InstantiateNewProjectile());
                //TODO
                //currentEnemy.transform.LookAt(playerGO.transform);
                timer = 0.0f;
            }
            TextTimer.SetActive(false);
        }
        else
        {
            if(timerSetup > 25.0f && !is10sPlayer)
            {
                OnStartWarning.SetActive(true);
                OnStartWarning.GetComponentInChildren<TextMeshProUGUI>().text = "5s Before Disaster";
                is10sPlayer = true;
            }
        }
    }


    IEnumerator InstantiateNewProjectile()
    {
        currentSpawner = projectileSpawners[Random.Range(0, projectileSpawners.Length)].transform;
        GameObject currentEnemy = Instantiate(enemy, currentSpawner.position, Quaternion.identity);
        GameObject currentMarker = SetUpMarker(currentEnemy);
        yield return new WaitForSeconds(timeBeforeMovement);
        if (currentMarker != null)
        {
            Destroy(currentMarker);
        }
        currentEnemy.GetComponent<ProjectileController>().isMoving = true;
    }

    GameObject SetUpMarker(GameObject currentProjectile)
    {
        if (currentProjectile != null)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(currentProjectile.transform.position);
            if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)
            {
                // Positionnez le marqueur à la limite de l'écran
                Vector3 screenPos = new Vector3(Mathf.Clamp(viewportPos.x, 0.1f, 0.9f), Mathf.Clamp(viewportPos.y, 0.1f, 0.9f),
                    viewportPos.z);
                GameObject currentmarker = Instantiate(marker, Camera.main.ViewportToWorldPoint(screenPos), Quaternion.identity);

                marker.transform.position = Camera.main.ViewportToWorldPoint(screenPos);
                return currentmarker;
            }

            return null;
        }
        return null;
    }
}
