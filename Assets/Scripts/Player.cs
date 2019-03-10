using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _maxPlayerSpeed;
    [SerializeField] private float _laneSpeed;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private BoxCollider _boxCollider;
    private Vector3 _boxColliderSize;


    private int _currentLane = 1;
    private Vector3 _verticalTargetPosition;

    private bool isJumping = false;
    private float _jumpStart;
    [SerializeField] private float _jumpLength;
    [SerializeField] private float _jumpHeight;

    private bool isSliding = false;
    private float _slideStart;
    [SerializeField] private float _slideLength;

    [SerializeField] private GameView _gameView;
    private float _score;

	void Start ()
    {
        _playerAnimator.SetBool("Dead", false);

        _boxColliderSize = _boxCollider.size;
        _playerAnimator.Play("runStart");
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }

        if (isJumping)
        {
            float ratio = (transform.position.z - _jumpStart) / _jumpLength;
            if(ratio >= 1f)
            {
                isJumping = false;
                _playerAnimator.SetBool("Jumping", false);
            }
            else
            {
                _verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * _jumpHeight;
            }
        }
        else
        {
            // trazer o personagem de volta para o chão, já que nâo estou usando a gravidade
            _verticalTargetPosition.y = Mathf.MoveTowards(_verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if (isSliding)
        {
            float ratio = (transform.position.z - _slideStart) / _slideLength;
            if(ratio >= 1)
            {
                isSliding = false;
                _playerAnimator.SetBool("Sliding", false);
                _boxCollider.size = _boxColliderSize;
            }
        }

        Vector3 targetPositon = new Vector3(_verticalTargetPosition.x, _verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPositon, _laneSpeed * Time.deltaTime);

        _score += Time.deltaTime * _playerSpeed;
        _gameView.UpdateScoreText((int)_score);
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = Vector3.forward * _playerSpeed;
    }

    private void ChangeLane(int direction)
    {
        int targetLane = _currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;

        _currentLane = targetLane;
        _verticalTargetPosition = new Vector3((_currentLane -1),0,0);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            _jumpStart = transform.position.z;
            _playerAnimator.SetFloat("JumpSpeed", _playerSpeed / _jumpLength);
            _playerAnimator.SetBool("Jumping", true);
            isJumping = true;
        }
    }

    private void Slide()
    {
        if(!isJumping && !isSliding)
        {
            _slideStart = transform.position.z;
            _playerAnimator.SetFloat("JumpSpeed", _playerSpeed / _slideLength);
            _playerAnimator.SetBool("Sliding", true);

            Vector3 newBoxColliderSize = _boxCollider.size;
            newBoxColliderSize.y = newBoxColliderSize.y / 2;
            _boxCollider.size = newBoxColliderSize;

            isSliding = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _playerAnimator.SetTrigger("Hit");
            _playerSpeed = 0;
            _playerAnimator.SetBool("Dead", true);
            _gameView.GameOver();
        }
    }

    public void IncreaseSpeed()
    {
        _playerSpeed *= 1.15f;
        if (_playerSpeed >= _maxPlayerSpeed)
        {
            _playerSpeed = _maxPlayerSpeed;
        }
    }
}
