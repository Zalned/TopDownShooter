using UnityEngine;
using Zenject;

public class ProjectBootstrap {

    [Inject]
    public ProjectBootstrap() {
        Application.targetFrameRate = 144;
    }
}