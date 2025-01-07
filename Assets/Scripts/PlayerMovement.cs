using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    Vector2 moveVector;  // Вектор движения
    public float moveSpeed = 5f;  // Скорость движения
    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    private bool facingRight = true;  // Указывает, смотрит ли персонаж вправо


    public event EventHandler OnPlayerAttack;

    private void Awake()
    {
        Instance = this;
    }

    // Получение ввода от пользователя
    public void InputPlayer(InputAction.CallbackContext _context)
    {
        moveVector = _context.ReadValue<Vector2>();
    }

    public void InputPlayer2(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
            Debug.Log("Кнопка нажата (начало нажатия)");
            
        }
        
        else if (_context.canceled)
        {
            Debug.Log("Кнопка отпущена");
            
        }
    }

    private void Start()
    {
        OnPlayerAttack += Player_OnPlayerAttack;

    }
    private void Player_OnPlayerAttack(object sender, EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void Update()
    {
        // Создаем вектор для 2D движения
        Vector2 movement = new Vector2(moveVector.x, moveVector.y);
        movement.Normalize();  // Нормализуем вектор для равномерного движения
        transform.Translate(moveSpeed * movement * Time.deltaTime);  // Применяем движение к объекту

        // Проверка направления движения и разворот спрайта
        if (movement.x > minMovingSpeed && !facingRight)
        {
            Flip();
        }
        else if (movement.x < -minMovingSpeed && facingRight)
        {
            Flip();
        }

        // Проверка, движется ли игрок
        if (Mathf.Abs(movement.x) > minMovingSpeed || Mathf.Abs(movement.y) > minMovingSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // Разворачиваем по оси X
        transform.localScale = scale;
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
