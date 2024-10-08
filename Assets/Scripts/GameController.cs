using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System.Linq;

public class GameController : MonoBehaviour
{

    public TextMeshProUGUI disText;
    private LineRenderer lineRenderer;
    public GameObject[] pickUps;
    public GameObject pickUp;
    public float new_distance = 0;
    public float master_distance = 0;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        pickUps = GameObject.FindGameObjectsWithTag("PickUp"); //load prefabs from PlayerController

        // see if GameController has access to prefabs
        //for (int i = 0; i < pickUps.Length; i++)
        //{
        //    Debug.Log(pickUps[i].transform.position);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        objDistance();
    }


    private void objDistance()
    {
        Vector3 ball_loc = transform.position;


        for(int i = 0; i < pickUps.Length; i++)
        {
            new_distance = 0;
            //Debug.Log(i);
            // only proceed if pickUp is visible
            if (pickUps[i].gameObject.activeSelf == true){

                // find distance between pickUp and ball locations
                new_distance = Vector3.Distance(ball_loc, pickUps[i].transform.position);
                    //Debug.Log(master_distance.ToString());
                    //Debug.Log(new_distance.ToString());
                if (i == 0)
                {
                    master_distance = new_distance;
                }

                if (new_distance < master_distance)
                {
                    master_distance = new_distance;

                    // change colour of closest pickUp, while keeping all other pickUps white
                    for (int j = 0; j < pickUps.Length; j++)
                    {
                        pickUps[j].GetComponent<Renderer>().material.color = Color.white;
                    }
                    // 0 for the start point , position vector ’ startPosition ’
                    lineRenderer.SetPosition(0, ball_loc);
                    // 1 for the end point , position vector ’endPosition ’
                    lineRenderer.SetPosition(1, pickUps[i].transform.position);
                    // Width of 0.1 f both at origin and end of the line
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;

                    pickUps[i].GetComponent<Renderer>().material.color = Color.blue;
                }

            }

        }

        // if all pickUps have been picked up, set distance = 0
        if (pickUps.All(obj => obj.activeInHierarchy == false))
        {
            master_distance = 0;
        }

        // display distance
        disText.text = "Distance: " + master_distance.ToString();
    }
}
