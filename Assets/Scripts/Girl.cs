using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using DG.Tweening;

public class Girl : MonoBehaviour
{
    public CanvasGroup endBG;

    SpriteRenderer spriteRend;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    [YarnCommand("move")]
    public IEnumerator Move()
    {
        spriteRend.enabled = true;
        transform.DOMoveX(2, 5);
        yield return new WaitForSeconds(5);
    }

    [YarnCommand("fin")]
    public void Fin()
    {
        endBG.DOFade(1, 5);
        //yield return new WaitForSeconds(5);
        //while (true)
        //{
        //    yield return null;
        //}
    }
}
