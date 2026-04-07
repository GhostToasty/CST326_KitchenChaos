using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    //static class: cannot be attached to an object or any instances || good if everything in this class is meant to be static  

    public enum Scene
    {
        MainMenuScene, 
        GameScene,
        LoadingScene
    }
    
    private static Scene targetScene;  


    public static void Load(Scene targetSceneSelect)
    {
        Loader.targetScene = targetSceneSelect;
        
        //load scene is selected and then loaded
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }


    public static void LoaderCallback()
    {
        //loads selected scene
        SceneManager.LoadScene(targetScene.ToString());
    }

}
