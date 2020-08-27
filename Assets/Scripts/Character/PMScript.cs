using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PMScript : MonoBehaviour
{
    public CharacterController _characterController;
    private float _rotationSpeed = 100.0f;

    public Camera _camera;

    private float yDeg;

    private float _moveSpeed = 180.0f;

    public bool _isGrounded;

    public Transform GroundChecker;

    public LayerMask Ground;
    private float gravity = -120.0f;

    public float _verticalVelocity=0.0f;

    private float _jumpForce = 80.0f;

    public LayerMask _TurretPlatformsLayerMask;

    public TextMeshProUGUI _TurretPlacingInfo;

    public TextMeshProUGUI _TurretPauseText;
    public TextMeshProUGUI _TurretDeleteText;



    public AudioSource _footstepsSound;




    // Start is called before the first frame update
    void Start()
    {
        _isGrounded = false;
        _characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
        CheckJump();

        CheckIfLookingAtTurretPlatform();

        CheckIfMenuKeyPressed();

        CheckIfPlayerWantPause();
        OffTheTurret();
    }

    void OffTheTurret()
    {
        RaycastHit hit;

        if (Physics.Raycast(_characterController.transform.position, _characterController.transform.TransformDirection(Vector3.forward), out hit, 200.0f, _TurretPlatformsLayerMask))
        {
            
            if(hit.transform.gameObject.transform.GetChild(1).gameObject.active == true)
            {
                _TurretDeleteText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.X))
                {
                    hit.transform.gameObject.active = false;
                }
            }
        }
        else
        {
            _TurretDeleteText.gameObject.SetActive(false);
        }
    }

    void Rotate()
    {
        // character rotation
        float yRotation = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
        Vector3 rotation = new Vector3(0, yRotation, 0);

        // cam
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //

        this.transform.Rotate(rotation);

        // cam
        //_camera.transform.LookAt(transform);
        //_camera.transform.RotateAround(target, Vector3.up, yRotation);

        //

        // camera rotation
        RotateCameraWithMouse();
    }

    void RotateCameraWithMouse()
    {
        // up down rotation

        float xRotation = Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;


    }

    void Move()
    {


        float horizontal, vertical;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (vertical != 0)
        {
            _characterController.Move(transform.forward * Time.deltaTime * _moveSpeed * vertical);

            //_walkAnimation.Play();
        }

        if (horizontal != 0)
        {
            _characterController.Move(transform.right * Time.deltaTime * _moveSpeed * horizontal);
            //_walkAnimation.Play();
        }

        if(vertical != 0 || horizontal != 0)
        {
            if (!_footstepsSound.isPlaying)
                _footstepsSound.Play();
        }
        else
        {
            _footstepsSound.Stop();
        }

    }

    void CheckJump()
    {
        _isGrounded = Physics.CheckSphere(GroundChecker.position, 1.85f, Ground, QueryTriggerInteraction.Ignore);


        if (_isGrounded)
        {

            _verticalVelocity = 0.0f;

            if (Input.GetButtonDown("Jump"))
            {
                _verticalVelocity = _jumpForce;
            }
        }
        else // jeżeli jesteśmy nad ziemią
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }

        _characterController.Move(transform.up * _verticalVelocity * Time.deltaTime);

    }

    void CheckIfLookingAtTurretPlatform()
    {
        RaycastHit hit;

        if (Physics.Raycast(_characterController.transform.position, _characterController.transform.TransformDirection(Vector3.forward), out hit, 200.0f, _TurretPlatformsLayerMask))
        {


            // wyświetlić info na canvasie o możliwości postawienia wieżyczki
            if (hit.transform.gameObject.transform.GetChild(1).gameObject.active == false)
                _TurretPlacingInfo.transform.gameObject.SetActive(true);
            else
                _TurretPlacingInfo.transform.gameObject.SetActive(false);

            // obsluzyc input do postawienia wieży
            PParametersScript pParametersScript = gameObject.GetComponent<PParametersScript>();

            // sprawdzenie czy nie kupiono juz danej wiezy
            if(!hit.transform.gameObject.transform.GetChild(1).gameObject.active)
            {

                // wodna
                if(Input.GetKeyDown(KeyCode.F1) && pParametersScript._currentCoinsValue > 100)
                {
                    hit.transform.gameObject.GetComponent<TurretParametersScript>()._TurretType = "water";
                    hit.transform.gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    pParametersScript._currentCoinsValue -= 100;
                }

                // ognista
                if(Input.GetKeyDown(KeyCode.F2) && pParametersScript._currentCoinsValue > 200)
                {
                    hit.transform.gameObject.GetComponent<TurretParametersScript>()._TurretType = "fire";
                    hit.transform.gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    pParametersScript._currentCoinsValue -= 200;
                }
            }

            // jezeli kupiono to mozna ulepszyc
            else
            {
                if(Input.GetKeyDown(KeyCode.E) && pParametersScript._currentCoinsValue > 100)
                {
                    if(hit.transform.gameObject.GetComponent<TurretParametersScript>()._turretLevel < 4)
                    {
                        hit.transform.gameObject.GetComponent<TurretParametersScript>()._turretLevel++;
                        pParametersScript._currentCoinsValue -= 100;
                    }
                }
            }

        }
        else
        {
            _TurretPlacingInfo.transform.gameObject.SetActive(false);
        }

    }
    
    void CheckIfMenuKeyPressed()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            this.gameObject.GetComponent<SceneChanger>().GoToStartMenuScene();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // hitnelo coina
        if(hit.transform.tag == "CoinTag")
        {
            Destroy(hit.transform.gameObject);
            transform.GetComponent<PParametersScript>().GetCoin();
        }
    }

    void CheckIfPlayerWantPause()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                _TurretPauseText.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                _TurretPauseText.gameObject.SetActive(false);
            }
        }
    }

}
