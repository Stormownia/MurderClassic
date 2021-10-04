using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Threading.Tasks;

namespace Murder
{
	public class Tips : Panel
	{

		public Panel Wrapper;
		public Label Tip, TipSub;

		public Tips()
		{
			Wrapper = Add.Panel( "wrapper" );
			Tip = Wrapper.Add.Label( "", "tip" );
			TipSub = Wrapper.Add.Label( "", "tipSub" );
		}

		public override void Tick()
		{
			var player = Local.Pawn as Player;
			if ( player == null ) return;

			var game = Game.Instance;
			if ( game == null ) return;

			var round = game.Round;
			if ( round == null ) return;

			var CountDown = game.TimeToStartRound;
			if ( CountDown == null ) return;

			if ( !round.ShowTip )
			{
				Tip.SetClass( "hidden", true );
				return;
			}
			else
				Tip.SetClass( "hidden", false );

			/*
			 * ROUND STATE WAITING FOR PLAYERS
			 */

			if ( round.RoundName == "WAITING FOR PLAYERS" )
				Tip.Text = round.WaitingForPly;

			/*
			 * ROUND STATE STARTING
			 */

			if ( round.RoundName == "ROUND STARTING" )
			{
				Tip.Text = $"Round starts in {CountDown}";
				if ( CountDown == 0 )
					Tip.Text = "READY!";
			}

			/*
			 * MURDERER FOG???
			 */

			if ( !round.ShowTipSub )
			{
				TipSub.SetClass( "hidden", true );
				return;
			}
			else
				TipSub.SetClass( "hidden", false );

			TipSub.Text = round.TipSub;

		}
	}
}
