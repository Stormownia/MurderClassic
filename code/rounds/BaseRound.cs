﻿using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murder
{
	public abstract partial class BaseRound : BaseNetworkable
	{
		public virtual int RoundDuration => 0;
		public virtual string RoundName => "roundname";

		/*
		 * TIPS - Bottom screen
		 */
		public virtual bool ShowTip => false;
		public virtual bool ShowTipSub => false;
		public virtual string WaitingForPly => "";
		public virtual string TipSub => "";

		/*
		 * Other Shit
		 */
		public virtual bool CanPlayerSuicide => false;
		public virtual bool ShowTimeLeft => false;
		public virtual bool ShowRoundInfo => false;
		public virtual bool ShowRoleInfo => false;

		public List<Player> Players = new();

		public float RoundEndTime { get; set; }

		public float TimeLeft
		{
			get
			{
				return RoundEndTime - Sandbox.Time.Now;
			}
		}

		[Net] public int TimeLeftSeconds { get; set; }

		public void Start()
		{
			if ( Host.IsServer && RoundDuration > 0 )
				RoundEndTime = Sandbox.Time.Now + RoundDuration;

			OnStart();
		}

		public void Finish()
		{
			if ( Host.IsServer )
			{
				RoundEndTime = 0f;
				Players.Clear();
			}

			OnFinish();
		}

		public void AddPlayer( Player player )
		{
			Host.AssertServer();

			if ( !Players.Contains( player ) )
				Players.Add( player );
		}


		public virtual void OnPlayerJoin( Player player ) { }

		public virtual void OnPlayerKilled( Player player ) { }

		public virtual void OnPlayerLeave( Player player )
		{
			Players.Remove( player );
		}

		public virtual void OnTick() { }

		public virtual void OnSecond()
		{
			if ( Host.IsServer )
			{
				if ( RoundEndTime > 0 && Sandbox.Time.Now >= RoundEndTime )
				{
					RoundEndTime = 0f;
					OnTimeUp();
				}
				else
					TimeLeftSeconds = TimeLeft.CeilToInt();
			}
		}

		protected virtual void OnStart() { }

		protected virtual void OnFinish() { }

		protected virtual void OnTimeUp() { }
	}
}
