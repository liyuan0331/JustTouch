using UnityEngine;
using System.Collections;

public class OtherPlayerLRMove : OtherPlayer {
	enum State{right,left,up,down}
	State myState=State.right;

	protected override void Awake ()
	{
		base.Awake ();
		maxSpeed = 1.5f;

        //		acc = .3f;
        StartCoroutine(Wandering());
	}

    IEnumerator Wandering(bool isrunAwayTo = false,bool isChasing = false)
    {
        while (Vector2.Distance(gameObject.transform.position,realPlayer.transform.position) > 4f)
        {
            while (!GameUI.Instance.userControl)
            {
                yield return null;
            }
            Vector2 randMove;
            if (isrunAwayTo)
            {
                randMove = transform.parent.position - realPlayer.transform.position;
            }
            else if (isChasing)
            {
                randMove = realPlayer.transform.position - transform.parent.position;
            }
            else
            {
                randMove = new Vector2(Random.Range(-1, 1f), (Random.Range(-1, 1f)));
            }
            isrunAwayTo = false;
            isChasing = false;
            float speed = 10f;

            float totalTime = 5f;
            float sumTime = 0;
            while ((sumTime += Time.deltaTime) < totalTime && Vector2.Distance(gameObject.transform.position, realPlayer.transform.position) > 4f)
            {
                if (sumTime <= 0.5f)
                {
                    speed = 0;
                    speed += 10 * Time.deltaTime; 
                }
                else if (sumTime >= totalTime - 0.5f)
                    speed -= speed * Time.deltaTime;
                else
                    speed = 10;
                PlayerMoveDir(randMove.normalized * speed);
                yield return new WaitForEndOfFrame();
            }
            //yield return new WaitForSeconds(Random.Range(.5f, 1.5f));
            //PlayerMoveTowards(Vector2.zero);
            //yield return new WaitForSeconds(Random.Range(.5f, 3));

            float waitTimer = 0;
            while (waitTimer < 0.8f && Vector2.Distance(gameObject.transform.position, realPlayer.transform.position) > 4f)
            {
                yield return new WaitForEndOfFrame();
                waitTimer += Time.deltaTime;
            }
            //yield return new WaitForSeconds(0.5f);
        }
        StopAllCoroutines();
        if (natureMoney > realPlayer.natureMoney)
            StartCoroutine(Chasing());
        else
            StartCoroutine(RunAway());
        yield break;
    }

    IEnumerator RunAway()
    {
        while (Vector2.Distance(gameObject.transform.position, realPlayer.transform.position) <=4f)
        {
            while (!GameUI.Instance.userControl)
            {
                yield return null;
            }
            Vector2 m = transform.parent.position - realPlayer.transform.position;
            float speed = 12f;

            PlayerMoveDir(m.normalized * speed);

            yield return new WaitForEndOfFrame();
        }
        StopAllCoroutines();
        StartCoroutine(Wandering(true));
        yield break;
    }

    IEnumerator Chasing()
    {
        while (Vector2.Distance(gameObject.transform.position, realPlayer.transform.position) <= 7f)
        {
            while (!GameUI.Instance.userControl)
            {
                yield return null;
            }
            Vector2 m = realPlayer.transform.position - transform.parent.position;
            float speed = 11f;

            float totalTime = 2f;
            float sumTime = 0;
            while ((sumTime += Time.deltaTime) < totalTime && Vector2.Distance(gameObject.transform.position, realPlayer.transform.position) <= 7f) 
            {
                PlayerMoveDir(m.normalized * speed);
                yield return new WaitForEndOfFrame();
            }
            
            yield return new WaitForEndOfFrame();
        }
        StopAllCoroutines();
        StartCoroutine(Wandering(false,true));
        yield break;
    }

    // Update is called once per frame
    protected override void Update(){
		base.Update ();

        //switch (myState) {
        //case State.right:
        //	if (!PlayerMoveTowards (Vector3.right)) {
        //		myState = State.left;
        //	}

        //	break;
        //case State.left:
        //	if (!PlayerMoveTowards (Vector3.left)) {
        //		myState = State.right;
        //	}
        //	break;
        //}

        //if (Vector2.Distance(gameObject.transform.position,realPlayer.transform.position) <= 1f)
        //{
        //    switch (myState)
        //    {
        //        case State.right:
        //            break;
        //        case State.left:
        //            break;
        //        case State.up:
        //            break;
        //        case State.down:
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //else
        //{

        //}
	}
}
