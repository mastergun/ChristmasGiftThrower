using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public List<AudioClip> gameEffects;
    public Slider effectsSlider;
    public Slider musicSlider;
    private AudioSource effectSource;

    public float effectsVol = 1;
    float musicVol = 1;
    // Use this for initialization
    void Start()
    {
        effectSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        effectsVol = effectsSlider.value;
        musicVol = musicSlider.value;

    }

    public void PlayGameEffect(int id)
    {
        //float vol = Random.Range(0.5f, 0.7f);
        effectSource.Stop();
        effectSource.PlayOneShot(gameEffects[id], effectsVol);
    }
}
