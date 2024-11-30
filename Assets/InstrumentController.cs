using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentController : MonoBehaviour
{
    [SerializeField] private Slider timelineSlider;
    [SerializeField] private Slider bpmSlider;
    [SerializeField] private TextMeshProUGUI bpmSlider_text;
    [SerializeField] private AudioSource source;
    [SerializeField] private float bpm;
    private float step;
    private float threshold;
    private int nextBeat;

    //beat bools
    private bool[] played_beats = new bool[16];

    [Header("Tick Balls")]
    [SerializeField] private Image[] tickBalls = new Image[16];
    [SerializeField] private Color ball_defaultColor;
    [SerializeField] private Color ball_activeColor;
    [SerializeField] private float ball_waitTime;

    //beat clip lists
    private List<AudioClip>[] beats_master;

    [SerializeField] private List<AudioClip> clips_beat1 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat2 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat3 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat4 = new List<AudioClip>();

    [SerializeField] private List<AudioClip> clips_beat5 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat6 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat7 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat8 = new List<AudioClip>();

    [SerializeField] private List<AudioClip> clips_beat9 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat10 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat11 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat12 = new List<AudioClip>();

    [SerializeField] private List<AudioClip> clips_beat13 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat14 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat15 = new List<AudioClip>();
    [SerializeField] private List<AudioClip> clips_beat16 = new List<AudioClip>();

    private void Awake()
    {
        beats_master = new List<AudioClip>[]
        {
            clips_beat1, clips_beat2, clips_beat3, clips_beat4, clips_beat5, clips_beat6, clips_beat7, clips_beat8, clips_beat9, clips_beat10, clips_beat11, clips_beat12,
            clips_beat13, clips_beat14, clips_beat15, clips_beat16
        };
    }

    void Start()
    {
        ResetValues();
        SetBPM();
    }

    void Update()
    {
        step = bpm / 60;
        timelineSlider.value += step * Time.deltaTime;

        if (timelineSlider.value >= 4)
        {
            ResetValues();
        }

        if (timelineSlider.value >= threshold && !played_beats[nextBeat])
        {
            PlayClips(beats_master[nextBeat], tickBalls[nextBeat]);
            played_beats[nextBeat] = true;
            threshold += .25f;
            nextBeat++;
        }
    }

    private void ResetValues()
    {
        timelineSlider.value = 0f;
        threshold = 0f;
        nextBeat = 0;

        for (int i = 0; i < played_beats.Length; i++)
        {
            played_beats[i] = false;
        }
    }

    public void SetBPM()
    {
        bpm = bpmSlider.value;
        bpmSlider_text.text = bpm + " BPM";
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
        beats_master[beat - 1].Add(clip);
    }

    public void RemoveFromBeat(int beat, AudioClip clip)
    {
        beats_master[beat - 1].Remove(clip);
    }
}
