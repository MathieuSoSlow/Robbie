using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterMarker : MonoBehaviour
{
    public BattleManager BattleManager { get; set; }

    public KeyCode Key { get; set; }

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

    void OnCollisionEnter2D(Collision2D col)
    {
        var go = col.gameObject;

        if (go.name == "PerfectMarker" && BattleManager != null)
        {
            BattleManager.letterMarkerGo = gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        var go = col.gameObject;
        if (go.name == "PerfectMarker" && BattleManager != null)
        {
            BattleManager.letterMarkerGo = null;
        }
    }
}
