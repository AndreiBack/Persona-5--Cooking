using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private Animator animator;

    [SerializeField] private Player player;

    private void Awake() {
        animator = GetComponent<Animator>();

    }
    private void Update() {
        animator.SetBool("isWalking", player.IsWalking());
        animator.SetBool("isRunning", player.IsRunning());
    }
}