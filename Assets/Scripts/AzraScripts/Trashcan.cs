using UnityEngine;

namespace AzraScripts
{
    public class Trashcan : MonoBehaviour
    {
        public string[] allowedTags; // Tags that this trashcan can destroy
        [SerializeField] private Animator animator;
        [SerializeField] private bool isSalmonCannon;

        [SerializeField] private AudioClip correctDropoffSound;
        [SerializeField] private AudioClip incorrectDropoffSound;
        [SerializeField] private AudioClip salmonCannonSound;
        
        
        
        
        

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger entered");
            foreach (string tag in allowedTags)
            {
                if (other.CompareTag(tag))
                {
                    Debug.Log("Tag matched: " + tag);
                    Destroy(other.gameObject);
                    return; // Exit the loop once an item is destroyed
                }
            }
        }
    
        public void PutTrashInTrashcan(GameObject trash)
        {
            Debug.Log("Putting stuff in the trashcan");
            foreach (string tag in allowedTags)
            {
                if (trash.CompareTag(tag))
                {
                    //Give the player points
                    if (HighscoreCounter.Instance != null)
                    {
                        HighscoreCounter.Instance.AddToHighscore(1);
                    }
                    if (!isSalmonCannon)
                    {
                        animator.SetTrigger("ItemDeposited");
                        SoundFXManager.instance.PlaySoundFXClip(correctDropoffSound, 1f);
                        Destroy(trash);
                    }
                    else
                    {
                        SalmonCannon cannon = GetComponent<SalmonCannon>();
                        cannon.LaunchSalmon(trash);
                    }
                    Debug.Log("Right trash");
                    
                }
                else
                {
                    Debug.Log("Wrong trash");
                    if (HighscoreCounter.Instance != null)
                    {
                        HighscoreCounter.Instance.AddToHighscore(-1);
                    }
                    
                    SoundFXManager.instance.PlaySoundFXClip(incorrectDropoffSound, 0.5f);
                    animator.SetTrigger("ItemDeposited");
                    Destroy(trash);
                }
                //Give the player negative points
            }
            Debug.Log("Trash deposited");
            
        }
    }
}
