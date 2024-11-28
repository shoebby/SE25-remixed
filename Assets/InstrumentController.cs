using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentController : MonoBehaviour
{
    [SerializeField] private Slider timelineSlider;
    [SerializeField] private Slider tempoSlider;
    [SerializeField] private AudioSource source;
    [SerializeField] private float tempo;

    //beat bools
    private bool played_beat1;
    private bool played_beat2;
    private bool played_beat3;
    private bool played_beat4;

    [Header("Tick Balls")]
    [SerializeField] private Image ball1;
    [SerializeField] private Image ball2;
    [SerializeField] private Image ball3;
    [SerializeField] private Image ball4;
    [SerializeField] private Color ball_defaultColor;
    [SerializeField] private Color ball_activeColor;
    [SerializeField] private float ball_waitTime;

    //beat clip lists
    [SerializeField] private List<AudioClip> clips_beat1 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat2 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat3 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat4 = new List<AudioClip>();

    private void Awake()
    {
        
    }

    void Start()
    {
        ResetBools();
    }

    void Update()
    {
        timelineSlider.value += Time.deltaTime / tempo;

        if (timelineSlider.value >= 1)
        {
            timelineSlider.value = 0;
            ResetBools();
        }
            

        if (timelineSlider.value >= 0 && !played_beat1)
        {
            PlayClips(clips_beat1, ball1);
            played_beat1 = true;
        }

        if (timelineSlider.value >= .25 && !played_beat2)
        {
            PlayClips(clips_beat2, ball2);
            played_beat2 = true;
        }

        if (timelineSlider.value >= .5 && !played_beat3)
        {
            PlayClips(clips_beat3, ball3);
            played_beat3 = true;
        }

        if (timelineSlider.value >= .75 && !played_beat4)
        {
            PlayClips(clips_beat4, ball4);
            played_beat4 = true;
        }
    }

    private void ResetBools()
    {
        played_beat1 = false;
        played_beat2 = false;
        played_beat3 = false;
        played_beat4 = false;
    }

    public void SetTempo()
    {
        tempo = tempoSlider.value;
    }

    private void PlayClips(List<AudioClip> list, Image ball)
    {        
        foreach (AudioClip clip in list)
        {
            source.PlayOneShot(clip);
        }

        StartCoroutine(FlashBall(ball));
    }

    private IEnumerator FlashBall(Image image)
    {
        image.color = ball_activeColor;
        yield return new WaitForSeconds(ball_waitTime);
        image.color = ball_defaultColor;
    }

    public void AddToBeat(int beat, AudioClip clip)
    {
        if (beat == 1)
            clips_beat1.Add(clip);
        else if (beat == 2)
            clips_beat2.Add(clip);
        else if (beat == 3)
            clips_beat3.Add(clip);
        else if (beat == 4)
            clips_beat4.Add(clip);
    }

    public void RemoveFromBeat(int beat, AudioClip clip)
    {
        if (beat == 1)
            clips_beat1.Remove(clip);
        else if (beat == 2)
            clips_beat2.Remove(clip);
        else if (beat == 3)
            clips_beat3.Remove(clip);
        else if (beat == 4)
            clips_beat4.Remove(clip);
    }
}
