using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TextFadeAway : MonoBehaviour
{
    public float fadeAwayTime = 5f;

    private Text textComp;
    private string previousText;
    private string currentText;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        textComp = this.GetComponent<Text>();
        currentText = textComp.text;
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        textComp.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        previousText = currentText;
        currentText = textComp.text;

        if (!previousText.Equals(currentText)) // when there is new text pop out - let it disappear after 5 secs
        {

            coroutine = WaitAndPrint(fadeAwayTime);
            StartCoroutine(coroutine);
        }
    }
}
