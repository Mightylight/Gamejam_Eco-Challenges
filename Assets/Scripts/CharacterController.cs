using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AzraScripts;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int carryingCapacity;

    [SerializeField] private PlayerType playerType;
    
    
    [SerializeField] private Rigidbody rb;

    [SerializeField] private AudioClip[] pickupSounds;
    [SerializeField] private AudioClip dropOffSound;
    [SerializeField] private AudioClip[] walkingSounds;

    [SerializeField] private Animator animator;
    
    
    
    private float lastStartTime;
    private float cooldownTime = 1;
    
    private bool pressedInteract = false;
    
    
    
    
    
    //Add variable for items carried
    //public List<ThrashItem> itemsCarried = new ();
    
    //public ThrashItem itemCarrying;
    private Queue<ThrashItem> itemsCarried;

    private void Awake()
    {
        itemsCarried = new Queue<ThrashItem>();
        lastStartTime = Time.time;
    }

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
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            // rb.velocity = new Vector3(0,0, speed);
            movement += new Vector3(0,0,speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // rb.velocity = new Vector3(0,0, -speed);
            movement += new Vector3(0,0,-speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // rb.velocity = new Vector3(-speed, 0,0);
            movement += new Vector3(-speed,0,0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // rb.velocity = new Vector3(speed, 0,0);
            movement += new Vector3(speed,0,0);
        }
        
        if (movement != Vector3.zero)
        {
            int random = UnityEngine.Random.Range(0, 100);
            if (random < 5)
            {
                //SoundFXManager.instance.PlayRandomSoundFXClip(walkingSounds,1f);
            }
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
        rb.velocity = movement;
    }
    private void MovePlayer2()
    {
        Vector3 movement = Vector3.zero;
        
        //Arrow keys in x and z
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // rb.velocity = new Vector3(0,0, speed);
            movement += new Vector3(0,0,speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // rb.velocity = new Vector3(0, 0,-speed);
            movement += new Vector3(0,0,-speed);
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // rb.velocity = new Vector3(-speed, 0,0);
            movement += new Vector3(-speed,0,0);
        } 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // rb.velocity = new Vector3(speed, 0,0);
            movement += new Vector3(speed,0,0);
        }

        if (movement != Vector3.zero)
        {
            int random = UnityEngine.Random.Range(0, 100);
            if (random < 5)
            {
                //SoundFXManager.instance.PlayRandomSoundFXClip(walkingSounds,1f);
            }
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        rb.velocity = movement;
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     //switch case for player type, player 1 picks up with E, player 2 picks up with right shift
    //     switch (playerType)
    //     {
    //         case PlayerType.Player1:
    //             if (Input.GetKeyUp(KeyCode.E))
    //             {
    //                 CheckForThrashItem(other);
    //
    //                 CheckForTrashCan(other);
    //             }
    //             break;
    //         case PlayerType.Player2:
    //             if (Input.GetKeyUp(KeyCode.RightShift))
    //             {
    //                 CheckForThrashItem(other);
    //
    //                 CheckForTrashCan(other);
    //             }
    //             
    //             
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }

    

    private void OnCollisionStay(Collision other)
    {
        //switch case for player type, player 1 picks up with E, player 2 picks up with right shift
        switch (playerType)
        {
            case PlayerType.Player1:
                if (Input.GetKeyUp(KeyCode.E))
                {
                    CheckForThrashItem(other);

                    CheckForTrashCan(other);
                }
                break;
            case PlayerType.Player2:
                if (Input.GetKeyUp(KeyCode.RightShift))
                {
                    CheckForThrashItem(other);

                    CheckForTrashCan(other);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private IEnumerator StopClicking()
    {
        Debug.Log("Wollah nif");
        yield return new WaitForSeconds(1);
        Debug.Log("Wollah nif 2");
        pressedInteract = false;
        
    }
    
    private void CheckForTrashCan(Collision other)
    {
        other.gameObject.TryGetComponent(out Trashcan trashcan);
        if (trashcan != null)
        {
            Debug.Log("pressedInteract: " + pressedInteract);
            if (pressedInteract) return;
            pressedInteract = true;
            StartCoroutine(StopClicking());
            if (itemsCarried.Count > 0)
            {
                ThrashItem item = itemsCarried.Dequeue();
                trashcan.PutTrashInTrashcan(item.gameObject);
                Destroy(item.gameObject);
                Debug.Log("Destroyed Items");
                Debug.Log("Items carried: " + itemsCarried.Count);
                foreach (ThrashItem remainingItem in itemsCarried)
                {
                    remainingItem.transform.localPosition += new Vector3(0,-1,0);
                    Debug.Log("Moved items down");
                }
                
                SoundFXManager.instance.PlaySoundFXClip(dropOffSound,1f);
            }
        }
    }

    private void CheckForThrashItem(Collision other)
    {
        other.gameObject.TryGetComponent(out ThrashItem item);
        if (item != null)
        {
            if (itemsCarried.Count < carryingCapacity)
            {
                Debug.Log("Picking up items");
                itemsCarried.Enqueue(item);
                item.transform.SetParent(transform);
                int index = itemsCarried.Count - 1;
                item.transform.localPosition = new Vector3(0,index + 1 , 0);
                Destroy(item.GetComponent<Rigidbody>());
                SoundFXManager.instance.PlayRandomSoundFXClip(pickupSounds,1f);
            }
        }
    }
    
}

[Serializable]
public enum PlayerType
{
    Player1,
    Player2
}
