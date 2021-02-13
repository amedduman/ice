using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameObject startText;
    static LevelManager instance;
    
    [SerializeField] GameObject startCanvas;
    private void Awake()
    {
        //LevelManager[] levelManagers = FindObjectsOfType<LevelManager>();
        //if (levelManagers.Length > 1)
        //{

        //    Destroy(gameObject);


        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);

        //startText = GameObject.FindGameObjectWithTag("star text");
        //startText.transform.GetChild(0).gameObject.SetActive(true);
        //print("HERE! " + startText.transform.GetChild(0).gameObject);
        //}
        

        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {

        // print(PlayerPrefs.GetInt("CurrentLevel"));
        if (PlayerPrefs.GetInt("CurrentLevel")!=0)
        {
            LoadLevel(PlayerPrefs.GetInt("CurrentLevel",0));
        }
        LoadLevel(PlayerPrefs.GetInt("CurrentLevel"));
    }
    
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void SetCurrentLevel(int currentLevel)
    {
       PlayerPrefs.SetInt("CurrentLevel",currentLevel);
    }
}
