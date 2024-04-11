using UnityEngine;

namespace AzraScripts
{
    public class Trashcan : MonoBehaviour
    {
        public string[] allowedTags; // Tags that this trashcan can destroy

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
                    HighscoreCounter.Instance.AddToHighscore(1);
                    Debug.Log("Right trash");
                    return;
                }
                
                Debug.Log("Wrong trash");
                HighscoreCounter.Instance.AddToHighscore(-1);
                //Give the player negative points
            }
        }
    }
}
