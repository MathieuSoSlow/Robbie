using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public float letterSpeed;
    public float timeBetweenLetters;

    private GameObject robotGo;
    private Robot robotScript;
    private Animator robotAnimator;

    private object walkingMutex = new object();
    [SerializeField] private bool mouseIsWalking = false;
    private Vector3 enemyPosition = new Vector3(5f, -2.5f);

    [SerializeField] private GameObject mousePrefab;
    [SerializeField] private int enemyCount;
    private int currentEnemyCount;

    [SerializeField] private int scoreBoardValue;
    [SerializeField] private GameObject scoreBoardGo;
    private TextMeshPro scoreBoard;

    private GameObject selectedEnemyGo;
    private Enemy selectedEnemyScript;
    private Animation selectedEnemyAnimation;

    public GameObject hitMarkerGo;
    public GameObject letterMarkerGo;

    public GameObject prefabDropDownCounter;
    
    public GameObject prefabF;
    public GameObject prefabG;
    public GameObject prefabH;
    public GameObject prefabJ;

    public int markerCounter = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartLevel(3, 2, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        lock(walkingMutex)
            if (mouseIsWalking)
                return;
        CheckAttack();
        if (markerCounter == 0)
        {
            markerCounter = -1;

            StartCoroutine(DeathAnimationCoroutine());
        }
    }

    void StartLevel(int enemyNumber, float speed, float time)
    {

        enemyCount = enemyNumber;
        letterSpeed = speed;
        timeBetweenLetters = time;

        AddScore(0);
        robotGo = GameObject.Find("Robot");
        robotScript = robotGo.GetComponent<Robot>();
        robotAnimator = robotGo.GetComponent<Animator>();

        selectedEnemyGo = null;
        selectedEnemyScript = null;
        markerCounter = -1;
        StartCoroutine(DropDownCounterCoroutine());
    }

    private void NextEnemy()
    {
        if (currentEnemyCount < enemyCount)
        {
            selectedEnemyGo = Instantiate(mousePrefab);
            selectedEnemyScript = selectedEnemyGo.GetComponent<Enemy>();
            selectedEnemyAnimation = selectedEnemyGo.GetComponent<Animation>();
            currentEnemyCount++;
            StartCoroutine(EnterSceneCoroutine());
        }
        else
        {
            SceneManager.LoadScene("brand-new-wold");
        }
    }

    private IEnumerator DropDownCounterCoroutine()
    {
        var counterGo = Instantiate(prefabDropDownCounter);
        var animation = counterGo.GetComponent<Animation>();

        for (int i = 3; i > 0; i--)
        {
            animation.Play("DropDown");
            counterGo.GetComponent<TextMeshPro>().text = i.ToString();
            yield return new WaitForSeconds(animation["DropDown"].length + 0.15f);
        }
        Destroy(counterGo);
        NextEnemy();
    }

    private IEnumerator DeathAnimationCoroutine()
    {
        if (selectedEnemyAnimation != null)
        {
            selectedEnemyAnimation.Play("Death");
            yield return new WaitForSeconds(selectedEnemyAnimation["EnterScene"].length);
            Destroy(selectedEnemyGo);
            NextEnemy();
        }
    }

    private IEnumerator EnterSceneCoroutine()
    {
        lock(walkingMutex)
            mouseIsWalking = true;
        var animation = selectedEnemyGo.GetComponent<Animation>();
        animation.Play("EnterScene");
        yield return new WaitForSeconds(animation["EnterScene"].length);
        lock (walkingMutex)
            mouseIsWalking = false;
        StartCoroutine(RunEnemyCoroutine());
    }

    private IEnumerator RunEnemyCoroutine()
    {
        var toSend = selectedEnemyScript.lifePoints;
        markerCounter = toSend.Count;

        foreach (var keyCode in toSend)
        {
            GameObject currentMarkerGo = null;
            switch (keyCode)
            {
                case KeyCode.F:
                    currentMarkerGo = Instantiate(prefabF);
                    break;
                case KeyCode.G:
                    currentMarkerGo = Instantiate(prefabG);
                    break;
                case KeyCode.H:
                    currentMarkerGo = Instantiate(prefabH);
                    break;
                case KeyCode.J:
                    currentMarkerGo = Instantiate(prefabJ);
                    break;
            }
            if (currentMarkerGo != null)
            {
                var letterMarker = currentMarkerGo.GetComponent<LetterMarker>();
                letterMarker.battleManager = this;
                letterMarker.speed = letterSpeed;
            }

            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }

    private void CheckAttack()
    {
        var isAttack = true;
        var keyCode = KeyCode.F;

        if (Input.GetKeyDown(KeyCode.F))
            keyCode = KeyCode.F;
        else if (Input.GetKeyDown(KeyCode.G))
            keyCode = KeyCode.G;
        else if (Input.GetKeyDown(KeyCode.H))
            keyCode = KeyCode.H;
        else if (Input.GetKeyDown(KeyCode.J))
            keyCode = KeyCode.J;
        else
            isAttack = false;

        if (isAttack)
            AttackEnemy(keyCode);
    }

    private void AttackEnemy(KeyCode keyCode)
    {
        if (letterMarkerGo == null)
        {
            AddScore(-1);
            return;

        }

        if (letterMarkerGo.GetComponent<LetterMarker>().key == keyCode)
        {

            if (Vector3.Distance(letterMarkerGo.transform.position, hitMarkerGo.transform.position) <
                hitMarkerGo.GetComponent<BoxCollider2D>().size.x / 2)
                AddScore(10);
            else
                AddScore(5);

            markerCounter--;
            Destroy(letterMarkerGo);
            letterMarkerGo = null;
        }
        else
        {
            AddScore(-1);
        }
    }

    public void AttackPlayer(float damage)
    {
        //RobotScript.Defend(damage);
    }

    void AddScore(int toAdd)
    {
        scoreBoardValue += toAdd;
        scoreBoardGo.GetComponent<TextMeshPro>().text = "score: " + scoreBoardValue;
    }

    public void ChangeDifficulty(int diff)
    {

    }
}
