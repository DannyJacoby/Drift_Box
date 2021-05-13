using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class KartController : MonoBehaviour
{
    // private PostProcessVolume m_PostVolume;
    // private PostProcessProfile m_PostProfile;

    public Transform kartModel;
    public Transform kartNormal;
    public Rigidbody sphere;

    // public List<ParticleSystem> primaryParticles = new List<ParticleSystem>();
    // public List<ParticleSystem> secondaryParticles = new List<ParticleSystem>();

    private float m_Speed, m_CurrentSpeed;
    private float m_Rotate, m_CurrentRotate;
    private int m_DriftDirection;
    private float m_DriftPower;
    private int m_DriftMode;
    private bool m_First, m_Second, m_Third;
    private Color m_C;

    [Header("Booleans")]
    public bool drifting;

    [Header("Parameters")]

    public float acceleration = 30f;
    public float steering = 80f;
    public float gravity = 10f;
    public float boostLevels = 1.5f;
    public LayerMask layerMask;

    [Header("Model Parts")]

    public Transform frontWheels;
    public Transform backWheels;
    // public Transform steeringWheel;

    // [Header("Particles")]
    // public Transform wheelParticles;
    // public Transform flashParticles;
    // public Color[] turboColors;

    [Header("Special Components")] public BackgroundMusic player;
    
    private void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Audio Player").GetComponent<BackgroundMusic>();
        // player.PlayEngineStart();
        // if (Camera.main is { }) m_PostVolume = Camera.main.GetComponent<PostProcessVolume>();
        // m_PostProfile = m_PostVolume.profile;
        //
        // for (var i = 0; i < wheelParticles.GetChild(0).childCount; i++)
        // {
        //     primaryParticles.Add(wheelParticles.GetChild(0).GetChild(i).GetComponent<ParticleSystem>());
        // }
        //
        // for (var i = 0; i < wheelParticles.GetChild(1).childCount; i++)
        // {
        //     primaryParticles.Add(wheelParticles.GetChild(1).GetChild(i).GetComponent<ParticleSystem>());
        // }
        //
        // foreach(var p in flashParticles.GetComponentsInChildren<ParticleSystem>())
        // {
        //     secondaryParticles.Add(p);
        // }
    }

    private void Update()
    {
        //Follow Collider
        transform.position = sphere.transform.position - new Vector3(0, 0.4f, 0);

        //Accelerate
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            m_Speed = acceleration;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            m_Speed = -acceleration;

        // //Sound Effects (forward/backward)
        // if (Input.GetKey(KeyCode.UpArrow) 
        //     || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        // {
        //     player.PlayMovementSound();
        // }

        //Steer
        if (Input.GetAxis("Horizontal") != 0)
        {
            var dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            var amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            Steer(dir, amount);
        }

        //Drift
        if (Input.GetButtonDown("Jump") && !drifting)
        {
            drifting = true;
            m_DriftDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;

            // foreach (var p in primaryParticles)
            // {
            //     #pragma warning disable 618
            //     p.startColor = Color.clear;
            //     #pragma warning restore 618
            //     p.Play();
            // }

            kartModel.parent.DOComplete();
            kartModel.parent.DOPunchPosition(transform.up * .2f, .3f, 5);
            
        }

        if (drifting)
        {
            var control = (m_DriftDirection == 1) ? Input.GetAxis("Horizontal").Remap(-1, 1, 0, 2) : Input.GetAxis("Horizontal").Remap(-1, 1, 2, 0);
            var powerControl = (m_DriftDirection == 1) ? Input.GetAxis("Horizontal").Remap(-1, 1, .2f, 1) : Input.GetAxis("Horizontal").Remap(-1, 1, 1, .2f);
            Steer(m_DriftDirection, control);
            m_DriftPower += powerControl;

            // ColorDrift();
            
            // TODO fix this sound effect, as it keeps competing with the forward movement sound which is bad
            // Sound Effects
            // player.PlaySlidingSound();
        }

        if (Input.GetButtonUp("Jump") && drifting)
        {
            Boost();
        }

        m_CurrentSpeed = Mathf.SmoothStep(m_CurrentSpeed, m_Speed, Time.deltaTime * 12f); m_Speed = 0f;
        m_CurrentRotate = Mathf.Lerp(m_CurrentRotate, m_Rotate, Time.deltaTime * 4f); m_Rotate = 0f;

        //Animations    

        //a) Kart
        if (!drifting)
        {
            kartModel.localEulerAngles = Vector3.Lerp(kartModel.localEulerAngles, new Vector3(0, 90 + (Input.GetAxis("Horizontal") * 15), kartModel.localEulerAngles.z), .2f);
        }
        else
        {
            // TODO discover why the car vibrates like a chihuahua when drifting, this is the cause but why
            // var control = (m_DriftDirection == 1) ? Input.GetAxis("Horizontal").Remap(-1, 1, .5f, 2) : Input.GetAxis("Horizontal").Remap(-1, 1, 2, .5f);
            // kartModel.parent.localRotation = Quaternion.Euler(0, Mathf.LerpAngle(kartModel.parent.localEulerAngles.y,(control * 15) * m_DriftDirection, .2f), 0);
        }

        //b) Wheels
        frontWheels.localEulerAngles = new Vector3(0, (Input.GetAxis("Horizontal") * 15), frontWheels.localEulerAngles.z);
        frontWheels.localEulerAngles += new Vector3(0, sphere.velocity.magnitude/2, 0);
        backWheels.localEulerAngles += new Vector3(sphere.velocity.magnitude/2, 0, 0);

        //c) Steering Wheel
        // steeringWheel.localEulerAngles = new Vector3(-25, 90, ((Input.GetAxis("Horizontal") * 45)));

    }

    private void FixedUpdate()
    {
        // // Reset the player
        // if (Input.GetKey(KeyCode.R))
        // {
        //     transform.GetChild(0).transform.position = new Vector3(0, 10, 0);
        //     transform.GetChild(0).transform.rotation = Quaternion.identity;
        //     transform.GetChild(1).transform.position = new Vector3(0, 10, 0);
        //     transform.GetChild(1).transform.rotation = Quaternion.identity;
        //     return;
        // }

        // // Return to main menu
        // if (Input.GetKey(KeyCode.Escape))
        // {
        //     SceneManager.LoadScene("OpeningScene");
        //     return;
        // }
        
        //Forward Acceleration
        if(!drifting)
            sphere.AddForce(-kartModel.transform.right * m_CurrentSpeed, ForceMode.Acceleration);
        else
            sphere.AddForce(transform.forward * m_CurrentSpeed, ForceMode.Acceleration);

        //Gravity
        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Steering
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + m_CurrentRotate, 0), Time.deltaTime * 5f);
        
        Physics.Raycast(transform.position + (transform.up*.1f), Vector3.down, out var hitOn, 1.1f,layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f)   , Vector3.down, out var hitNear, 2.0f, layerMask);

        //Normal Rotation
        kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        kartNormal.Rotate(0, transform.eulerAngles.y, 0);
    }

    public void Boost()
    {
        drifting = false;

        if (m_DriftMode > 0)
        {
            DOVirtual.Float(m_CurrentSpeed * boostLevels, m_CurrentSpeed, .3f * m_DriftMode, Speed);
            // DOVirtual.Float(0, 1, .5f, ChromaticAmount).OnComplete(() => DOVirtual.Float(1, 0, .5f, ChromaticAmount));
            // kartModel.Find("Tube001").GetComponentInChildren<ParticleSystem>().Play();
            // kartModel.Find("Tube002").GetComponentInChildren<ParticleSystem>().Play();
        }

        m_DriftPower = 0;
        m_DriftMode = 0;
        m_First = false; m_Second = false; m_Third = false;

        // foreach (var p in primaryParticles)
        // {
        //     #pragma warning disable 618
        //     p.startColor = Color.clear;
        //     #pragma warning restore 618
        //     p.Stop();
        // }

        kartModel.parent.DOLocalRotate(Vector3.zero, .5f).SetEase(Ease.OutBack);

    }

    public void Steer(int direction, float amount)
    {
        m_Rotate = (steering * direction) * amount;
    }

    public void ColorDrift()
    {
        if(!m_First)
            m_C = Color.clear;

        if (m_DriftPower > 50 && m_DriftPower < 100-1 && !m_First)
        {
            m_First = true;
            // m_C = turboColors[0];
            m_DriftMode = 1;

            // PlayFlashParticle(c);
        }

        if (m_DriftPower > 100 && m_DriftPower < 150- 1 && !m_Second)
        {
            m_Second = true;
            // m_C = turboColors[1];
            m_DriftMode = 2;

            // PlayFlashParticle(c);
        }

        if (m_DriftPower > 150 && !m_Third)
        {
            m_Third = true;
            // m_C = turboColors[2];
            m_DriftMode = 3;

            // PlayFlashParticle(c);
        }

        // foreach (var p in primaryParticles)
        // {
        //     var pMain = p.main;
        //     pMain.startColor = m_C;
        // }
        //
        // foreach(var p in secondaryParticles)
        // {
        //     var pMain = p.main;
        //     pMain.startColor = m_C;
        // }
    }

    // void PlayFlashParticle(Color c)
    // {
    //     GameObject.Find("CM vcam1").GetComponent<CinemachineImpulseSource>().GenerateImpulse();
    //
    //     foreach (var p in secondaryParticles)
    //     {
    //         var pMain = p.main;
    //         pMain.startColor = c;
    //         p.Play();
    //     }
    // }

    private void Speed(float x)
    {
        m_CurrentSpeed = x;
    }

    // void ChromaticAmount(float x)
    // {
    //     postProfile.GetSetting<ChromaticAberration>().intensity.value = x;
    // }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position + transform.up, transform.position - (transform.up * 2));
    //}
}
