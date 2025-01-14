using UnityEngine;
using UnityEngine.UI;

public class CreationTimer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float fillDuration = 5f;

    private float elapsedTime = 0f;
    public bool timerEnded { get; private set; } = false;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        
        if (slider != null)
        {
            slider.value = 0f;
        }
    }

    void Update()
    {
        if (slider != null && slider.value < slider.maxValue)
        { 
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(elapsedTime / fillDuration) * slider.maxValue;
            if (slider.value == slider.maxValue) 
            {
                timerEnded = true;
            }
        }
    }

    public void ResetSlider()
    {
        timerEnded = false;
        elapsedTime = 0f;
        if (slider != null)
        {
            slider.value = 0f;
        }
    }
}
