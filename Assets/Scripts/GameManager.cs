using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> hearts;
    public static GameManager instance;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject layoutToInstantiateHeart;
    [SerializeField] float delayToChangeScene;
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
    public void ChangeSceneFunc(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }
    IEnumerator ChangeScene(string sceneName) 
    {
        yield return new WaitForSeconds(delayToChangeScene);
        SceneManager.LoadScene(sceneName);
    
    }
}
