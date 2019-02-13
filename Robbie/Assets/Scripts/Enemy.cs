using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<KeyCode> lifePoints;
    [SerializeField] private float damage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsAlive()
    {
        return lifePoints.Count > 0;
    }

    public bool Defend(KeyCode kc)
    {
        if (!IsAlive())
            return true;
        
        if (lifePoints.First() == kc)
        {
            lifePoints.RemoveAt(0);
            return false;
        }

        return false;
    }

    public void RemoveLastLifePoint()
    {
        if (lifePoints.Count > 0)
            lifePoints.RemoveAt(lifePoints.Capacity - 1);
    }
}
