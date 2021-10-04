using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murder
{
	public class RoundStarting : BaseRound
	{
		public override string RoundName => "ROUND STARTING";

		public override bool ShowTip => true;
		public override bool ShowTipSub => false;

		public override string WaitingForPly => "";
		public override string TipSub => "";

		protected override void OnStart()
		{
			//Log.Info( "[MURDER CLASSIC] - Round Starting" );

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
			//Log.Info( "[MURDER CLASSIC] - Finished Round Starting" );
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
