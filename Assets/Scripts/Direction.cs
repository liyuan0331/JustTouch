using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : YaSingleMono<Direction>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
        gameObject.transform.localScale = player.transform.localScale;
        ShowTarget();

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    
    public Transform player;
    GameObject theNearOne;

    void ShowTarget()
    {
        OtherPlayer[] otherPlayers = FindObjectsOfType<OtherPlayer>();

        if (otherPlayers.Length <= 0) return;
        for (int i = 0; i < otherPlayers.Length; i++)
        {
            if (i == 0)
            {
                theNearOne = otherPlayers[i].gameObject;
            }
            else
            {
                if (Mathf.Abs(Vector2.Distance(gameObject.transform.position, otherPlayers[i].transform.position)) < Mathf.Abs(Vector2.Distance(gameObject.transform.position, theNearOne.transform.position)))
                {
                    theNearOne = otherPlayers[i].gameObject;
                }
            }
        }

        transform.LookAt(theNearOne.transform);

    }

}
