using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryContainer : MonoBehaviour
{
    public AudioClip thisClip;
    public string clipName;

    [SerializeField] private InstrumentController instrument;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private Toggle[] toggles;

    private void Start()
    {
        instrument = FindObjectOfType<InstrumentController>();
        nameText.text = clipName;
    }

    public void ToggleBeatClip(int beat)
    {
        if (toggles[beat - 1].isOn)
            instrument.AddToBeat(beat, thisClip);
        else
            instrument.RemoveFromBeat(beat, thisClip);
    }
}
