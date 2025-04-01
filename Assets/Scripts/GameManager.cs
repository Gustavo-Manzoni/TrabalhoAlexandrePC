using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> hearts;
    public static GameManager instance;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject layoutToInstantiateHeart;
    // Start is called before the first frame update
    void Awake()
    {
       instance = this;
    }

   public void LessHeart()
    {
        StartCoroutine(hearts[0].GetComponent<UiHeart>().Fade(0.1f));
        hearts.RemoveAt(0);
        
    }
    public void AddHeart()
    {
        GameObject heartInstance = Instantiate(heartPrefab, layoutToInstantiateHeart.transform);
        hearts.Insert(0, heartInstance);
    }
}
