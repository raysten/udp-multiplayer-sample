using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClientInstaller : MonoInstaller
{
	[SerializeField]
	private InputField _inputField;
	[SerializeField]
	private Button _connectButton;
	[SerializeField]
	private GameObject controlledPlayerPrefab;
	[SerializeField]
	private GameObject remotePlayerPrefab;
	[SerializeField]
	private GameLoop gameLoop;
	[SerializeField]
	private Ball ball;
	[SerializeField]
	private ScoreUI scoreUI;
	[SerializeField]
	private DebugScreen debugScreen;

	public override void InstallBindings()
	{
		InstallGameLoop();
		InstallClient();
		InstallMessageScripts();
		InstallConnectionGUI();
		InstallMessageHandlers();
		InstallSpawner();
		InstallPlayer();
		InstallBall();
		InstallHelpers();
		InstallMisc();
		InstallScore();
		Container.BindInterfacesAndSelfTo<DebugScreen>().FromInstance(debugScreen).AsSingle();
	}

	private void InstallGameLoop()
	{
		Container.BindInterfacesAndSelfTo<GameLoop>().FromInstance(gameLoop).AsSingle();
	}

	private void InstallClient()
	{
		Container.BindInterfacesAndSelfTo<LocalClient>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerClockSyncSystem>().AsSingle();
	}

	private void InstallMessageScripts()
	{
		Container.BindInterfacesAndSelfTo<MessageSerializer>().AsSingle();
		Container.BindInterfacesAndSelfTo<MessageFactory>().AsSingle();
		Container.BindInterfacesAndSelfTo<MessageProcessor>().AsSingle();
	}

	private void InstallConnectionGUI()
	{
		Container.BindInstance(_inputField).AsSingle();
		Container.BindInstance(_connectButton).AsSingle();
		Container.BindInterfacesAndSelfTo<ConnectionGUI>().AsSingle();
	}

	private void InstallMessageHandlers()
	{
		Container.BindInterfacesAndSelfTo<SpawnPlayerMessageHandler>().AsSingle();
		Container.BindInterfacesAndSelfTo<ServerClockMessageHandler>().AsSingle();
		Container.BindInterfacesAndSelfTo<SnapshotHandler>().AsSingle();
		Container.BindInterfacesAndSelfTo<ScoreMessageHandler>().AsSingle();
	}

	private void InstallSpawner()
	{
		Container.BindInterfacesAndSelfTo<PlayerSpawner>().AsSingle();
	}

	private void InstallPlayer()
	{
		Container.BindFactory<Team, ControlledPlayer, ControlledPlayer.Factory>()
			.FromComponentInNewPrefab(controlledPlayerPrefab);
		Container.BindFactory<Team, RemotePlayer, RemotePlayer.Factory>()
			.FromComponentInNewPrefab(remotePlayerPrefab);
		Container.BindInterfacesAndSelfTo<PlayerInputSystem>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerRegistry>().AsSingle();
		Container.BindInterfacesAndSelfTo<PlayerReconciler>().AsSingle();
	}

	private void InstallBall()
	{
		Container.BindInterfacesAndSelfTo<Ball>().FromInstance(ball).AsSingle();
	}

	private void InstallHelpers()
	{
		Container.BindInterfacesAndSelfTo<PortFinder>().AsSingle();
	}

	private void InstallMisc()
	{
		Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
	}

	private void InstallScore()
	{
		Container.BindInterfacesAndSelfTo<ScoreUI>().FromInstance(scoreUI).AsSingle();
	}
}
