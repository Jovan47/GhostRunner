using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;
    [SerializeField] private float offSetPlayerMove = 4;
    [SerializeField] private float rayReach = 4;
    private bool moved;
    private bool isHoping;
    private Vector3 nextPlace;
    private Vector3 currentDiretcion;
    private Animator animator;
    public GameObject MapSpawner = null;
    private MapSpawner scriptRef = null;

    private string directionMove;

    void Start()
    {
        scriptRef = MapSpawner.GetComponent<MapSpawner>();
        currentDiretcion = new Vector3(0, 0, 0);
        moved = false;
        Vector3 nextPlace = new Vector3(0, 0, 0);
        Application.targetFrameRate = 70;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w") && !isHoping)
        {
            nextPlace = transform.position + new Vector3(offSetPlayerMove, 0, 0);
            currentDiretcion = (nextPlace - transform.position).normalized;
            moved = true;
            directionMove = "up";
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a") && !isHoping)
        {
            nextPlace = transform.position + new Vector3(0, 0, offSetPlayerMove);
            currentDiretcion = (nextPlace - transform.position).normalized;
            moved = true;
            directionMove = "left";
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s") && !isHoping)
        {
            nextPlace = transform.position + new Vector3(-offSetPlayerMove, 0, 0);
            currentDiretcion = (nextPlace - transform.position).normalized;
            moved = true;
            directionMove = "down";

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d") && !isHoping)
        {
            nextPlace = transform.position + new Vector3(0, 0, -offSetPlayerMove);
            currentDiretcion = (nextPlace - transform.position).normalized;
            moved = true;
            directionMove = "right";


        }
        //nextPlace.x <= maxX && nextPlace.x >= 0 && nextPlace.z <= maxZ && nextPlace.z >= 0
        if (  moved && !isHoping)
        {
            LeanTween.move(gameObject, nextPlace, 0.28f);
            animator.SetTrigger("hop");
            isHoping = true;
            moved = false;

            scriptRef.MoveTiles(directionMove);
        }

        RaycastHit hit;
        Debug.DrawRay(nextPlace + 2 * currentDiretcion, currentDiretcion * rayReach, Color.red);
        if (Physics.Raycast(nextPlace + 2 * currentDiretcion, currentDiretcion * rayReach, out hit))
        {
            if (hit.transform.tag == "Tile")
            {
                //Debug.Log("Udarili smo u " + hit.transform.tag + " at position " +hit.transform.position);
            }
        }
        else
        { //Debug.Log("Nema dalje"); }

        }

    }
        public void FinishedHop()
        {
            isHoping = false;
        }
}