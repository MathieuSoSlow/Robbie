using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterMarker : MonoBehaviour
{
    public float speed;

    public BattleManager battleManager;

    public KeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunMarkerCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RunMarkerCoroutine()
    {
        var animation = GetComponent<Animation>();
        animation["Marker"].speed = speed;

        var lag = 1/speed;
        
        animation.Play("Marker");
        Debug.Log(lag);
        yield return new WaitForSeconds(animation["Marker"].length * lag);
        battleManager.markerCounter--;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var go = col.gameObject;

        if (go.name == "PerfectMarker" && battleManager != null)
        {
            battleManager.letterMarkerGo = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var go = col.gameObject;
        if (go.name == "PerfectMarker" && battleManager != null)
        {
            battleManager.letterMarkerGo = null;
        }
    }
}
