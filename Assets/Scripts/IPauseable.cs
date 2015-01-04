public interface IPauseable
{
    bool Paused { get; }

    void Pause();

    void Play();
}
