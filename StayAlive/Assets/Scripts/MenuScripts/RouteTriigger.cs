using UnityEngine;
using UnityEngine.SceneManagement;

public class RouteTriigger : MonoBehaviour
{
    [SerializeField]
    private int _scene = 0;
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(_scene);
        }
    }
}
