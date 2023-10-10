using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private Text _healthText;

    public void SetHealth(string health)
    {
        _healthText.text = health;
    }
}
