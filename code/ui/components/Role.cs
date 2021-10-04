using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace Murder
{
	public class RoleInfo : Panel
	{

		public Panel Container;
		public Label RoleName;

		public RoleInfo()
        {
			Container = Add.Panel( "role" );
			RoleName = Container.Add.Label( "bystander", "roleName" );
        }

		public override void Tick()
        {
			var player = Local.Pawn as Player;
			if (player == null) return;

			var game = Game.Instance;
			if (game == null) return;

			var round = game.Round;
			if ( round == null ) return;

			if ( !round.ShowRoleInfo )
			{
				SetClass( "hidden", true );
				return;
			}
			else
				SetClass( "hidden", false );

			RoleName.Text = round.RoundName; // still testing

        }
	}
}
