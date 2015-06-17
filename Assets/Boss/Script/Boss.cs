using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject moveBossGameObject;
    [SerializeField] private Transform playerCoordinates;
    [SerializeField] private GameObject sawHandle;
    [SerializeField] private float swingVelocity = 80;
    [SerializeField] private float slamDropHeight = 30;
    [SerializeField] private float slamUpSpeed = 20;
    [SerializeField] private float slamDropSpeed = 40;
    [SerializeField] private float WaitTimeBeforeAttacks = 8;
    [SerializeField] private float WaitTimeBeforeShockOver = 3;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private CameraShake cameraShake;

    private Vector3 velocity;

    private ActionState currentState;
    private AttackPattern attackPattern;
    private Vector3 locationToAttack;
    private bool cutting = false;
    public bool SawCanCut = false; //When in position to cut the player or object
    private Vector3 sawHandleDefaultPosition;
    private Quaternion sawHandleDefaultRotation;
    private Vector3 trailPosition;
    private Vector3 positionNotchanging; //Bug related to Vector3.SmoothDamp
    private float slamYLocation;
    private bool slammingAction = false;
    [SerializeField]private float slamShockTime = 5;
    private int revivers = 0;

    public bool SlammingDown = false; //DirectionDown
    public bool Vulnerable = false;
    public int life = 3;
    private const int MaxLives = 3;


    private bool moving = false;
    private bool dying = false;

    private enum AttackPattern
    {
        DropDown,
        CutHim
    }

    private enum ActionState
    {
        WaitForCommands,
        GettingReadyToIdle, //Moving back for movement animation.
        Idle,
        GettingReadyToAttack,
        Attacking,
    }

    public int Revivers
    {
        get { return revivers; }
        set { revivers = value; }
    }

    void Start()
    {
        currentState = ActionState.Idle;
        StartCoroutine(CreateAttacks());
        GetRequiredComponents();
    }

    private void GetRequiredComponents()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Moving", true);
        _rigidBody = moveBossGameObject.GetComponent<Rigidbody2D>();
        //_playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    private IEnumerator CreateAttacks()
    {
        while (true)
        {
            if (currentState != ActionState.Idle)
            {
                yield return new WaitForSeconds(WaitTimeBeforeAttacks);
                continue;
            }
            yield return new WaitForSeconds(WaitTimeBeforeAttacks);

            locationToAttack = playerCoordinates.position;
            currentState = ActionState.GettingReadyToAttack;
            _animator.enabled = false;
            _animator.SetBool("Moving", false);
            trailPosition = moveBossGameObject.transform.position;
            
            int random = Random.Range(1, 5); //the higher the number the less likelyhood of DropDown Attack
            attackPattern = random == 1 ? AttackPattern.DropDown : AttackPattern.CutHim;
           // attackPattern = AttackPattern.DropDown;
        }
    }

    private Vector3 GetLocationToAttack()
    {
        if (attackPattern == AttackPattern.DropDown)
            slamYLocation = locationToAttack.y + slamDropHeight;

        return new Vector3(locationToAttack.x, locationToAttack.y, moveBossGameObject.transform.position.z);
    }

    void Update()
    {
        if (life == 0)
        {
            GameOver();
            return;
        }

        if (currentState == ActionState.GettingReadyToAttack)
            MoveToAttack(GetLocationToAttack());    //Change to Constant Location
        else if (currentState == ActionState.Attacking)
            Attack();
        else if (currentState == ActionState.GettingReadyToIdle)
            MoveBackToNormal();
    }

    private void GameOver()
    {
        _animator.enabled = true;
        _animator.SetBool("Dying", true);
        _animator.SetBool("Moving", false);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
        {
            if (revivers > 0)
            {
                _animator.SetBool("Dying", false);
                life = MaxLives;

            }
            else
            {
                
            }
        }
    }

    private void MoveToAttack(Vector3 targetLocation)
    {
        positionNotchanging = moveBossGameObject.transform.position;
        moveBossGameObject.transform.position = Vector3.SmoothDamp(moveBossGameObject.transform.position, targetLocation, ref velocity, 0.1f);
        if (moveBossGameObject.transform.position == targetLocation || moveBossGameObject.transform.position == positionNotchanging)
        {
            currentState = ActionState.Attacking;
        }
    }

    private void MoveBackToNormal()
    {
        Debug.Log("Calling too much");

        positionNotchanging = moveBossGameObject.transform.position;
        moveBossGameObject.transform.position = Vector3.SmoothDamp(moveBossGameObject.transform.position, trailPosition, ref velocity, 0.3f);
        if (moveBossGameObject.transform.position == trailPosition || moveBossGameObject.transform.position == positionNotchanging)
        {
            currentState = ActionState.Idle;
            _animator.enabled = true;
            _animator.SetBool("Moving", true);
        }
    }

    private void Attack()
    {
        switch (attackPattern)
        {
            case AttackPattern.CutHim:
                sawHandle.transform.RotateAround(moveBossGameObject.transform.position, Vector3.up, swingVelocity * Time.deltaTime);
                if (!cutting)
                {
                    cutting = true;
                    sawHandleDefaultPosition = sawHandle.transform.position;
                    sawHandleDefaultRotation = sawHandle.transform.rotation;
                }
                else
                {
                    if (sawHandle.transform.rotation.y < 0.36f)
                    {
                        SawCanCut = true;
                    }

                    if (sawHandle.transform.rotation.y < -0.60f)
                    {
                        currentState = ActionState.GettingReadyToIdle;
                        cutting = false;
                        SawCanCut = false;
                        sawHandle.transform.position = sawHandleDefaultPosition;
                        sawHandle.transform.rotation = sawHandleDefaultRotation;
                    }
                }
                
                break;
            case AttackPattern.DropDown:
                if (!slammingAction)
                {
                    _rigidBody.velocity = Vector3.up * slamUpSpeed;
                    slammingAction = true;
                }
                else
                {
                    if (moveBossGameObject.transform.position.y > slamYLocation)
                    {
                        _rigidBody.velocity = Vector3.down*slamDropSpeed;
                        SlammingDown = true;
                    }
                }
                break;
        }
    }

    public void SlammedGround()
    {
        if (slammingAction)
        {
            SlammingDown = false;
            currentState = ActionState.WaitForCommands;
            StartCoroutine(Shocked());
            _rigidBody.velocity = Vector3.zero;
            slammingAction = false;
            cameraShake.ShakeFor(slamShockTime);
            //TODO if player grounded should impulse pushed a bit higher, enough to freeze him for a second
        }
    }

    private IEnumerator Shocked()
    {
        yield return new WaitForSeconds(WaitTimeBeforeAttacks);
        currentState = ActionState.GettingReadyToIdle;
    }


    public void TakeDamge()
    {
        life--;
        //TODO sounds and whatever else when taking damage.
    }
}
