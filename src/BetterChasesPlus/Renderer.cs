using System;
using System.Collections.Generic;
using System.Drawing;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;

namespace BetterChasesPlus;

public class Renderer : Script
{
	public class UIMarker
	{
		public MarkerType Type;

		public Entity Entity;

		public Color Color;
	}

	private class BigMessage
	{
		public string text = "";

		public string description = "";

		public HudColor color = HudColor.BLACK;

		public HudColor background = HudColor.WHITE;

		public int duration = 3000;
	}

	public enum HudColor
	{
		PURE_WHITE,
		WHITE,
		BLACK,
		GREY,
		GREYLIGHT,
		GREYDARK,
		RED,
		REDLIGHT,
		REDDARK,
		BLUE,
		BLUELIGHT,
		BLUEDARK,
		YELLOW,
		YELLOWLIGHT,
		YELLOWDARK,
		ORANGE,
		ORANGELIGHT,
		ORANGEDARK,
		GREEN,
		GREENLIGHT,
		GREENDARK,
		PURPLE,
		PURPLELIGHT,
		PURPLEDARK,
		PINK,
		RADAR_HEALTH,
		RADAR_ARMOUR,
		RADAR_DAMAGE,
		NET_PLAYER1,
		NET_PLAYER2,
		NET_PLAYER3,
		NET_PLAYER4,
		NET_PLAYER5,
		NET_PLAYER6,
		NET_PLAYER7,
		NET_PLAYER8,
		NET_PLAYER9,
		NET_PLAYER10,
		NET_PLAYER11,
		NET_PLAYER12,
		NET_PLAYER13,
		NET_PLAYER14,
		NET_PLAYER15,
		NET_PLAYER16,
		NET_PLAYER17,
		NET_PLAYER18,
		NET_PLAYER19,
		NET_PLAYER20,
		NET_PLAYER21,
		NET_PLAYER22,
		NET_PLAYER23,
		NET_PLAYER24,
		NET_PLAYER25,
		NET_PLAYER26,
		NET_PLAYER27,
		NET_PLAYER28,
		NET_PLAYER29,
		NET_PLAYER30,
		NET_PLAYER31,
		NET_PLAYER32,
		SIMPLEBLIP_DEFAULT,
		MENU_BLUE,
		MENU_GREY_LIGHT,
		MENU_BLUE_EXTRA_DARK,
		MENU_YELLOW,
		MENU_YELLOW_DARK,
		MENU_GREEN,
		MENU_GREY,
		MENU_GREY_DARK,
		MENU_HIGHLIGHT,
		MENU_STANDARD,
		MENU_DIMMED,
		MENU_EXTRA_DIMMED,
		BRIEF_TITLE,
		MID_GREY_MP,
		NET_PLAYER1_DARK,
		NET_PLAYER2_DARK,
		NET_PLAYER3_DARK,
		NET_PLAYER4_DARK,
		NET_PLAYER5_DARK,
		NET_PLAYER6_DARK,
		NET_PLAYER7_DARK,
		NET_PLAYER8_DARK,
		NET_PLAYER9_DARK,
		NET_PLAYER10_DARK,
		NET_PLAYER11_DARK,
		NET_PLAYER12_DARK,
		NET_PLAYER13_DARK,
		NET_PLAYER14_DARK,
		NET_PLAYER15_DARK,
		NET_PLAYER16_DARK,
		NET_PLAYER17_DARK,
		NET_PLAYER18_DARK,
		NET_PLAYER19_DARK,
		NET_PLAYER20_DARK,
		NET_PLAYER21_DARK,
		NET_PLAYER22_DARK,
		NET_PLAYER23_DARK,
		NET_PLAYER24_DARK,
		NET_PLAYER25_DARK,
		NET_PLAYER26_DARK,
		NET_PLAYER27_DARK,
		NET_PLAYER28_DARK,
		NET_PLAYER29_DARK,
		NET_PLAYER30_DARK,
		NET_PLAYER31_DARK,
		NET_PLAYER32_DARK,
		BRONZE,
		SILVER,
		GOLD,
		PLATINUM,
		GANG1,
		GANG2,
		GANG3,
		GANG4,
		SAME_CREW,
		FREEMODE,
		PAUSE_BG,
		FRIENDLY,
		ENEMY,
		LOCATION,
		PICKUP,
		PAUSE_SINGLEPLAYER,
		FREEMODE_DARK,
		INACTIVE_MISSION,
		DAMAGE,
		PINKLIGHT,
		PM_MITEM_HIGHLIGHT,
		SCRIPT_VARIABLE,
		YOGA,
		TENNIS,
		GOLF,
		SHOOTING_RANGE,
		FLIGHT_SCHOOL,
		NORTH_BLUE,
		SOCIAL_CLUB,
		PLATFORM_BLUE,
		PLATFORM_GREEN,
		PLATFORM_GREY,
		FACEBOOK_BLUE,
		INGAME_BG,
		DARTS,
		WAYPOINT,
		MICHAEL,
		FRANKLIN,
		TREVOR,
		GOLF_P1,
		GOLF_P2,
		GOLF_P3,
		GOLF_P4,
		WAYPOINTLIGHT,
		WAYPOINTDARK,
		PANEL_LIGHT,
		MICHAEL_DARK,
		FRANKLIN_DARK,
		TREVOR_DARK,
		OBJECTIVE_ROUTE,
		PAUSEMAP_TINT,
		PAUSE_DESELECT,
		PM_WEAPONS_PURCHASABLE,
		PM_WEAPONS_LOCKED,
		END_SCREEN_BG,
		CHOP,
		PAUSEMAP_TINT_HALF,
		NORTH_BLUE_OFFICIAL,
		SCRIPT_VARIABLE_2,
		H,
		HDARK,
		T,
		TDARK,
		HSHARD,
		CONTROLLER_MICHAEL,
		CONTROLLER_FRANKLIN,
		CONTROLLER_TREVOR,
		CONTROLLER_CHOP,
		VIDEO_EDITOR_VIDEO,
		VIDEO_EDITOR_AUDIO,
		VIDEO_EDITOR_TEXT,
		HB_BLUE,
		HB_YELLOW
	}

