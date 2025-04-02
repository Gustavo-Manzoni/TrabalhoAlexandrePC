using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypeWritterEffect : MonoBehaviour
{

    [SerializeField] float cooldownForEachLetter;
    [SerializeField] float cooldownToStart;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Write());
    }
    IEnumerator Write()
    {
       
        TMP_Text text = GetComponent<TMP_Text>();
        char[] letters = text.text.ToCharArray();
        text.text = "";
        yield return new WaitForSeconds(cooldownToStart);
        for (int i = 0; i < letters.Length; i++)
        {
            text.text += letters[i];

            yield return new WaitForSeconds(cooldownForEachLetter);
        }

    }
}