using UnityEngine;
using UnityEngine.SceneManagement;

public class Thorn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnakeHead")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.tag == "CollisionCone")
        {
            Transform snake = other.transform.parent.parent;
            Snake snakeComponent = snake.GetComponent<Snake>();

            if (snakeComponent.IsFeverActive())
            {
                snakeComponent.AddPart();
                Destroy(gameObject);
            }
        }
    }
}

