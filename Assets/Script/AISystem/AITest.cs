using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ActionSystem;
using Script.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.AISystem
{
    public class AITest : MonoBehaviour
    {
        [SerializeField] private float detectionRadius = 2f;
        [SerializeField] private LayerMask visibleLayerMask;

        public List<Transform> PathPoints;
        private Transform currentTarget;

        public Movement _move;

        private GameObject _player;
        private SpriteRenderer _currentSpriteRenderer;

        private bool isWaiting = false;
        private bool isChasing = false;

        private void Start()
        {
            _player = GameObject.Find("Winter");
            if(_player == null)
            {
                Debug.LogError("Player Winter not found!");
            }

            _currentSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (isWaiting)
            {
                Vector3 _moveDirection = Vector3.zero;
                new MoveCommand(_move, _moveDirection, false).Execute();
                return;
            }

            // if colliding with player, back to main menu
            if (Vector3.Distance(transform.position, _player.transform.position) < 0.3f)
            {
                SceneManager.LoadScene("MainMenu");
            }

            if (CanSeePlayer())
            {
                isChasing = true;
                currentTarget = _player.transform;
                _currentSpriteRenderer.color = Color.red;
                MoveToTarget(true);
            }
            else
            {
                if (isChasing)  // If character was chasing but now lost the player
                {
                    isChasing = false;
                    ChooseRandomPathPoint();
                }

                _currentSpriteRenderer.color = Color.black;
                HandleRandomMovement();
            }
        }

        private void HandleRandomMovement()
        {
            if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.position) < 1f)
            {
                StartCoroutine(WaitAtPoint());
                ChooseRandomPathPoint();
            }
            else
            {
                MoveToTarget();
            }
        }

        IEnumerator WaitAtPoint()
        {
            isWaiting = true;
            yield return new WaitForSeconds(2f);  // Wait for 2 seconds
            isWaiting = false;
        }

        private List<Transform> recentPoints = new List<Transform>();
        private void ChooseRandomPathPoint()
        {
            if (PathPoints.Count == 0) return;

            List<Transform> availablePoints = PathPoints.Except(recentPoints).ToList();
            if (availablePoints.Count == 0)
            {
                recentPoints.Clear();
                availablePoints = PathPoints;
            }

            int randomIndex = Random.Range(0, availablePoints.Count);
            currentTarget = availablePoints[randomIndex];
            recentPoints.Add(currentTarget);
            if (recentPoints.Count > 2) // Adjust the number based on how many points you want to exclude
            {
                recentPoints.RemoveAt(0);
            }
        }

        private void MoveToTarget(bool isRunning = false)
        {
            Vector3 _moveDirection = Vector3.zero;

            // if there is no target, return
            if (currentTarget == null) return;

            // if target is on the left, move left
            if (currentTarget.position.x < transform.position.x) _moveDirection.x -= 1;

            // if target is on the right, move right
            else if (currentTarget.position.x > transform.position.x) _moveDirection.x += 1;

            new MoveCommand(_move, _moveDirection, isRunning).Execute();
        }

        public bool CanSeePlayer()
        {
            if(_player == null) return false;

            Vector3 toPlayer = _player.transform.position - transform.position;

            if (toPlayer.magnitude <= detectionRadius)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toPlayer.normalized, detectionRadius, visibleLayerMask.value);

                // Check if the ray hit the player Winter
                if (hit.collider.gameObject == _player && hit.collider.gameObject.name == "Winter")
                {
                    return true;
                }
            }
            return false;
        }
    }
}