using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BgMusic : MonoBehaviour
{
    public TextMeshProUGUI songName;
    public GameObject uiGramophone;

    public AudioSource audioSource;
    private VinylItem currentVinyl;

    private bool playerActive = false;
    private ItemContainer gramophoneContainer;

    private ItemContainer playerInv;
    [SerializeField]
    private Canvas invPanel;
    private void Start()
    {
        gramophoneContainer = GameManager.Instance.grContainer;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component is missing.");
            }
        }
    }
    private VinylItem GetCurrentVinyl()
    {
        ItemSlot slot = gramophoneContainer.slots.Find(s => s.item is VinylItem);
        Debug.Log(slot.item.name);
        return slot?.item as VinylItem;
    }

    public void Play()
    {
        VinylItem vinyl = GetCurrentVinyl();

        if (vinyl != null && vinyl.track != null)
        {
            Debug.Log("PLAY");
            audioSource.clip = vinyl.track;
            audioSource.Play();

            songName.text = vinyl.track.name;
        }
        else
        {
            Debug.Log("No valid vinyl in the Gramophone slot.");
        }
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && playerActive) 
        {
            invPanel.gameObject.SetActive(true);
            uiGramophone.SetActive(!uiGramophone.activeSelf);   
        }
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
            uiGramophone.gameObject.SetActive(false);
        }
    }
}
