using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SalmonCannon : MonoBehaviour
{
    public GameObject salmonPrefab;
    public float launchSpeed = 10f;
    public Transform spawnPoint;
    [SerializeField] private AudioClip salmonFire;
    [SerializeField] private AudioClip salmonLoad;
    
    


    public void LaunchSalmon(GameObject pFish)
    {
        StartCoroutine(LaunchSalmonCoroutine(pFish));
    }

    private IEnumerator LaunchSalmonCoroutine(GameObject pFish)
    {
        pFish.transform.parent = spawnPoint;
        SoundFXManager.instance.PlaySoundFXClip(salmonLoad,1);
        while (spawnPoint.position != pFish.transform.position)
        {
            pFish.transform.position = Vector3.MoveTowards(pFish.transform.position, spawnPoint.position, 0.1f);
            yield return null;
        }
        
        pFish.transform.position = spawnPoint.position;
        pFish.transform.rotation = spawnPoint.rotation;

        Rigidbody salmonRb = pFish.GetComponent<Rigidbody>();

        if (salmonRb == null)
        {
            pFish.AddComponent<Rigidbody>();
            salmonRb = pFish.GetComponent<Rigidbody>();
            salmonRb.AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
        }
        else
        {
            salmonRb.AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
        }
        SoundFXManager.instance.PlaySoundFXClip(salmonFire,1);

        yield return new WaitForSeconds(5);
        Destroy(pFish);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchSalmon(salmonPrefab);
        }
    }
}
