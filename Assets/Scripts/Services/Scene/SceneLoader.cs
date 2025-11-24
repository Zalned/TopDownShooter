using UnityEngine.SceneManagement;

public class SceneLoader {
    public void LoadGameScene() {
        LoadScene( Defines.SceneNames.GameScene );
    }

    public void LoadMenuScene() {
        LoadScene( Defines.SceneNames.MainMenu );
    }

    private void LoadScene( string sceneName ) { 
        SceneManager.LoadScene( sceneName );
    }
}