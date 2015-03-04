using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour {

    public AudioClip jumpSoundEffect;

    public void PlayJumpEffect()
    {
        this.GetComponent<AudioSource>().PlayOneShot(jumpSoundEffect);
    }
}
