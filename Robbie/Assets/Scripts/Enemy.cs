using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<KeyCode> lifePoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Defend(KeyCode kc)
    {
        if (lifePoints.Count == 0)
            return true;
        if (lifePoints.First() == kc)
        {
            lifePoints.RemoveAt(0);
            return true;
        }

        return false;
    }

    void RemoveLastLifePoint()
    {
        if (lifePoints.Count > 0)
            lifePoints.RemoveAt(lifePoints.Capacity - 1);
    }
}
