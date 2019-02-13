using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private GameObject robotGo;
    private Robot robotScript;
    private Animator robotAnimator;


    private bool mouseIsWalking = false;
    private Vector3 enemyPosition = new Vector3(5f, -2.5f);

    [SerializeField] private GameObject mousePrefab;
    [SerializeField] private int enemyCount;
    private int currentEnemyCount;

    private GameObject selectedEnemyGo;
    private Enemy selectedEnemyScript;
    private Animator selectedEnemyAnimator;

    [SerializeField] private float tempo;
    [SerializeField] private float timer;

    [SerializeField] private float halfWindow;
    [SerializeField] private float perfectHalfWindow;

    [SerializeField] private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        robotGo = GameObject.Find("Robot");
        robotScript = robotGo.GetComponent<Robot>();
        robotAnimator = robotGo.GetComponent<Animator>();
        
        selectedEnemyGo = null;
        selectedEnemyScript = null;


        NextEnemy();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttack();

        timer += Time.fixedDeltaTime;

        if (timer > tempo)
        {
            canAttack = true;
            timer = 0;
        }

        if (mouseIsWalking)
        {
            Vector3.MoveTowards(selectedEnemyGo.transform.position, enemyPosition, 1);
            if (selectedEnemyGo.transform.position == enemyPosition)
            {
                mouseIsWalking = false;
            }
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
            mouseIsWalking = true;
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

    private void AttackEnemy(KeyCode ak)
    {
        var currentTime = timer;
        if (!canAttack)
            return;

        var halfTempo = tempo / 2;
        var lowerBound = halfTempo - halfWindow;
        var higherBound = halfTempo + halfWindow;

        var perfectLowerBound = halfTempo - perfectHalfWindow;
        var perfectHigherBound = halfTempo + perfectHalfWindow;

        if (currentTime > perfectLowerBound && currentTime < perfectHigherBound)
        {
            if (selectedEnemyScript.Defend(ak))
            {

            }
        }

        if (currentTime > lowerBound && currentTime > higherBound)
        {
            canAttack = false;
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
