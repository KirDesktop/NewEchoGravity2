using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _sounds;

    private AudioSource _source;

    private void Awake()
    {
        _source = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        _source.clip = _sounds[Random.Range(0,_sounds.Count)];
        _source.Play();
    }
}
