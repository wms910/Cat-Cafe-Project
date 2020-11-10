using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] Hover;
    public AudioClip[] Coin;
    public AudioClip[] Buy;
    public AudioClip[] Starting;
    public AudioClip[] startHover;
    public AudioClip[] Settings;
    void Start()
    {
       // gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playHover()
    {
        
        AudioClip clip = GetRandomClip(Hover);

        source.PlayOneShot(clip);
    }
    public void playSettings()
    {
        AudioClip clip = GetRandomClip(Settings);

        source.PlayOneShot(clip);
    }
    public void playStart()
    {
        
        AudioClip clip = GetRandomClip(Starting);

        source.PlayOneShot(clip);

       
      

    }
    public void playStarthover()
    {
        AudioClip clip = GetRandomClip(startHover);

        source.PlayOneShot(clip);
    }
    public void playCoin()
    {
        AudioClip clip = GetRandomClip(Coin);

        source.PlayOneShot(clip);
    }
    public void playBuy()
    {
        AudioClip clip = GetRandomClip(Buy);

        source.PlayOneShot(clip);
    }
    private AudioClip GetRandomClip(AudioClip[] array)
    {
        // return a random AudioClip from the array that is passed as an argument, chosen between index 0 and array.Length (the length of the array)
        return array[Random.Range(0, array.Length)];
    }
}
