using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VedioPlayer : MonoBehaviour
{
    //public MovieTexture movie;
    //public AudioSource audio;
    private MovieTexture movie;
    private AudioSource audio;
    // Use this for initialization
    void Start()
    {
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        audio.Play();
        movie.Play();

        //DontDestroyOnLoad(movie);
        //DontDestroyOnLoad(audio);
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
