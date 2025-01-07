using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private bool facingRight = true;

    // Метод для установки направления из PlayerMovement
    public void SetDirection(bool isFacingRight)
    {
        // Проверяем, изменилось ли направление
        if (facingRight != isFacingRight)
        {
            facingRight = isFacingRight;
            Flip();
        }
    }

    // Метод для переворота спрайта по оси X
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // Инвертируем масштаб по оси X
        transform.localScale = scale;
    }
}
