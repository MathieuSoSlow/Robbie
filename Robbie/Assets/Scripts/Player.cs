using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BattleManager battleManager;


    [SerializeField] private float currentBattery;
    [SerializeField] private float maximumBattery;

    // Start is called before the first frame update
    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            battleManager.AttackEnemy(KeyCode.Q);
        else if (Input.GetKeyDown(KeyCode.W))
            battleManager.AttackEnemy(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.E))
            battleManager.AttackEnemy(KeyCode.E);
        else if (Input.GetKeyDown(KeyCode.R))
            battleManager.AttackEnemy(KeyCode.R);
    }

    public bool Defend(float damage)
    {
        currentBattery -= damage;
        if (currentBattery <= 0)
            return true;
        return false;

    }
}
