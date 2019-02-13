using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Robot player;

    private Enemy selectedEnemy;
    private Queue<Enemy> enemies;

    [SerializeField] private float tempo;
    [SerializeField] private float timer;

    [SerializeField] private float halfWindow;
    [SerializeField] private float perfectHalfWindow;

    [SerializeField] private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Robot>();
        enemies = new Queue<Enemy>();
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
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Enqueue(enemy);
    }

    private void CheckAttack()
    {
        var isAttack = true;
        var keyCode = KeyCode.Q;

        if (Input.GetKeyDown(KeyCode.Q))
            keyCode = KeyCode.Q;
        else if (Input.GetKeyDown(KeyCode.W))
            keyCode = KeyCode.W;
        else if (Input.GetKeyDown(KeyCode.E))
            keyCode = KeyCode.E;
        else if (Input.GetKeyDown(KeyCode.R))
            keyCode = KeyCode.R;
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
            if (selectedEnemy.Defend(ak))
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
        player.Defend(damage);
    }

}
