using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClipsContainer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private string path;
    [SerializeField] private GameObject clipPrefab;
    [SerializeField] private Transform content;

    void Start()
    {
        clips = Resources.LoadAll(path, typeof(AudioClip)).Cast<AudioClip>().ToArray();

        foreach (AudioClip clip in clips)
        {
            GameObject newClipPrefab = Instantiate(clipPrefab, content);
            EntryContainer newPrefabScript = newClipPrefab.GetComponent<EntryContainer>();
            newPrefabScript.thisClip = clip;
            newPrefabScript.clipName = "(" + clip.length.ToString().Substring(0,4) + " sec) " + clip.name;
        }
    }
}
