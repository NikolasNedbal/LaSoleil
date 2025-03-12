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

    public Image vinylPictur;

    public Animator anim;

    private ItemContainer playerInv;
    private void Start()
    {
        playerInv = GameManager.Instance.invContainer;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.Log("nema");
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

            vinylPictur.sprite = currentVinyl.vinylPic;
            anim.SetBool(currentVinyl.animBool, true);
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

            anim.SetBool(currentVinyl.animBool, true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && playerActive) 
        {
            uiGramophone.SetActive(!uiGramophone.activeSelf);
            
        }
    }

    public void PlayAnim()
    {
        anim.enabled = true;
        anim.SetBool(currentVinyl.animBool, true);
    }

    public void StopVinyl()
    {
        anim.SetBool(currentVinyl.animBool, false);
        anim.enabled = false;

        vinylPictur.sprite = currentVinyl.vinylPic;
    }

    private VinylItem FindVinyl()
    {
        VinylItem foundVinyl = null;
        ItemSlot itemSlot = playerInv.slots.Find(slot =>
        {
            if (slot.item != null)
            {
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
