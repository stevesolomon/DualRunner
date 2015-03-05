using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour {

    public AudioClip jumpSoundEffect;

    void Start()
    {
        //Subscribe to events that will give us sound effects to fire
        MessageBus.Instance.Subscribe<PlayerJumpedMessage>(PlayJumpEffect);
    }

    void OnDestroy()
    {
        MessageBus.Instance.Unsubscribe<PlayerJumpedMessage>(PlayJumpEffect);
    }

    public void PlayJumpEffect(IMessage message)
    {
        this.GetComponent<AudioSource>().PlayOneShot(jumpSoundEffect);
    }
}
