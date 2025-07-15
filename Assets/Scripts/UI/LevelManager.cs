using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject skillMenu;

    [SerializeField]
    private bool isPaused = false;

    private void Update()
    {
        if (!isPaused)
        {
            skillMenu.SetActive(false);
        }
        else
        {
            skillMenu.SetActive(true);
        }
    }

    public void InitiatePauseMenu(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
    }
}