	private Scaleform Scaleform = Scaleform.RequestMovie("MP_BIG_MESSAGE_FREEMODE");

	private static List<string> HelpMessages = new List<string>();

	private static List<BigMessage> BigMessages = new List<BigMessage>();

	public static List<UIMarker> Markers = new List<UIMarker>();

	private int HelpMessageTime = 0;

	private int HelpMessageInterval = 5000;

	private int BigMessageTime = 0;

	private int BigMessageInterval = 5000;

	private int BetterChasesIconOffsetX;

	private int BetterChasesIconOffsetY;

	private int ArrestWarrantsIconOffsetX;

	private int ArrestWarrantsIconOffsetY;

	private int ArrestWarrantsTextOffsetX;

	private int ArrestWarrantsTextOffsetY;

	private int ArrestWarrantsGradientOffsetX;

	private int ArrestWarrantsGradientOffsetY;

	private int ShowBetterChasesHUDUntil = 0;

	private int ShowArrestWarrantsHUDUntil = 0;

	private int OffsetChangeDisplayLength = 5000;

	private readonly Sprite CopsRamSprite = new Sprite("mpinventory", "vehicle_deathmatch", new SizeF(18f, 18f), new PointF(1015f, 5f));

	private readonly Sprite CopsShootSprite = new Sprite("mpinventory", "survival", new SizeF(18f, 18f), new PointF(1000f, 5f));

	private readonly Sprite PedWantedSprite = new Sprite("mpinventory", "mp_specitem_ped", new SizeF(16f, 16f), new PointF(1150f, 701f));

	private readonly Sprite CarWantedSprite = new Sprite("mpinventory", "mp_specitem_car", new SizeF(16f, 16f), new PointF(1136f, 701f));

	private readonly Sprite PlaneWantedSprite = new Sprite("mpinventory", "mp_specitem_plane", new SizeF(16f, 16f), new PointF(1136f, 701f));

