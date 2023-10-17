using Script.ActionSystem;
using UnityEngine;

namespace Script.AISystem
{
    public class MoveTo : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Movement _move;
    }
}