using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int carryingCapacity;

    [SerializeField] private PlayerType playerType;
    
    
    [SerializeField] private Rigidbody rb;
    
    
    
    //Add variable for items carried
    public List<ThrashItem> itemsCarried = new ();


    private void FixedUpdate()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                MovePlayer1();
                break;
            case PlayerType.Player2:
                MovePlayer2();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void MovePlayer1()
    {
        //WASD
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0,0, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0,0, -speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, 0,0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, 0,0);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            //pickup items
        }
    }
    private void MovePlayer2()
    {
        //Arrow keys in x and z
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector3(0,0, speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector3(0, 0,-speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-speed, 0,0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(speed, 0,0);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        
        if (Input.GetKey(KeyCode.RightShift))
        {
            //pickup items
        }
    }

    private void OnCollisionStay(Collision other)
    {
        //switch case for player type, player 1 picks up with E, player 2 picks up with right shift
        switch (playerType)
        {
            case PlayerType.Player1:
                if (Input.GetKey(KeyCode.E))
                {
                    other.gameObject.TryGetComponent(out ThrashItem item);
                    if (item != null)
                    {
                        if (itemsCarried.Count < carryingCapacity)
                        {
                            itemsCarried.Add(item);
                            
                        }
                    }
                }
                break;
            case PlayerType.Player2:
                if (Input.GetKey(KeyCode.RightShift))
                {
                    other.gameObject.TryGetComponent(out ThrashItem item);
                    if (item != null)
                    {
                        if (itemsCarried.Count < carryingCapacity)
                        {
                            itemsCarried.Add(item);
                            item.gameObject.SetActive(false);
                        }
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

[Serializable]
public enum PlayerType
{
    Player1,
    Player2
}
