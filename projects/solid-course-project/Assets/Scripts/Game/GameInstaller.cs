using UnityEngine;
public class GameInstaller : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private GameEventListener _gameEventListener;

    private void Awake()
    {

        _gameEventListener.Configure(GetSavePersistence());
        _mainMenu.Configure(GetLoadPersistence());
    }

    private ILoader GetLoadPersistence()
    {
#if UNITY_EDITOR
        return new FilePersistence();
#endif
        return new PlayerPrefsPersistence();
    }

    private ISaver GetSavePersistence()
    {
#if UNITY_EDITOR
        return new FilePersistence();
#endif
        return new PlayerPrefsPersistence();
    }
}
