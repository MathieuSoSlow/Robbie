﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private GameObject robotGo;
    private Robot robotScript;
    private Animator robotAnimator;

    private object walkingMutex = new object();
    [SerializeField] private bool mouseIsWalking = false;
    private Vector3 enemyPosition = new Vector3(5f, -2.5f);

    [SerializeField] private GameObject mousePrefab;
    [SerializeField] private int enemyCount;
    private int currentEnemyCount;

    private GameObject selectedEnemyGo;
    private Enemy selectedEnemyScript;
    private Animator selectedEnemyAnimator;

    public GameObject hitMarkerGo;
    public GameObject letterMarkerGo;

    public GameObject prefabF;
    public GameObject prefabG;
    public GameObject prefabH;
    public GameObject prefabJ;

    public int markerCounter = 0;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        robotGo = GameObject.Find("Robot");
        robotScript = robotGo.GetComponent<Robot>();
        robotAnimator = robotGo.GetComponent<Animator>();

        
        selectedEnemyGo = null;
        selectedEnemyScript = null;

        NextEnemy();
        //var marker = GameObject.Find("MarkerF");
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
            Destroy(selectedEnemyGo);
            markerCounter = -1;
            NextEnemy();
        }
    }

    private void NextEnemy()
    {
        if (currentEnemyCount < enemyCount)
        {
            selectedEnemyGo = Instantiate(mousePrefab);
            selectedEnemyScript = selectedEnemyGo.GetComponent<Enemy>();
            selectedEnemyAnimator = selectedEnemyGo.GetComponent<Animator>();
            currentEnemyCount++;
            StartCoroutine(EnterSceneCoroutine());
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
                currentMarkerGo.GetComponent<LetterMarker>().battleManager = this;

            yield return new WaitForSeconds(1);
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
            return;

        if (letterMarkerGo.GetComponent<LetterMarker>().key == keyCode)
        {

            if (Vector3.Distance(letterMarkerGo.transform.position, hitMarkerGo.transform.position) <
                hitMarkerGo.GetComponent<BoxCollider2D>().size.x / 2)
                score += 1;

            score += 1;
            markerCounter--;
            Destroy(letterMarkerGo);
            letterMarkerGo = null;
        }
    }

    public void AttackPlayer(float damage)
    {
        //RobotScript.Defend(damage);
    }

    public void ChangeDifficulty(int diff)
    {

    }
}
