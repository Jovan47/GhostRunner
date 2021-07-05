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
    private float jumpDistance = 4;


    public Transform target;
    private Animator animator;
    private string directionMove;

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
    public string ShortestPath()
    {
        PossibleJumpPlaces();

        float distance =0;
        float minDistance = 100000;
        distance= Vector3.Distance(left, transform.position);
        if (distance < minDistance)
        {
            minPath = left;
            return "left";
        }
        distance = Vector3.Distance(right, transform.position);
        if (distance > minDistance)
        {
            minPath = right;
            return "right";

        }
        distance = Vector3.Distance(up, transform.position);
        if (distance > minDistance)
        {
            minPath = up;
            return "up";

        }
        distance = Vector3.Distance(down, transform.position);
        if (distance > minDistance)
        {
            minPath = down;
            return "down";
        }
        return null;
    }

    public void EnemyJump()
    {
        string dirr = " ";
        directionMove= ShortestPath();

        if (dirr == "left")
        {
            LeanTween.move(gameObject, left, 0.3f);
            animator.SetTrigger("hop");
        }
        else if (dirr == "right")
        {
            LeanTween.move(gameObject, right, 0.3f);
            animator.SetTrigger("hop");
        }
        else if (dirr == "up") 
        {
            LeanTween.move(gameObject, up, 0.3f);
            animator.SetTrigger("hop");
        }
        else if (dirr == "down")
        {
            LeanTween.move(gameObject, down, 0.3f);
            animator.SetTrigger("hop");
        }
    }


    private void StartTimer()
    {
        timer = 0f;
    }
}