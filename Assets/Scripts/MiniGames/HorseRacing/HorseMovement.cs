using UnityEngine;

public class HorseMovement : MonoBehaviour
{
    public float Acceleration = 10f;
    public float MaxSpeed = 100f;
    public float RotationSpeed = 4f;
    public float SteeringAccelerationModifier = 3f;
    public float ReverseModifier = .2f;
    public float DriftingBoundary = 10f;
    public float VolumeModifier = 5f;
    public float VolumeOffset = .2f;
    
    public bool isDrifting
    {
        get { return _isDrifting; }
    }

    private Rigidbody2D rigidbody2D;
    private AudioSource audioSource;
    private ParticleSystem particleSystem;

    private float Rotation = 0f;
    private bool _isDrifting = false;
    private bool isPlayingAudio;
    private bool isReversing = false;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrifting && !isPlayingAudio)
        {
            audioSource.Play();
            particleSystem.Play();
            isPlayingAudio = true;
            return;
        }

        if (!isDrifting && isPlayingAudio)
        {
            audioSource.Stop();
            particleSystem.Stop();
            isPlayingAudio = false;
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        isReversing = vertical < 0;

        if (isReversing) vertical *= ReverseModifier;
        rigidbody2D.AddRelativeForce(new Vector2(0, vertical * Acceleration));
        rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, MaxSpeed - (Mathf.Abs(horizontal) * SteeringAccelerationModifier));
        audioSource.volume = rigidbody2D.velocity.magnitude / MaxSpeed * VolumeModifier - VolumeOffset;

        float steeringWheelRotation = rigidbody2D.velocity.magnitude / MaxSpeed;
        float rotation = Mathf.Lerp(rigidbody2D.rotation, (isReversing ? 1 : -1) * horizontal * RotationSpeed + rigidbody2D.rotation, steeringWheelRotation);

        _isDrifting = Mathf.Abs(rigidbody2D.transform.InverseTransformDirection(rigidbody2D.velocity).x) > DriftingBoundary;

        rigidbody2D.MoveRotation(rotation);

    }
}
