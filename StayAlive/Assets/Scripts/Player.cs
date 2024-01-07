using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private HealthBar _healthBarScript;

    public void AddDamage(int hp)
    {
        _health -= hp;
        
        _healthBarScript.UpdateHealth(-hp);
        
        if (_health <= 0)
        {
            Invoke(nameof(PlayerDied), 1.1f);
        }
    }

    public void PlayerDied()
    {
        SceneManager.LoadScene(0);
    }

    public bool isAlive() => _health >= 1;
}
