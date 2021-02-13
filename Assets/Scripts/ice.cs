using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ice : MonoBehaviour
{

    [SerializeField] Text debugText;
    [SerializeField] float kuculmeMiktari = 0.1f;
    [SerializeField] float iceSpeed = 10;
    [SerializeField] float iceLowSpeed = 5;
    float iceNormalSpeed;

    [SerializeField] Text startingText;
    [SerializeField] Text finishText;
    [SerializeField] Text gameOverText;
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button restartLevelButton;
    [SerializeField] ParticleSystem gameOverSmoke;
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject finishTextBck;
    [SerializeField] GameObject drops;

    LevelManager levelManager;
    DropScaleProccess dropScale;
   

    Vector3 startScale;

    bool isFinished = false;
    bool isVerticallyMovingAllowed = false;
    bool isHorizontalMovementAllowed = false;

    Vector3 minScale;
    [SerializeField] float minKuculme = 0.1f;
    float timeForLerping = 0;
    bool isScalingDown = false;

    static int number = 1;

    private void Start()
    {
        dropScale = FindObjectOfType<DropScaleProccess>();

        if (number==2)
        {
            startingText.gameObject.SetActive(true);
        }
        number++;

        minScale = new Vector3(transform.localScale.x, minKuculme, transform.localScale.z);

        levelManager = FindObjectOfType<LevelManager>();

        startScale = transform.localScale;
        iceNormalSpeed = iceSpeed;
        normalHorizontalSpeed = horizontalSpeed;

        isGameOver = false;

    }

    void Update()
    {
        if (isGameOver==false)
        {
            HorizontalMovementProcess();

            if (isScalingDown) { ScaleDownLerp(); }

            VerticalMovementProcess();

            if (isVerticallyMovingAllowed) { VerticalMovement(); }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                LevelCompleteProccess();
                LoadNextLevel();
            }
        }
       

        //debugText.text = icePos.ToString();

        //if (wallleftBool)
        //{
        //    if (icePos < 400)
        //    {
        //        horizontalSpeed = 0;
        //    }
        //    else
        //    {
        //        horizontalSpeed = normalHorizontalSpeed;
        //        wallleftBool = false;
        //    }
        //}

        //if (wallRightBool)
        //{
        //    if (icePos > 400)
        //    {
        //        horizontalSpeed = 0;
        //    }
        //    else
        //    {
        //        horizontalSpeed = normalHorizontalSpeed;
        //        wallRightBool = false;
        //    }
        //}

    }

    private void VerticalMovementProcess()
    {
        if (Input.touchCount > 0 && isFinished == false && isGameOver == false)
        {
            startingText.enabled = false;
            isVerticallyMovingAllowed = true;
            isHorizontalMovementAllowed = true;
        }
    }


    float icePos;
    [SerializeField] float horizontalSpeed = 10;
    [SerializeField] float lowHorizontalSpeed = 5;
    float normalHorizontalSpeed = 10;


    private void HorizontalMovementProcess()
    {
        if (isHorizontalMovementAllowed)
        {

            //icePos = (Input.mousePosition.x / Screen.width *10);


            //float step = horizontalSpeed* Time.deltaTime;
            //Vector3 target = new Vector3(transform.position.x, transform.position.y, icePos);
            //transform.position = Vector3.MoveTowards(transform.position,target , step);
            if (Input.touchCount > 0)
            {
                icePos = Input.GetTouch(0).position.x;
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float x = transform.position.z + touch.deltaPosition.x * horizontalSpeed;
                    float y = Mathf.Clamp(x, -4, 7.2f);
                    transform.position = new Vector3(transform.position.x, transform.position.y, y);

                    //debugText.text = y.ToString();

                }
            }
        }
    }
    bool isGameOver = false;
    private void ScaleDownLerp()
    {

        timeForLerping += Time.deltaTime * kuculmeMiktari;
        transform.localScale = Vector3.Lerp(startScale, minScale, timeForLerping);

        if (timeForLerping >= 1)  ///////        GAME OVER!!!
        {
            isGameOver = true;
            iceSpeed = 0;
            isHorizontalMovementAllowed = false;
            isVerticallyMovingAllowed = false;
            gameOverSmoke.Play();
            StartCoroutine(ShowGameOverScreen());
        }
    }

    IEnumerator ShowGameOverScreen()
    {
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(.8f);
        gameOverText.gameObject.SetActive(true);
        restartLevelButton.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void VerticalMovement()
    {
        transform.Translate(Vector3.right * Time.deltaTime*-iceSpeed);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isFinished==false)
        {
            if (collision.gameObject.tag == "obstacle")
            {
                iceSpeed = iceLowSpeed;
                horizontalSpeed = lowHorizontalSpeed;
                isScalingDown = true;
                dropScale.isOnObstacle = true;
            }
        }
       
    }

    

    private void OnCollisionExit(Collision collision)
    {
        if (isFinished==false)
        {
            isScalingDown = false;

            iceSpeed = iceNormalSpeed;
            horizontalSpeed = normalHorizontalSpeed;
        }

        if (collision.gameObject.tag== "obstacle")  /// drops
        {
             dropScale.isOnObstacle = false;
            //drops.SetActive(false);
        }

    }

    bool wallleftBool = false;
    bool wallRightBool = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wallLeft")
        {
            horizontalSpeed = 0;
            wallleftBool = true;
        }

        if (collision.gameObject.tag == "wallRight")
        {
            horizontalSpeed = 0;
            wallRightBool = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            isFinished = true;
            iceSpeed = 0;
            kuculmeMiktari = 0;
            isHorizontalMovementAllowed = false;
            finishText.gameObject.SetActive(true);  ////////////// LEVEL COMPLETE !!!!!!!
            finishTextBck.SetActive(true);

            LevelCompleteProccess();

            nextLevelButton.gameObject.SetActive(true);
        }
    }

    public void LoadNextLevel()
    {
        

        if (SceneManager.GetActiveScene().buildIndex + 1<SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0); // tutorial leveli olan sıfırıncı leveli tekrar oynatıyor
        }
    }
    
    void LevelCompleteProccess()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            levelManager.SetCurrentLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            levelManager.SetCurrentLevel(0); // tutorial level bir daha yükleniyor
        }
    }
}
