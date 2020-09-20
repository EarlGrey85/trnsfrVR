using Simulation;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LessonSettingsInstaller", menuName = "Installers/LessonSettingsInstaller")]
public class LessonSettingsInstaller : ScriptableObjectInstaller<LessonSettingsInstaller>
{
    [SerializeField] private MoveLesson.Settings settings = null;
    [SerializeField] private RotateTurretLesson.Settings _rotateTurretSettings = null;
    [SerializeField] private ShootingLesson.Settings _shootingLessonSettings = null;

    public override void InstallBindings()
    {
        Container.BindInstance(settings);
        Container.BindInstance(_rotateTurretSettings);
        Container.BindInstance(_shootingLessonSettings);
    }
}