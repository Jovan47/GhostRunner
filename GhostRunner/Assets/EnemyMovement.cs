using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool moved;
    private bool isHoping;

    //keep updated places around 
    private Vector3 left = new Vector3();
    private Vector3 right = new Vector3();
    private Vector3 up = new Vector3();
    private Vector3 down = new Vector3();
    private Vector3 minPath = new Vector3();
    private float jumpDistance = 4f;


    public Transform target;
    private Animator animator;
    private string directionMove;

    private bool goLeft  = false;
    private bool goRight = false;
    private bool goUp    = false;
    private bool goDown  = false;


    private float timeTick = 0.5f;
    private float timer;
    void Start()
    {
         moved = false;
        Vector3 nextPlace = new Vector3(0, 0, 0);
        Application.targetFrameRate = 70;
        animator = GetComponent<Animator>();
        StartTimer();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>= timeTick)
        {
            timer = 0;
            EnemyJump();
        }





        /*
        ShortestPath();
        if (moved && !isHoping)
        {
           
        }
       */
    }
    public void FinishedHopEnemy()
    {
        isHoping = false;
    }

    public void PossibleJumpPlaces()
    {
        this.left  = transform.position + new Vector3(0, 0, jumpDistance);
        this.right = transform.position + new Vector3(0, 0, -jumpDistance);
        this.up    = transform.position + new Vector3(jumpDistance,  0, 0);
        this.down  = transform.position + new Vector3(-jumpDistance, 0, 0);

    }

    //return ShortestPath where to jump
    public void ShortestPath()
    {
        PossibleJumpPlaces();

        float distanceLeft  =   Vector3.Distance(target.position, left);
        float distanceRight =   Vector3.Distance(target.position, right);
        float distanceUp    =   Vector3.Distance(target.position, up);
        float distanceDown  =   Vector3.Distance(target.position, down);

        float minDistance = Mathf.Min(distanceUp, distanceRight, distanceLeft, distanceDown);
        
        if (minDistance == distanceLeft)
        {
            minDistance = distanceLeft;
            minPath = left;
            goLeft = true;
            return;
        }
        if (minDistance == distanceRight)
        {
            minDistance = distanceRight;
            minPath = right;
            goRight = true;
            return;
        }
        if (distanceUp == minDistance)
        {
            minDistance = distanceUp;
            minPath = up;
            goUp = true;
            return;
        }
        if (distanceDown == minDistance)
        {
            minDistance = distanceDown;
            minPath = down;
            goDown = true;
            return;
        }

    }

    public void EnemyJump()
    {
        
       ShortestPath();
        

        if (goLeft)
        {
            goLeft = false;
            LeanTween.move(gameObject, left, 0.3f);
            animator.SetTrigger("hoped");
        }
        else if (goRight)
        {
            goRight = false;
            LeanTween.move(gameObject, right, 0.3f);
            animator.SetTrigger("hoped");
        }
        else if (goUp) 
        {
            goUp = false;
            LeanTween.move(gameObject, up, 0.3f);
            animator.SetTrigger("hoped");
        }
        else if (goDown)
        {
            goDown = false;
            LeanTween.move(gameObject, down, 0.3f);
            animator.SetTrigger("hoped");
        }
    }


    private void StartTimer()
    {
        timer = 0f;
    }
}