	private readonly Sprite HeliWantedSprite = new Sprite("mpinventory", "mp_specitem_heli", new SizeF(16f, 16f), new PointF(1136f, 701f));

	private readonly Sprite BoatWantedSprite = new Sprite("mpinventory", "mp_specitem_boat", new SizeF(16f, 16f), new PointF(1136f, 701f));

	private readonly Sprite BikeWantedSprite = new Sprite("mpinventory", "mp_specitem_bike", new SizeF(16f, 16f), new PointF(1136f, 701f));

	private readonly Sprite WantedGradient = new Sprite("timerbars", "all_white_bg", new SizeF(500f, 22f), new PointF(790f, 698f), Color.DarkRed);

	private readonly Sprite MichaelGradient = new Sprite("timerbars", "all_white_bg", new SizeF(500f, 22f), new PointF(790f, 698f), Color.CornflowerBlue);

	private readonly Sprite FranklinGradient = new Sprite("timerbars", "all_white_bg", new SizeF(500f, 22f), new PointF(790f, 698f), Color.ForestGreen);

	private readonly Sprite TrevorGradient = new Sprite("timerbars", "all_white_bg", new SizeF(500f, 22f), new PointF(790f, 698f), Color.DarkOrange);

	private readonly Sprite DimGradient = new Sprite("timerbars", "all_white_bg", new SizeF(500f, 22f), new PointF(790f, 698f), Color.DimGray);

	private readonly ContainerElement SpottedMeterBG = new ContainerElement(new PointF(1135f, 665f), new SizeF(112f, 30f), Color.FromArgb(220, Color.Black));

	private TextElement SuspectWanted = new TextElement("~w~SUSPECT WANTED", new PointF(1130f, 700f), 0.4f, Color.White, (GTA.UI.Font)0);

	private TextElement WarrantActive = new TextElement("~w~WARRANT ACTIVE", new PointF(1130f, 700f), 0.4f, Color.White, (GTA.UI.Font)0);

	private TextElement SpottedMeterText = new TextElement("", new PointF(1190f, 665f), 0.3f, Color.White, (GTA.UI.Font)0, (Alignment)0);

	public Renderer()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Expected O, but got Unknown
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Expected O, but got Unknown
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Expected O, but got Unknown
		base.Tick += OnTick;
		base.Interval = 4;
		Function.Call(unchecked((Hash)(-2332038263791780395L)), (InputArgument[])(object)new InputArgument[2]
		{
			(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
			(InputArgument)true
		});
	}

