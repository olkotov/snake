using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Color color;

    void Start()
    {
        Renderer rendererComponent = transform.GetComponent<Renderer>();
        rendererComponent.material.SetColor("_Color", color);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnakeHead")
        {
            Transform snake = other.transform.parent;
            Snake snakeComponent = snake.GetComponent<Snake>();

            snakeComponent.ChangeColor(color);
        }
    }
}

