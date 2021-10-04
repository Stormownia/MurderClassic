using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murder
{
	public class WaitingForPlayers : BaseRound
	{
		public override string RoundName => "WAITING FOR PLAYERS";
		public override bool ShowTip => true;
		public override bool ShowTipSub => false;
		public override string WaitingForPly => "Not enough players to start round";
		public override string TipSub => "";

		protected override void OnStart()
		{
			Log.Info( "[MURDER CLASSIC] - Started Waiting for players Round" );

			if ( Host.IsServer )
			{
				var players = Client.All.Select( ( client ) => client.Pawn as Player );

				foreach ( var player in players )
					OnPlayerJoin( player );

				//Game.Instance
			}
		}

		protected override void OnFinish()
		{
			Log.Info( "[MURDER CLASSIC] - Finished Waiting for players Round" );
		}

		public override void OnPlayerJoin( Player player )
		{
			if ( Players.Contains( player ) ) return;

			//player.MakeSpectator( true );

			AddPlayer( player );

			base.OnPlayerJoin( player );
		}
	}
}
