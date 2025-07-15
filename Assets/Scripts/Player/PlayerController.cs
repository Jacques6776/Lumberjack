using Scripts.Skill_System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField]
    private float startingMoveSpeed = 5f;
    private float currentMoveSpeed;
    private Vector2 moveVector;
    private Rigidbody2D playerRB;

    //the list containing the unlocked skills
    private PlayerSkillManager playerSkillManager;

    //Attack
    [SerializeField]
    private ScriptableSkill attackSkill;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        currentMoveSpeed = startingMoveSpeed;
    }

    private void Start()
    {
        playerSkillManager = FindFirstObjectByType<PlayerSkillManager>();
    }

    private void Update()
    {
        MovePlayer();
    }

    //Movement
    public void InitiateMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        Vector2 movement = new Vector2(moveVector.x, moveVector.y);
        playerRB.linearVelocity = movement * currentMoveSpeed;
    }

    //Attack
    public void InitiateAttack(InputAction.CallbackContext context)
    {
        PlayerAttackAction();
    }

    private void PlayerAttackAction()
    {
        if (playerSkillManager.unlockedSkills.Contains(attackSkill))
        {
            Debug.Log("Attacked");
        }
        else
        {
            Debug.Log("No attack");
            return;
        }
    }
}