	private void OnTick(object sender, EventArgs e)
	{
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Expected O, but got Unknown
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Expected O, but got Unknown
		//IL_0a91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ade: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_0873: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0917: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0979: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_0998: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
		if (Function.Call<bool>(unchecked((Hash)(-6312787983359068731L)), Array.Empty<InputArgument>()) || Function.Call<bool>(unchecked((Hash)(-2750907727005174945L)), Array.Empty<InputArgument>()))
		{
			return;
		}
		Ped character = Game.Player.Character;
		int wantedLevel = Game.Player.Wanted.WantedLevel;
		if ((Config.Options.BetterChases.IconOffsetX != 0 && Config.Options.BetterChases.IconOffsetX != BetterChasesIconOffsetX) || (Config.Options.BetterChases.IconOffsetY != 0 && Config.Options.BetterChases.IconOffsetY != BetterChasesIconOffsetY))
		{
			CopsRamSprite.Position = new PointF(1015f + (float)Config.Options.BetterChases.IconOffsetX, 5 + Config.Options.BetterChases.IconOffsetY);
			CopsShootSprite.Position = new PointF(1000f + (float)Config.Options.BetterChases.IconOffsetX, 5 + Config.Options.BetterChases.IconOffsetY);
			ShowBetterChasesHUDUntil = Game.GameTime;
			BetterChasesIconOffsetX = Config.Options.BetterChases.IconOffsetX;
			BetterChasesIconOffsetY = Config.Options.BetterChases.IconOffsetY;
		}
		else if ((Config.Options.ArrestWarrants.IconOffsetX != 0 && Config.Options.ArrestWarrants.IconOffsetX != ArrestWarrantsIconOffsetX) || (Config.Options.ArrestWarrants.IconOffsetY != 0 && Config.Options.ArrestWarrants.IconOffsetY != ArrestWarrantsIconOffsetY))
		{
			PedWantedSprite.Position = new PointF(1150f + (float)Config.Options.ArrestWarrants.IconOffsetX, 701f + (float)Config.Options.ArrestWarrants.IconOffsetY);
			CarWantedSprite.Position = new PointF(1136f + (float)Config.Options.ArrestWarrants.IconOffsetX, 701f + (float)Config.Options.ArrestWarrants.IconOffsetY);
			PlaneWantedSprite.Position = new PointF(1136f + (float)Config.Options.ArrestWarrants.IconOffsetX, 701f + (float)Config.Options.ArrestWarrants.IconOffsetY);
			HeliWantedSprite.Position = new PointF(1136f + (float)Config.Options.ArrestWarrants.IconOffsetX, 701f + (float)Config.Options.ArrestWarrants.IconOffsetY);
			BoatWantedSprite.Position = new PointF(1136f + (float)Config.Options.ArrestWarrants.IconOffsetX, 701f + (float)Config.Options.ArrestWarrants.IconOffsetY);
			BikeWantedSprite.Position = new PointF(1136f + (float)Config.Options.ArrestWarrants.IconOffsetX, 701f + (float)Config.Options.ArrestWarrants.IconOffsetY);
			ShowArrestWarrantsHUDUntil = Game.GameTime;
			ArrestWarrantsIconOffsetX = Config.Options.ArrestWarrants.IconOffsetX;
			ArrestWarrantsIconOffsetY = Config.Options.ArrestWarrants.IconOffsetY;
		}
		else if ((Config.Options.ArrestWarrants.TextOffsetX != 0 && Config.Options.ArrestWarrants.TextOffsetX != ArrestWarrantsTextOffsetX) || (Config.Options.ArrestWarrants.TextOffsetY != 0 && Config.Options.ArrestWarrants.TextOffsetY != ArrestWarrantsTextOffsetY))
		{
			SuspectWanted = new TextElement("~w~SUSPECT WANTED", new PointF(1130f + (float)Config.Options.ArrestWarrants.TextOffsetX, 700f + (float)Config.Options.ArrestWarrants.TextOffsetY), 0.4f, Color.White, (GTA.UI.Font)0);
			WarrantActive = new TextElement("~w~WARRANT ACTIVE", new PointF(1130f + (float)Config.Options.ArrestWarrants.TextOffsetX, 700f + (float)Config.Options.ArrestWarrants.TextOffsetY), 0.4f, Color.White, (GTA.UI.Font)0);
			ShowArrestWarrantsHUDUntil = Game.GameTime;
			ArrestWarrantsTextOffsetX = Config.Options.ArrestWarrants.TextOffsetX;
			ArrestWarrantsTextOffsetY = Config.Options.ArrestWarrants.TextOffsetY;
		}
		else if ((Config.Options.ArrestWarrants.GradientOffsetX != 0 && Config.Options.ArrestWarrants.GradientOffsetX != ArrestWarrantsGradientOffsetX) || (Config.Options.ArrestWarrants.GradientOffsetY != 0 && Config.Options.ArrestWarrants.GradientOffsetY != ArrestWarrantsGradientOffsetY))
		{
			WantedGradient.Position = new PointF(790f + (float)Config.Options.ArrestWarrants.GradientOffsetX, 698f + (float)Config.Options.ArrestWarrants.GradientOffsetY);
			MichaelGradient.Position = new PointF(790f + (float)Config.Options.ArrestWarrants.GradientOffsetX, 698f + (float)Config.Options.ArrestWarrants.GradientOffsetY);
			FranklinGradient.Position = new PointF(790f + (float)Config.Options.ArrestWarrants.GradientOffsetX, 698f + (float)Config.Options.ArrestWarrants.GradientOffsetY);
			TrevorGradient.Position = new PointF(790f + (float)Config.Options.ArrestWarrants.GradientOffsetX, 698f + (float)Config.Options.ArrestWarrants.GradientOffsetY);
			DimGradient.Position = new PointF(790f + (float)Config.Options.ArrestWarrants.GradientOffsetX, 698f + (float)Config.Options.ArrestWarrants.GradientOffsetY);
			ShowArrestWarrantsHUDUntil = Game.GameTime;
			ArrestWarrantsGradientOffsetX = Config.Options.ArrestWarrants.GradientOffsetX;
			ArrestWarrantsGradientOffsetY = Config.Options.ArrestWarrants.GradientOffsetY;
		}
		Model model;
		if (Config.IsMenuOpen && ShowBetterChasesHUDUntil != 0 && ShowBetterChasesHUDUntil + OffsetChangeDisplayLength > Game.GameTime)
		{
			CopsRamSprite.Draw();
			CopsShootSprite.Draw();
		}
		else if (Config.IsMenuOpen && ShowArrestWarrantsHUDUntil != 0 && ShowArrestWarrantsHUDUntil + OffsetChangeDisplayLength > Game.GameTime)
		{
			WantedGradient.Draw();
			PedWantedSprite.Draw();
			CarWantedSprite.Draw();
			SuspectWanted.Draw();
		}
		else
		{
			if (Config.Options.BetterChases.ShowHUD && BetterChases.ActiveChase.PITAuthorized && wantedLevel > 0)
			{
				CopsRamSprite.Draw();
			}
			if (Config.Options.BetterChases.ShowHUD && BetterChases.ActiveChase.DeadlyForce && wantedLevel > 0)
			{
				CopsShootSprite.Draw();
			}
			if (Config.Options.ArrestWarrants.ShowHUD)
			{
				if (ArrestWarrants.PedWarrant.pedHash != 0)
				{
					goto IL_07ec;
				}
				if (ArrestWarrants.VehicleWarrant.vehicleHash != 0 && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
				{
					model = ((Entity)character.CurrentVehicle).Model;
					if (model.Hash == ArrestWarrants.VehicleWarrant.vehicleHash)
					{
						goto IL_07ec;
					}
				}
			}
		}
		goto IL_0a48;
		IL_07ec:
		if (wantedLevel > 0 || Function.Call<bool>((Hash)751735369465373403L, (InputArgument[])(object)new InputArgument[1] { (Player)Game.Player }))
		{
			SuspectWanted.Draw();
			WantedGradient.Draw();
		}
		else
		{
			WarrantActive.Draw();
			if (((Entity)character).Model == (Model)((PedHash)225514697))
			{
				MichaelGradient.Draw();
			}
			else if (((Entity)character).Model == (Model)(unchecked((PedHash)(-1692214353))))
			{
				FranklinGradient.Draw();
			}
			else if (((Entity)character).Model == (Model)(unchecked((PedHash)(-1686040670))))
			{
				TrevorGradient.Draw();
			}
			else
			{
				DimGradient.Draw();
			}
		}
		if (ArrestWarrants.PedWarrant.pedHash != 0)
		{
			PedWantedSprite.Draw();
		}
		if (ArrestWarrants.VehicleWarrant.vehicleHash != 0 && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
		{
			model = ((Entity)character.CurrentVehicle).Model;
			if (model.Hash == ArrestWarrants.VehicleWarrant.vehicleHash)
			{
				model = Helpers.GetModel(ArrestWarrants.VehicleWarrant.vehicleHash);
				if (model.IsCar)
				{
					CarWantedSprite.Draw();
				}
				else
				{
					model = Helpers.GetModel(ArrestWarrants.VehicleWarrant.vehicleHash);
					if (!model.IsBicycle)
					{
						model = Helpers.GetModel(ArrestWarrants.VehicleWarrant.vehicleHash);
						if (!model.IsBike)
						{
							model = Helpers.GetModel(ArrestWarrants.VehicleWarrant.vehicleHash);
							if (model.IsBoat)
							{
								BoatWantedSprite.Draw();
							}
							else
							{
								model = Helpers.GetModel(ArrestWarrants.VehicleWarrant.vehicleHash);
								if (model.IsPlane)
								{
									PlaneWantedSprite.Draw();
								}
								else
								{
									model = Helpers.GetModel(ArrestWarrants.VehicleWarrant.vehicleHash);
									if (model.IsHelicopter)
									{
										HeliWantedSprite.Draw();
									}
								}
							}
							goto IL_0a48;
						}
					}
					BikeWantedSprite.Draw();
				}
			}
		}
		goto IL_0a48;
		IL_0a48:
		if (Config.Options.ArrestWarrants.ShowSpottedIndicators && Markers.Count > 0)
		{
			foreach (UIMarker marker in Markers)
			{
				World.DrawMarker(marker.Type, marker.Entity.Position + new Vector3(0f, 0f, 1.5f), new Vector3(0f, 0f, 0f), new Vector3(0f, 180f, 0f), new Vector3(0.5f, 0.5f, 0.5f), marker.Color, true, false, true, "", "", false);
			}
		}
		if (Config.Options.ArrestWarrants.ShowSpottedMeter && Markers.Count > 0 && ArrestWarrants.ShowSpottedMeter)
		{
			SpottedMeterBG.Draw();
			SpottedMeterText.Caption = "A cop is watching you.~n~~y~Recognition process: ~r~" + Math.Round(ArrestWarrants.SpottedMeter, 0) + "%";
			SpottedMeterText.Draw();
		}
		if (HelpMessages.Count > 0 && Game.GameTime > HelpMessageTime + HelpMessageInterval)
		{
			Function.Call(unchecked((Hash)(-8860350453193909743L)), (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
			Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)HelpMessages[0] });
			Function.Call((Hash)2562546386151446694L, (InputArgument[])(object)new InputArgument[4]
			{
				(InputArgument)0,
				(InputArgument)0,
				(InputArgument)0,
				(InputArgument)HelpMessageInterval
			});
			HelpMessageTime = Game.GameTime;
			HelpMessages.RemoveAt(0);
		}
		if (BigMessages.Count > 0 && BigMessageTime == 0 && Game.GameTime > BigMessageTime + BigMessageInterval)
		{
			BigMessage bigMessage = BigMessages[0];
			Scaleform.CallFunction("SHOW_SHARD_CENTERED_MP_MESSAGE", new object[4]
			{
				bigMessage.text,
				bigMessage.description,
				(int)bigMessage.color,
				(int)bigMessage.background
			});
			Function.Call((Hash)7477453855356397301L, (InputArgument[])(object)new InputArgument[3]
			{
				(InputArgument)0,
				(InputArgument)"CHECKPOINT_NORMAL",
				(InputArgument)"HUD_MINI_GAME_SOUNDSET"
			});
			BigMessageTime = Game.GameTime;
			BigMessageInterval = bigMessage.duration;
		}
		else if (BigMessageTime > 0 && Game.GameTime < BigMessageTime + BigMessageInterval)
		{
			Scaleform.Render2D();
		}
		else if (BigMessageTime > 0 && Game.GameTime > BigMessageTime + BigMessageInterval)
		{
			Scaleform.CallFunction("TRANSITION_OUT", Array.Empty<object>());
			BigMessageTime = 0;
			BigMessages.RemoveAt(0);
		}
	}

	public static void ShowSubtitle(string text, int duration = 2500)
	{
		Function.Call(unchecked((Hash)(-5153745325143710083L)), (InputArgument[])(object)new InputArgument[1] { (InputArgument)"CELL_EMAIL_BCON" });
		Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)text });
		Function.Call(unchecked((Hash)(-7100200333308705802L)), (InputArgument[])(object)new InputArgument[2]
		{
			(InputArgument)duration,
			(InputArgument)true
		});
	}

	public static void ShowHelpMessage(string text)
	{
		HelpMessages.Add(text);
	}

	public static void ShowBigMessage(string text, string description, HudColor color, HudColor background, int duration = 3000)
	{
		BigMessage item = new BigMessage
		{
			text = text,
			description = description,
			color = color,
			background = background,
			duration = duration
		};
		BigMessages.Add(item);
	}
}
