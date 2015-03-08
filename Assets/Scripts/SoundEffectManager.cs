using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour, IListener<PlayerJumpedMessage> {

    public AudioClip jumpSoundEffect;

    void Start()
    {
        //Subscribe to events that will give us sound effects to fire
        MessageBus.Instance.Subscribe<PlayerJumpedMessage>(this);
    }

    void OnDestroy()
    {
       // MessageBus.Instance.Unsubscribe<PlayerJumpedMessage>(PlayJumpEffect);
    }

    public void HandleMessage(PlayerJumpedMessage message)
    {
        this.GetComponent<AudioSource>().PlayOneShot(jumpSoundEffect);
    }
}
