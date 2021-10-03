using UnityEngine;
using UnityEngine.SceneManagement;

public class People : MonoBehaviour
{
    public Color color;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer rendererComponent = transform.GetChild(i).GetComponent<Renderer>();
            rendererComponent.material.SetColor("_Color", color);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollisionCone")
        {
            Transform snake = other.transform.parent.parent;
            Snake snakeComponent = snake.GetComponent<Snake>();

            if (!snakeComponent.IsFeverActive())
            {
                Color snakeColor = snakeComponent.GetColor();

                if (snakeColor != color)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

            snakeComponent.AddPart();
            Destroy(gameObject);
        }
    }
}

