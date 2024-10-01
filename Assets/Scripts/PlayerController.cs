using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;

    private int count;
    private int numPickups = 6;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI veloText;
    public TextMeshProUGUI posText;

    public Vector3 oldPos;

    
    void Start( ){

        count = 0;
        oldPos.x = 0;
        oldPos.y = 0;
        oldPos.z = 0;
        winText.text = "";
        SetCountText();
    }

    void Update(){
        debugPlayer();
        oldPos = transform.position;
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "PickUp"){
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if(count >= numPickups)
        {
            winText.text = "You win!";
        }
    }

    private void debugPlayer(){
       
        posText.text = "Position: " + transform.position.ToString();
        float velocity = (transform.position - oldPos).magnitude / Time.deltaTime;
        veloText.text = "Velocity: " + velocity.ToString();
        
    }


}