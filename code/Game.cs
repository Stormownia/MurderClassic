using Sandbox;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Murder
{
	[Library( "murder", Title = "Murder Classic" )]
	partial class Game : Sandbox.Game
	{
		public Hud Hud { get; set; }

		public static Game Instance
		{
			get => Current as Game;
		}

		[Net]
		public BaseRound Round { get; private set; }

		//private BaseRound _lastRound;

		[ServerVar( "min_players", Help = "The minimum players required to start." )]
		private static int MinPlayers { get; set; } = 2;

		[ServerVar( "time_to_start_round", Help = "Time to start round. (in seconds)" )]
		[Net]
		public int TimeToStartRound { get; private set; }

		public Game()
		{
			if ( IsServer )
			{
				Hud = new();
				TimeToStartRound = 5; // dodaæ mo¿liwoœæ pobierania wartoœæi z configu
			}
		}

		public override void PostLevelLoaded()
		{
			_ = StartSecondTimer();

			base.PostLevelLoaded();
			CheckMinimumPlayers();
		}


		public override void ClientDisconnect( Client client, NetworkDisconnectionReason reason )
		{
			Log.Info( client.Name + " left, checking minimum player count..." );

			Round?.OnPlayerLeave( client.Pawn as Player );

			base.ClientDisconnect( client, reason );
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );
			var player = new SandboxPlayer( client );
			player.Respawn();

			client.Pawn = player;

			Round?.OnPlayerJoin( player );
		}

		public void ChangeRound( BaseRound round )
		{
			Assert.NotNull( round );

			Round?.Finish();
			Round = round;
			Round?.Start();

			CheckRoundState( 5 );
		}

		public async Task StartSecondTimer()
		{
			while ( true )
			{
				await Task.DelaySeconds( 1 );
				OnSecond();
			}
		}

		public async Task TimeToStartRoundCountDown()
		{
			while ( true )
			{
				await Task.DelaySeconds( 1 );
				TimeToStartRound--;
				if ( TimeToStartRound == 0 ) return;
			}
		}

		public override void DoPlayerNoclip( Client player )
		{
			if ( player.Pawn is Player basePlayer )
			{
				if ( basePlayer.DevController is NoclipController )
				{
					Log.Info( "Noclip Mode Off" );
					basePlayer.DevController = null;
				}
				else
				{
					Log.Info( "Noclip Mode On" );
					basePlayer.DevController = new NoclipController();
				}
			}
		}

		private void OnSecond()
		{
			CheckMinimumPlayers();
			Round?.OnSecond();
		}

		private void CheckRoundState(int delay)
		{

			/*
			 * RESTART TIMER ON ROUND CHANGE
			 */

			if ( TimeToStartRound == 0 )
				TimeToStartRound = delay;

			if ( Round is RoundStarting )
			{
				if ( TimeToStartRound == 0 ) return;
				if ( TimeToStartRound == delay )
					TimeToStartRoundCountDown();
					return;
			}
		}

		private void CheckMinimumPlayers()
		{
			if ( Client.All.Count >= MinPlayers )
			{
				if ( Round is WaitingForPlayers || Round == null )
					ChangeRound( new RoundStarting() );
			}
			else if ( Round is not WaitingForPlayers )
			{
				ChangeRound( new WaitingForPlayers() );
			}
		}
	}
}
