using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class GameUI : YaSingleMono<GameUI> {
    enum State{ Title,Gaming,End}
    State state = State.Title;

    public bool userControl=false;

	public Text textAllMoney;
	public Text textPlayerMoney;

	MePlayer mePlayer;

	public GameObject gameOver;
	public GameObject gameWin;

    public CanvasGroup titleUI;

    public DialogueRunner dialogueRunner;

    public MePlayer player;

    public GameObject[] prefabLevels;
    //public Transform levelContainer;

    int curLevel = 1;

	// Use this for initialization
	void Start () {
		mePlayer = Object.FindObjectOfType<MePlayer> ();

        curLevel = PlayerPrefs.GetInt("curLevel", 0);
        if (curLevel == 0)
        {
            StartCoroutine(TitleWaiting());
        }
        else
        {
            titleUI.alpha = 0;
            StartCoroutine(ReadyToStartLevel(curLevel));
        }
	}
	
    IEnumerator TitleWaiting()
    {
        state = State.Title;
        userControl = false;
        //等待任意键按下
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        titleUI.DOFade(0, 3f);
        yield return new WaitForSeconds(3f);
        //titleUI.interactable = false;
        //titleUI.blocksRaycasts = false;

        //userControl = true;
        //StartCoroutine(ShowFirstDialogue());

        curLevel = 1;
        StartCoroutine(ReadyToStartLevel(curLevel));

        //dialogueRunner.StartDialogue("END");

        yield break;
    }



    IEnumerator ReadyToStartLevel(int level)
    {
        //Time.timeScale = 0;
        mePlayer.transform.localScale = Vector3.one* 1.128379f;
        userControl = false;
        mePlayer.natureMoney = 1;
        mePlayer.borrowMoney = 0;
        mePlayer.myRig2D.velocity = Vector2.zero;
        mePlayer.moveRig2D.velocity = Vector2.zero;
        mePlayer.transform.position = Vector3.zero;
        
        GameObject levelStuff = GameObject.Instantiate(prefabLevels[level - 1]);
        //levelStuff.transform.parent = levelContainer;

        dialogueRunner.StartDialogue("Level"+level);
        while(dialogueRunner.isDialogueRunning)
        {
            yield return null;
        }

        state = State.Gaming;

        userControl = true;
        //Time.timeScale = 1;
        yield break;
    }

	// Update is called once per frame
	void Update () {
        if (state == State.End) return;

        if (state == State.Gaming)
        {
            OtherPlayer[] otherPlayers = FindObjectsOfType<OtherPlayer>();
            if (otherPlayers.Length==0)
            {
                curLevel++;
                if (curLevel >= 4)
                {
                    //Time.timeScale = 0;
                    mePlayer.transform.localScale = Vector3.one * 1.128379f;
                    userControl = false;
                    mePlayer.natureMoney = 1;
                    mePlayer.borrowMoney = 0;
                    mePlayer.myRig2D.velocity = Vector2.zero;
                    mePlayer.moveRig2D.velocity = Vector2.zero;
                    mePlayer.transform.position = Vector3.zero;

                    PlayerPrefs.SetInt("curLevel", 0);
                    dialogueRunner.StartDialogue("END");
                    state = State.End;
                }
                else
                {
                    PlayerPrefs.SetInt("curLevel", curLevel);
                    StartCoroutine(ReadyToStartLevel(curLevel));
                }
            }
        }

		textAllMoney.text = "总金额: " + MoneySystem.Instance.allMoney.ToString("f2");
		if (mePlayer == null || mePlayer.isActiveAndEnabled == false) {
			textPlayerMoney.text = "玩家额度: 无";
		} else {
			textPlayerMoney.text = "玩家额度: " + (mePlayer.lentMoney + mePlayer.borrowMoney + mePlayer.natureMoney).ToString("f2");
		}
	}

	public void GameOver(){
		gameWin.SetActive (false);
		gameOver.SetActive (true);
	}

	public void GameWin(){
		
		gameOver.SetActive (false);
		gameWin.SetActive (true);
	}

    public void Continue()
    {
        //PlayerPrefs.SetInt("curLevel", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart(){
        //		Application.LoadLevel (Application.loadedLevel);
        PlayerPrefs.SetInt("curLevel", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        //Continue();
	}
}
