using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private List<Enemy> enemy;
    private Player player;

    [SerializeField] private float tempo;
    [SerializeField] private float timer;

    [SerializeField] private float halfWindow;
    [SerializeField] private float perfectHalfWindow;

    [SerializeField] private bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.fixedDeltaTime;

        if (timer > tempo)
        {
            canAttack = true;
            timer = 0;
        }
    }

    public void AttackEnemy(KeyCode ak)
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

        }

        if (currentTime > lowerBound && currentTime > higherBound)
        {
            canAttack = false;
        }
    }

    public void AttackPlayer(float damage)
    {

    }

}
