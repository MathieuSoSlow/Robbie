using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test");
        StartCoroutine(DisplayText(other));
    }

    public IEnumerator DisplayText(Collider2D other)
    {
        other.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        other.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);

    }
}
