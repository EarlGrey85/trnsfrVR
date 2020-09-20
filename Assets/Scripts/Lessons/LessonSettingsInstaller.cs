using Simulation;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LessonSettingsInstaller", menuName = "Installers/LessonSettingsInstaller")]
public class LessonSettingsInstaller : ScriptableObjectInstaller<LessonSettingsInstaller>
{
    [SerializeField] private MoveLesson.MoveLessonSettings _moveLessonSettings = null;
    [SerializeField] private RotateTurretLesson.RotateLessonSettings _rotateTurretSettings = null;
    [SerializeField] private ShootingLesson.ShootingLessonSettings _shootingLessonSettings = null;

    public override void InstallBindings()
    {
        Container.BindInstance(_moveLessonSettings);
        Container.BindInstance(_rotateTurretSettings);
        Container.BindInstance(_shootingLessonSettings);
    }
}