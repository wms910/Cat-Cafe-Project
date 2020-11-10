using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOver : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] Hover;
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseOver()
    {
        AudioClip clip = GetRandomClip(Hover);
        Debug.Log("dfsfd");
        source.PlayOneShot(clip);
    }
    private AudioClip GetRandomClip(AudioClip[] array)
    {
        // return a random AudioClip from the array that is passed as an argument, chosen between index 0 and array.Length (the length of the array)
        return array[Random.Range(0, array.Length)];
    }

}
