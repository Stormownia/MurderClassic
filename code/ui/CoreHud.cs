using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Threading.Tasks;

namespace Murder
{
	[Library]
	public partial class Hud : HudEntity<RootPanel>
	{
		public Hud()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/ui/scss/CoreHud.scss" );

			RootPanel.AddChild<RoleInfo>();
			RootPanel.AddChild<Tips>();

			//RootPanel.AddChild<Vitals>();
			//RootPanel.AddChild<NameTags>();
			//RootPanel.AddChild<CrosshairCanvas>();
			//RootPanel.AddChild<ChatBox>();
			//RootPanel.AddChild<VoiceList>();
			//RootPanel.AddChild<KillFeed>();
			//RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
			//RootPanel.AddChild<InventoryBar>();
			//RootPanel.AddChild<CurrentTool>();
			//RootPanel.AddChild<SpawnMenu>();
		}
	}
}
