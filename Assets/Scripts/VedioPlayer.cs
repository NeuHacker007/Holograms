using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VedioPlayer : MonoBehaviour
{
    //public MovieTexture movie;
    //public AudioSource audio;

    // Use this for initialization
    void Start()
    {
        MovieTexture movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;

        GetComponent<AudioSource>().clip = movie.audioClip;
        GetComponent<AudioSource>().Play();
        movie.Play();
        //Debug.Log("Play executed!");
        //movie = GetComponent<RawImage>().texture as MovieTexture;
        //audio = GetComponent<AudioSource>();
        //audio.clip = movie.audioClip;
        //movie.Play();
        //audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
