using UnityEngine;

public class PracticeDummy : MonoBehaviour
{
    /// <summary>
    /// Point from which will damage direction be calculated
    /// </summary>
    [SerializeField] Transform _damagePositionOrigin, _textSpawnOrigin;
    [SerializeField] string _frontAnimationsSetName, _backAnimationsSetName;
    [SerializeField] RandomAnimationPlayer _player;
    [SerializeField] Color _textColor, _textOutlineColor;
    public AudioSource SfxPlayer;
    public AudioClip[] AudioClips;
    public ParticleSystem HitParticles;


    public void Damage(int damage, Vector2 damagePosition, string info = "")
    {
        TextSpawner.SpawnText(damage.ToString() + info, _textSpawnOrigin.position, _textColor, _textOutlineColor);

        if (damagePosition.x < _damagePositionOrigin.position.x)    // If damaged from left | behind
            _player.Play(_backAnimationsSetName);
        else
            _player.Play(_frontAnimationsSetName);

        PlayRandomSfx();

        HitParticles.Stop();
        HitParticles.Play();
    }

    void PlayRandomSfx()
    {
        SfxPlayer.Stop();

        var choosedSfx = AudioClips[Random.Range(0, AudioClips.Length)];
        SfxPlayer.clip = choosedSfx;

        SfxPlayer.Play();
    }
}
