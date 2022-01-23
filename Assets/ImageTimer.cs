using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour
{
    public Image TimerImg;
    public float CurrTime;
    public float MaxTime;
    public bool tick;
    public AudioSource tickAudio;
    // Start is called before the first frame update
    void Start()
    {
        TimerImg = TimerImg.GetComponent<Image>();
        CurrTime = MaxTime;
        tickAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        tick = false;
        CurrTime -= Time.deltaTime;
        if (CurrTime <= 0)
        {
            CurrTime = MaxTime;
            tick = true;
            tickAudio.Play();
        }
        TimerImg.fillAmount = CurrTime / MaxTime;
    }
}
