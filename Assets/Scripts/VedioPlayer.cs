using HoloToolkit.Unity;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VedioPlayer : Singleton<VedioPlayer>
{
    //public MovieTexture movie;
    //public AudioSource audio;
    public MovieTexture movie { get; private set; }
    public AudioSource audio { get; private set; }
    // Use this for initialization
    void Start()
    {
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        this.gameObject.SetActive(false);
        //audio.Play();
        //movie.Play();

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
