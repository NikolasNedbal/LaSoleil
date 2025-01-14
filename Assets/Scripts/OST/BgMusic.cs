using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BgMusic : MonoBehaviour
{
    public TextMeshProUGUI songName;
    public GameObject uiGramophone;

    public AudioSource audioSource;
    private VinylItem currentVinyl;

    private bool playerActive = false;

    private ItemContainer playerInv;
    private void Start()
    {

        playerInv = GameManager.Instance.invContainer;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.Log("AudioSource is not assigned and no AudioSource component is found on this GameObject.");
            }
        }
    }
    public void InsertVinyl()
    {
        VinylItem foundVinyl = FindVinyl();
        if (foundVinyl != null)
        {
            currentVinyl = foundVinyl;
        }

        if (currentVinyl != null && currentVinyl.track != null)
        {
            songName.text = currentVinyl.track.name;
            PlayVinyl(currentVinyl);
        }
        else
        {
            Debug.Log("Vinyl / track is null");
        }
    }

    public void PlayVinyl(VinylItem vinyl)
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = currentVinyl.track;
            audioSource.Play();
            Debug.Log("Now playing: " + currentVinyl.Name);
        }
        else
        {
            Debug.Log("AudioSource not assigned");
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && playerActive) 
        {
            uiGramophone.SetActive(!uiGramophone.activeSelf);
            
        }
    }

    private VinylItem FindVinyl()
    {
        VinylItem foundVinyl = null;
        ItemSlot itemSlot = playerInv.slots.Find(slot =>
        {
            if (slot.item != null)
            {
                Debug.Log("Item type: " + slot.item.GetType().Name);
                if (slot.item is VinylItem vinyl && vinyl.track != null)
                {
                    foundVinyl = vinyl;
                    return true;
                }
            }
            return false;
        });

        return foundVinyl;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActive = false;
        }
    }
}
