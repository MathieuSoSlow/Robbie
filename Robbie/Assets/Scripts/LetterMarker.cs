using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterMarker : MonoBehaviour
{
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
        animation.Play("Marker");
        yield return new WaitForSeconds(animation["Marker"].length);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("hihi");
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
