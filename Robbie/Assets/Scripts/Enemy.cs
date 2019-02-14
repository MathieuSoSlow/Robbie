using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hardness;
    [SerializeField] private int lifeCount;
    [SerializeField] public Queue<KeyCode> lifePoints;
    [SerializeField] private float damage;

    // Start is called before the first frame update
    void Start()
    {
        lifePoints = new Queue<KeyCode>();
        for (int i = 0; i < lifeCount; i++)
        {
            var rand = Random.Range(0, hardness);
            switch (rand)
            {
                case 0:
                    lifePoints.Enqueue(KeyCode.F);
                    break;
                case 1:
                    lifePoints.Enqueue(KeyCode.G);
                    break;
                case 2:
                    lifePoints.Enqueue(KeyCode.H);
                    break;
                case 3:
                    lifePoints.Enqueue(KeyCode.J);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsAlive()
    {
        return lifePoints.Any();
    }

    public bool Defend(KeyCode kc)
    {
        if (!IsAlive())
            return true;
        
        if (lifePoints.Peek() == kc)
        {
            lifePoints.Dequeue();
            return false;
        }

        return false;
    }
}
