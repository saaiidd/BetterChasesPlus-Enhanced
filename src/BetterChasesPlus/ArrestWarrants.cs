using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using GTA;
using GTA.Math;
using GTA.Native;

namespace BetterChasesPlus;

public class ArrestWarrants : Script
{
	public class Warrant
	{
		public int WantedLevel = 0;

		public int WarrantCheckTime = 0;

		public DateTime WarrantIssueTime = default(DateTime);

		public Vector3 LastKnownLocation;

		public VehicleDescription VehicleDescription = new VehicleDescription();

		public PedDescription PedDescription = new PedDescription();

		public int pedHash = 0;

		public int vehicleHash = 0;

		public BetterChases.Chase Chase = new BetterChases.Chase();
	}

	public class VehicleDescription
	{
		public string plate;

		public VehicleColor primaryColor;

		public VehicleColor secondaryColor;
	}

	public class PedDescription
	{
		public int beardStyle;

		public int beardColor;

		public int hairStyle;

		public int hairColor;

		public int shirtStyle;

		public int shirtColor;

		public int pantStyle;

		public int pantColor;

		public int handStyle;

		public int handColor;

		public int footStyle;

		public int footColor;

		public int accessoryStyle;

		public int accessoryColor;

		public int accessory2Style;

		public int accessory2Color;

		public int hatExtraStyle;

		public int hatExtraColor;

		public int faceExtraStyle;

		public int faceExtraColor;

		public int earExtraStyle;

		public int earExtraColor;
	}

	public class VehicleColors
	{
		public VehicleColor primary;

		public VehicleColor secondary;
	}

	public static bool CopSearch;

	public static bool ShowSpottedMeter;

	public static double SpottedMeter;

	public static float VehicleMatch;

	public static float PedMatch;

	public static bool IsWarrantSearchActive;

	public static Warrant ActiveWarrant = new Warrant();

	public static Warrant PedWarrant = new Warrant();

	public static Warrant VehicleWarrant = new Warrant();

	public static List<Warrant> Warrants = new List<Warrant>();

	private bool IsWanted;

	private bool WasWanted;

	private int DescriptionCheckTime;

	private Ped ActiveCharacter = Game.Player.Character;

	private readonly int WarrantCheckFrequency = 59750;

	public ArrestWarrants()
	{
		base.Tick += OnTick;
		base.Aborted += Shutdown;
		base.Interval = 250;
		StoragePaths.MigrateLegacyFiles();
		LoadWarrants();
		Diagnostics.Info("Arrest warrant script initialized.");
	}

	private void OnTick(object sender, EventArgs e)
	{
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d8: Unknown result type (might be due to invalid IL or missing references)
		if (!Config.Options.ArrestWarrants.Enabled)
		{
			return;
		}
		int wantedLevel = Game.Player.Wanted.WantedLevel;
		Ped character = Game.Player.Character;
		IsWanted = Game.Player.Wanted.WantedLevel > 0;
		if (!IsWanted && (ActiveWarrant.pedHash != 0 || ActiveWarrant.vehicleHash != 0) && Function.Call<int>((Hash)5792747526117565206L, Array.Empty<InputArgument>()) >= 0 && Function.Call<int>((Hash)5792747526117565206L, Array.Empty<InputArgument>()) < 10000)
		{
			ClearWarrant(ActiveWarrant);
		}
		if (!IsWanted && (ActiveWarrant.pedHash != 0 || ActiveWarrant.vehicleHash != 0) && Function.Call<int>(unchecked((Hash)(-4106359238894428214L)), Array.Empty<InputArgument>()) >= 0 && Function.Call<int>(unchecked((Hash)(-4106359238894428214L)), Array.Empty<InputArgument>()) < 10000)
		{
			ClearWarrant(ActiveWarrant);
		}
		Model model;
		if (IsWanted && !WasWanted)
		{
			SpottedMeter = 0.0;
			ShowSpottedMeter = false;
			Renderer.Markers.Clear();
		}
		else if (!IsWanted && WasWanted)
		{
			if (ActiveWarrant.WantedLevel > 0 && (ActiveWarrant.pedHash != 0 || ActiveWarrant.vehicleHash != 0))
			{
				IssueWarrant(ActiveWarrant);
			}
		}
		else
		{
			if (wantedLevel > ActiveWarrant.WantedLevel && (ActiveWarrant.pedHash != 0 || ActiveWarrant.vehicleHash != 0))
			{
				ActiveWarrant.WantedLevel = wantedLevel;
			}
			PedWarrant = ((ActiveWarrant.pedHash != 0) ? ActiveWarrant : new Warrant());
			model = Helpers.GetModel(ActiveWarrant.vehicleHash);
			VehicleWarrant = (model.IsValid ? ActiveWarrant : new Warrant());
			if (Warrants.Count > 0)
			{
				foreach (Warrant warrant in Warrants)
				{
					if (warrant.pedHash != 0)
					{
						int pedHash = warrant.pedHash;
						model = ((Entity)character).Model;
						if (pedHash == model.Hash)
						{
							PedWarrant = warrant;
						}
					}
					if (warrant.vehicleHash != 0 && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
					{
						int vehicleHash = warrant.vehicleHash;
						model = ((Entity)character.CurrentVehicle).Model;
						if (vehicleHash == model.Hash)
						{
							VehicleWarrant = warrant;
						}
					}
				}
			}
			if (PedWarrant.WantedLevel > 0 && !IsWanted && PedWarrant.pedHash != 0 && GameClockCompat.Now.CompareTo(WarrantExpireTime(PedWarrant)) == 1)
			{
				ClearWarrant(PedWarrant);
			}
			else if (VehicleWarrant.WantedLevel > 0 && !IsWanted && VehicleWarrant.vehicleHash != 0 && GameClockCompat.Now.CompareTo(WarrantExpireTime(VehicleWarrant)) == 1)
			{
				ClearWarrant(VehicleWarrant);
			}
			if (Config.Options.ArrestWarrants.ShowNotifications)
			{
				if (PedWarrant.WantedLevel > 0 && !IsWanted && PedWarrant.pedHash != 0 && PedWarrant.WarrantCheckTime + WarrantCheckFrequency < Game.GameTime)
				{
					CheckWarrant(PedWarrant);
				}
				else if (VehicleWarrant.WantedLevel > 0 && !IsWanted && VehicleWarrant.vehicleHash != 0 && VehicleWarrant.WarrantCheckTime + WarrantCheckFrequency < Game.GameTime)
				{
					CheckWarrant(VehicleWarrant);
				}
				else
				{
					if (PedWarrant.WantedLevel > 0 && !IsWanted && PedWarrant.pedHash != 0)
					{
						model = ((Entity)character).Model;
						int hash = model.Hash;
						model = ((Entity)ActiveCharacter).Model;
						if (hash != model.Hash)
						{
							CheckWarrant(PedWarrant);
							goto IL_052f;
						}
					}
					if (VehicleWarrant.WantedLevel > 0 && !IsWanted && VehicleWarrant.vehicleHash != 0)
					{
						model = ((Entity)character).Model;
						int hash2 = model.Hash;
						model = ((Entity)ActiveCharacter).Model;
						if (hash2 != model.Hash)
						{
							CheckWarrant(VehicleWarrant);
						}
					}
				}
			}
		}
		goto IL_052f;
		IL_052f:
		if (IsWanted && ActiveWarrant.pedHash == 0)
		{
			CopSearch = true;
			if (Witnesses.GetMaxRecognition(Witnesses.Cops) >= 100.0)
			{
				CopSearch = false;
				IdentifyPlayer();
			}
		}
		if (IsWanted && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
		{
			if (ActiveWarrant.vehicleHash != 0)
			{
				model = ((Entity)character.CurrentVehicle).Model;
				if (model.Hash == ActiveWarrant.vehicleHash)
				{
					goto IL_0609;
				}
			}
			CopSearch = true;
			if (Witnesses.GetMaxRecognition(Witnesses.Cops, vehicle: true) >= 100.0)
			{
				CopSearch = false;
				IdentifyVehicle();
			}
		}
		goto IL_0609;
		IL_0609:
		if (!IsWanted)
		{
			if (PedWarrant.pedHash != 0)
			{
				goto IL_0668;
			}
			if (VehicleWarrant.vehicleHash != 0 && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
			{
				int vehicleHash2 = VehicleWarrant.vehicleHash;
				model = ((Entity)character.CurrentVehicle).Model;
				if (vehicleHash2 == model.Hash)
				{
					goto IL_0668;
				}
			}
		}
		IsWarrantSearchActive = false;
		SpottedMeter = 0.0;
		ShowSpottedMeter = false;
		Renderer.Markers.Clear();
		goto IL_0bf0;
		IL_0bf0:
		if (IsWanted && (Witnesses.GetMaxRecognition(Witnesses.Cops, vehicle: true) > 0.0 || Witnesses.GetMaxRecognition(Witnesses.Cops) > 0.0))
		{
			ActiveWarrant.LastKnownLocation = ((Entity)character).Position;
		}
		WasWanted = IsWanted;
		ActiveCharacter = character;
		return;
		IL_0668:
		CopSearch = true;
		ShowSpottedMeter = false;
		IsWarrantSearchActive = true;
		double num = 0.0;
		bool flag = false;
		Renderer.Markers.Clear();
		if (Game.GameTime > DescriptionCheckTime + 10000)
		{
			CompareWarrantDescription(VehicleWarrant, PedWarrant);
			DescriptionCheckTime = Game.GameTime;
		}
		foreach (Witnesses.Witness cop in Witnesses.Cops)
		{
			double num2 = 0.0;
			if (PedWarrant.pedHash != 0 && VehicleWarrant.vehicleHash != 0 && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
			{
				int vehicleHash3 = VehicleWarrant.vehicleHash;
				model = ((Entity)character.CurrentVehicle).Model;
				if (vehicleHash3 == model.Hash)
				{
					num2 = Math.Max(cop.Spotted, cop.VehicleSpotted);
					if (cop.Spotted > cop.VehicleSpotted)
					{
						flag = true;
					}
					goto IL_07e7;
				}
			}
			if (VehicleWarrant.vehicleHash != 0 && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
			{
				int vehicleHash4 = VehicleWarrant.vehicleHash;
				model = ((Entity)character.CurrentVehicle).Model;
				if (vehicleHash4 == model.Hash)
				{
					num2 = cop.VehicleSpotted;
					goto IL_07e7;
				}
			}
			if (PedWarrant.pedHash != 0)
			{
				num2 = cop.Spotted;
				flag = true;
			}
			goto IL_07e7;
			IL_07e7:
			if (cop.Recognition > 0.0 || cop.VehicleRecognition > 0.0)
			{
				ShowSpottedMeter = true;
			}
			if (!(num2 > 0.0))
			{
				continue;
			}
			if (cop.Ped.IsOnFoot && character.IsOnFoot)
			{
				if (cop.Spotted >= 30.0 && cop.distance < 30f)
				{
					cop.Ped.Task.GoTo((Entity)(object)character, default(Vector3), -1);
					cop.LastKnownLocation = ((Entity)character).Position;
				}
				else if (cop.Spotted >= 20.0 && cop.distance < 30f)
				{
					cop.Ped.Task.TurnTo((Entity)(object)character, -1);
				}
				else if (cop.Spotted >= 10.0 && cop.distance < 30f)
				{
					cop.Ped.Task.LookAt((Entity)(object)character, -1);
				}
				else
				{
					_ = cop.LastKnownLocation;
					if (true)
					{
						Function.Call(unchecked((Hash)(-2282141469963652331L)), (InputArgument[])(object)new InputArgument[7]
						{
							(Entity)((Entity)(object)cop.Ped),
							(InputArgument)cop.LastKnownLocation.X,
							(InputArgument)cop.LastKnownLocation.Y,
							(InputArgument)cop.LastKnownLocation.Z,
							(InputArgument)20f,
							(InputArgument)45f,
							(InputArgument)5f
						});
						cop.LastKnownLocation = default(Vector3);
					}
					else if (cop.Spotted < 5.0)
					{
						cop.Ped.Task.ClearAll();
					}
				}
			}
			Renderer.UIMarker uIMarker = new Renderer.UIMarker
			{
				Type = (MarkerType)20,
				Entity = (Entity)(object)cop.Ped
			};
			if (num2 > 0.0 && num2 <= 5.0)
			{
				uIMarker.Color = Color.Green;
			}
			else if (num2 > 5.0 && num2 < 20.0)
			{
				uIMarker.Color = Color.Cyan;
			}
			else if (num2 >= 20.0 && num2 < 50.0)
			{
				uIMarker.Color = Color.Yellow;
			}
			else if (num2 >= 50.0 && num2 < 70.0)
			{
				uIMarker.Color = Color.Orange;
			}
			else if (num2 >= 70.0)
			{
				uIMarker.Color = Color.Red;
			}
			if (num2 > num)
			{
				num = num2;
			}
			if (Config.Options.ArrestWarrants.ShowSpottedMeter)
			{
				Renderer.Markers.Add(uIMarker);
			}
		}
		SpottedMeter = num;
		if (SpottedMeter >= 100.0)
		{
			CopSearch = false;
			if (flag)
			{
				ResumeWarrent(PedWarrant, flag);
			}
			else
			{
				ResumeWarrent(VehicleWarrant, flag);
			}
		}
		goto IL_0bf0;
	}

	public static void IdentifyVehicle(bool showPlayerMessages = true)
	{
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		Ped character = Game.Player.Character;
		Model model;
		if (Warrants.Count > 0)
		{
			foreach (Warrant warrant in Warrants)
			{
				if (warrant.vehicleHash != 0)
				{
					int vehicleHash = warrant.vehicleHash;
					model = ((Entity)character.CurrentVehicle).Model;
					if (vehicleHash == model.Hash)
					{
						ActiveWarrant.WantedLevel = ((ActiveWarrant.WantedLevel > warrant.WantedLevel) ? ActiveWarrant.WantedLevel : warrant.WantedLevel);
						ActiveWarrant.Chase = BetterChases.MergeChases(ActiveWarrant.Chase, warrant.Chase);
						ActiveWarrant.pedHash = ((ActiveWarrant.pedHash != 0) ? ActiveWarrant.pedHash : warrant.pedHash);
						ActiveWarrant.vehicleHash = warrant.vehicleHash;
						Warrants.Remove(warrant);
						Helpers.WantedLevel = ActiveWarrant.WantedLevel;
						BetterChases.ActiveChase = ActiveWarrant.Chase;
						break;
					}
				}
			}
		}
		if (ActiveWarrant.vehicleHash != 0)
		{
			model = ((Entity)character.CurrentVehicle).Model;
			if (model.Hash == ActiveWarrant.vehicleHash)
			{
				if (Config.Options.ArrestWarrants.ShowNotifications && showPlayerMessages)
				{
					Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
					Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("Model: ~y~" + character.CurrentVehicle.LocalizedName + "~w~~n~Color: ~y~" + SimplifyColorString(((object)GetVehicleColors(character.CurrentVehicle).primary/*cast due to constrained. prefix*/).ToString()) + "~w~~n~Plate: ~y~" + Function.Call<string>((Hash)8998698628399956494L, (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)character.CurrentVehicle) })) });
					Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
					{
						(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
						(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
						(InputArgument)true,
						(InputArgument)0,
						(InputArgument)"VEHICLE IDENTIFIED",
						(InputArgument)"~c~LSPD"
					});
				}
				if (Config.Options.DisplayHints && showPlayerMessages)
				{
					Renderer.ShowHelpMessage("The ~b~LSPD~w~ has identified your vehicle as belonging to a previous suspect they were looking for.");
					Renderer.ShowHelpMessage("Previous arrest warrant has been combined with the current one.");
				}
				goto IL_04c2;
			}
		}
		if (Config.Options.ArrestWarrants.ShowNotifications && showPlayerMessages)
		{
			Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
			Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("Model: ~y~" + character.CurrentVehicle.LocalizedName + "~w~~n~Color: ~y~" + SimplifyColorString(((object)GetVehicleColors(character.CurrentVehicle).primary/*cast due to constrained. prefix*/).ToString()) + "~w~~n~Plate: ~y~" + Function.Call<string>((Hash)8998698628399956494L, (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)character.CurrentVehicle) })) });
			if (ActiveWarrant.vehicleHash != 0)
			{
				Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
				{
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)true,
					(InputArgument)0,
					(InputArgument)"NEW VEHICLE IDENTIFIED",
					(InputArgument)"~c~LSPD"
				});
			}
			else
			{
				Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
				{
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)true,
					(InputArgument)0,
					(InputArgument)"VEHICLE IDENTIFIED",
					(InputArgument)"~c~LSPD"
				});
			}
		}
		if (Config.Options.DisplayHints && showPlayerMessages)
		{
			Renderer.ShowHelpMessage("The ~b~LSPD~w~ has identified your vehicle. Cops will recognise it if you escape.");
			Renderer.ShowHelpMessage("You can avoid this by acquiring a new vehicle while out of the cops' sight.");
		}
		Warrant activeWarrant = ActiveWarrant;
		model = ((Entity)character.CurrentVehicle).Model;
		activeWarrant.vehicleHash = model.Hash;
		goto IL_04c2;
		IL_04c2:
		if (Config.Options.ArrestWarrants.ShowBigMessages && showPlayerMessages)
		{
			Renderer.ShowBigMessage("VEHICLE IDENTIFIED", "", Renderer.HudColor.GOLD, Renderer.HudColor.BLACK);
		}
		ActiveWarrant.VehicleDescription.plate = Function.Call<string>((Hash)8998698628399956494L, (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)character.CurrentVehicle) });
		ActiveWarrant.VehicleDescription.primaryColor = GetVehicleColors(character.CurrentVehicle).primary;
		ActiveWarrant.VehicleDescription.secondaryColor = GetVehicleColors(character.CurrentVehicle).secondary;
		VehicleWarrant = ActiveWarrant;
	}

	public static void IdentifyPlayer(bool showPlayerMessages = true)
	{
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Ped character = Game.Player.Character;
		Model model;
		if (Warrants.Count > 0)
		{
			foreach (Warrant warrant in Warrants)
			{
				if (warrant.pedHash != 0)
				{
					int pedHash = warrant.pedHash;
					model = ((Entity)character).Model;
					if (pedHash == model.Hash)
					{
						ActiveWarrant.WantedLevel = ((ActiveWarrant.WantedLevel > warrant.WantedLevel) ? ActiveWarrant.WantedLevel : warrant.WantedLevel);
						ActiveWarrant.Chase = BetterChases.MergeChases(ActiveWarrant.Chase, warrant.Chase);
						ActiveWarrant.vehicleHash = ((ActiveWarrant.vehicleHash != 0) ? ActiveWarrant.vehicleHash : warrant.vehicleHash);
						ActiveWarrant.pedHash = warrant.pedHash;
						Warrants.Remove(warrant);
						Helpers.WantedLevel = ActiveWarrant.WantedLevel;
						BetterChases.ActiveChase = ActiveWarrant.Chase;
						break;
					}
				}
			}
		}
		if (ActiveWarrant.pedHash != 0)
		{
			if (Config.Options.ArrestWarrants.ShowNotifications && showPlayerMessages)
			{
				Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("Fleeing suspect has been identified as a previous suspect ~y~" + ((object)(PedHash)ActiveWarrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~.") });
				Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
				{
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)true,
					(InputArgument)0,
					(InputArgument)"SUSPECT IDENTIFIED",
					(InputArgument)"~c~LSPD"
				});
			}
			if (Config.Options.DisplayHints && showPlayerMessages)
			{
				Renderer.ShowHelpMessage(((object)(PedHash)ActiveWarrant.pedHash/*cast due to constrained. prefix*/).ToString() + " has been identified by the ~b~LSPD~w~ as a previous suspect they were looking for.");
				Renderer.ShowHelpMessage("Previous arrest warrant has been combined with the current one.");
			}
		}
		else
		{
			Warrant activeWarrant = ActiveWarrant;
			model = ((Entity)character).Model;
			activeWarrant.pedHash = model.Hash;
			if (Config.Options.ArrestWarrants.ShowNotifications && showPlayerMessages)
			{
				Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("Fleeing suspect has been identified as ~y~" + ((object)(PedHash)ActiveWarrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~.") });
				Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
				{
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)true,
					(InputArgument)0,
					(InputArgument)"SUSPECT IDENTIFIED",
					(InputArgument)"~c~LSPD"
				});
			}
			if (Config.Options.DisplayHints && showPlayerMessages)
			{
				Renderer.ShowHelpMessage(((object)(PedHash)ActiveWarrant.pedHash/*cast due to constrained. prefix*/).ToString() + " has been identified by the ~b~LSPD~w~. Cops will be able to recognise you if you escape.");
				Renderer.ShowHelpMessage("Change clothes while out of sight to avoid being recognised after escaping.");
			}
		}
		if (Config.Options.ArrestWarrants.ShowBigMessages && showPlayerMessages)
		{
			Renderer.ShowBigMessage("IDENTIFIED", "", Renderer.HudColor.GOLD, Renderer.HudColor.BLACK);
		}
		ActiveWarrant.PedDescription.beardStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.beardColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.hairStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		ActiveWarrant.PedDescription.hairColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		ActiveWarrant.PedDescription.shirtStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)3
		});
		ActiveWarrant.PedDescription.shirtColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)3
		});
		ActiveWarrant.PedDescription.pantStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)4
		});
		ActiveWarrant.PedDescription.pantColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)4
		});
		ActiveWarrant.PedDescription.handStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)5
		});
		ActiveWarrant.PedDescription.handColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)5
		});
		ActiveWarrant.PedDescription.footStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)6
		});
		ActiveWarrant.PedDescription.footColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)6
		});
		ActiveWarrant.PedDescription.accessoryStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)8
		});
		ActiveWarrant.PedDescription.accessoryColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)8
		});
		ActiveWarrant.PedDescription.accessory2Style = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)9
		});
		ActiveWarrant.PedDescription.accessory2Color = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)9
		});
		ActiveWarrant.PedDescription.hatExtraStyle = Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)0
		});
		ActiveWarrant.PedDescription.hatExtraColor = Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)0
		});
		ActiveWarrant.PedDescription.faceExtraStyle = Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.faceExtraColor = Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.earExtraStyle = Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		ActiveWarrant.PedDescription.earExtraColor = Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		PedWarrant = ActiveWarrant;
	}

	public static void ClearWarrant(Warrant warrant)
	{
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		ActiveWarrant = new Warrant();
		Warrants.Remove(warrant);
		if (Config.Options.ArrestWarrants.ShowNotifications)
		{
			Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
			if (warrant.pedHash != 0)
			{
				if (warrant.vehicleHash != 0)
				{
					Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("The Arrest Warrant on ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~ and on the ~y~" + Function.Call<string>(unchecked((Hash)(-5614393605194657767L)), (InputArgument[])(object)new InputArgument[1] { (Model)Helpers.GetModel(warrant.vehicleHash) }) + "~w~ has been lifted.") });
				}
				else
				{
					Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("The Arrest Warrant on ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~ has been lifted.") });
				}
			}
			else if (warrant.vehicleHash != 0)
			{
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("The Arrest Warrant on the ~y~" + Function.Call<string>(unchecked((Hash)(-5614393605194657767L)), (InputArgument[])(object)new InputArgument[1] { (Model)Helpers.GetModel(warrant.vehicleHash) }) + "~w~ has been lifted.") });
			}
			Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
			{
				(InputArgument)"web_lossantospolicedept",
				(InputArgument)"web_lossantospolicedept",
				(InputArgument)true,
				(InputArgument)0,
				(InputArgument)"ARREST WARRANT LIFTED",
				(InputArgument)"~c~LSPD"
			});
			if (Config.Options.DisplayHints)
			{
				if (warrant.pedHash != 0)
				{
					Renderer.ShowHelpMessage(((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "'s Arrest Warrant has expired. You are now free to roam around without worries.");
					Renderer.ShowHelpMessage("~g~The police have forgotten about you~w~.");
				}
				else
				{
					Renderer.ShowHelpMessage("The " + Function.Call<string>(unchecked((Hash)(-5614393605194657767L)), (InputArgument[])(object)new InputArgument[1] { (Model)Helpers.GetModel(warrant.vehicleHash) }) + "'s Arrest Warrant has expired. You are now free to roam around without worries.");
					Renderer.ShowHelpMessage("~g~The police have forgotten about your vehicle~w~.");
				}
			}
		}
		if (Config.Options.ArrestWarrants.ShowBigMessages)
		{
			Renderer.ShowBigMessage("WARRANT LIFTED", "", Renderer.HudColor.BLUE, Renderer.HudColor.BLACK);
		}
		SaveWarrants();
	}

	public static void ClearWarrants()
	{
		ActiveWarrant = new Warrant();
		Warrants.Clear();
	}

	public static void IssuePlayerWarrant()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Ped character = Game.Player.Character;
		Warrant warrant = new Warrant();
		warrant.WantedLevel = 1;
		Model model = ((Entity)character).Model;
		warrant.pedHash = model.Hash;
		ActiveWarrant.PedDescription.beardStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.beardColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.hairStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		ActiveWarrant.PedDescription.hairColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		ActiveWarrant.PedDescription.shirtStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)3
		});
		ActiveWarrant.PedDescription.shirtColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)3
		});
		ActiveWarrant.PedDescription.pantStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)4
		});
		ActiveWarrant.PedDescription.pantColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)4
		});
		ActiveWarrant.PedDescription.handStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)5
		});
		ActiveWarrant.PedDescription.handColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)5
		});
		ActiveWarrant.PedDescription.footStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)6
		});
		ActiveWarrant.PedDescription.footColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)6
		});
		ActiveWarrant.PedDescription.accessoryStyle = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)8
		});
		ActiveWarrant.PedDescription.accessoryColor = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)8
		});
		ActiveWarrant.PedDescription.accessory2Style = Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)9
		});
		ActiveWarrant.PedDescription.accessory2Color = Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)9
		});
		ActiveWarrant.PedDescription.hatExtraStyle = Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)0
		});
		ActiveWarrant.PedDescription.hatExtraColor = Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)0
		});
		ActiveWarrant.PedDescription.faceExtraStyle = Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.faceExtraColor = Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)1
		});
		ActiveWarrant.PedDescription.earExtraStyle = Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		ActiveWarrant.PedDescription.earExtraColor = Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)character),
			(InputArgument)2
		});
		IssueWarrant(warrant);
	}

	public static Warrant IssueWarrant(Warrant warrant)
	{
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		if (warrant.WantedLevel <= 0 || (warrant.pedHash == 0 && warrant.vehicleHash == 0))
		{
			return warrant;
		}
		warrant.WarrantIssueTime = GameClockCompat.Now;
		warrant.WarrantCheckTime = Game.GameTime;
		if (Config.Options.ArrestWarrants.ShowNotifications)
		{
			Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
			if (warrant.pedHash != 0)
			{
				if (warrant.vehicleHash != 0)
				{
					Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("Name: ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~~n~Model: ~y~" + Function.Call<string>(unchecked((Hash)(-5614393605194657767L)), (InputArgument[])(object)new InputArgument[1] { (Model)Helpers.GetModel(warrant.vehicleHash) }) + "~w~~n~Expiration: ~y~" + WarrantExpireFormatted(warrant) + "~w~") });
				}
				else
				{
					Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("Name: ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~~n~Last Seen: ~y~" + World.GetStreetName(warrant.LastKnownLocation) + "~w~~n~Expiration: ~y~" + WarrantExpireFormatted(warrant) + "~w~") });
				}
			}
			else if (warrant.vehicleHash != 0)
			{
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("Model: ~y~" + Function.Call<string>(unchecked((Hash)(-5614393605194657767L)), (InputArgument[])(object)new InputArgument[1] { (Model)Helpers.GetModel(warrant.vehicleHash) }) + "~w~~n~Last Seen: ~y~" + World.GetStreetName(warrant.LastKnownLocation) + "~w~~n~Expiration: ~y~" + WarrantExpireFormatted(warrant) + "~w~") });
			}
			Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
			{
				(InputArgument)"web_lossantospolicedept",
				(InputArgument)"web_lossantospolicedept",
				(InputArgument)true,
				(InputArgument)0,
				(InputArgument)"ARREST WARRANT ISSUED",
				(InputArgument)"~c~LSPD"
			});
		}
		if (Config.Options.DisplayHints)
		{
			Renderer.ShowHelpMessage("You have escaped, but you're not safe yet.");
			Model model;
			if (warrant.pedHash != 0)
			{
				Renderer.ShowHelpMessage("The Arrest Warrant has ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~'s appareance description.");
				Renderer.ShowHelpMessage("You can change clothes to prevent the cops from being able to recognise you.");
				if (warrant.vehicleHash != 0)
				{
					Renderer.ShowHelpMessage("The Arrest Warrant has your vehicle's description too.");
					if (Helpers.IsValid((Entity)(object)Game.Player.Character.CurrentVehicle))
					{
						model = ((Entity)Game.Player.Character.CurrentVehicle).Model;
						if (model.Hash == warrant.vehicleHash)
						{
							Renderer.ShowHelpMessage("It is best that you get rid of the vehicle, or modify it.");
							goto IL_0390;
						}
					}
					Renderer.ShowHelpMessage("However, you are in a different vehicle, so you don't have to worry about the vehicle warrant.");
				}
				goto IL_0390;
			}
			if (warrant.vehicleHash != 0)
			{
				string[] obj = new string[5] { "~y~", null, null, null, null };
				model = ((Entity)Game.Player.Character).Model;
				obj[1] = ((object)(PedHash)model.Hash/*cast due to constrained. prefix*/).ToString();
				obj[2] = "~w~ hasn't been identfied, but ~y~";
				model = ((Entity)Game.Player.Character).Model;
				obj[3] = ((object)(PedHash)model.Hash/*cast due to constrained. prefix*/).ToString();
				obj[4] = "~w~'s vehicle has.";
				Renderer.ShowHelpMessage(string.Concat(obj));
				if (Helpers.IsValid((Entity)(object)Game.Player.Character.CurrentVehicle))
				{
					model = ((Entity)Game.Player.Character.CurrentVehicle).Model;
					if (model.Hash == warrant.vehicleHash)
					{
						Renderer.ShowHelpMessage("It is best that you get rid of the vehicle, or modify it.");
						goto IL_04b1;
					}
				}
				Renderer.ShowHelpMessage("However, you are in a different vehicle, so you don't have to worry about the vehicle warrant.");
			}
			goto IL_04b1;
		}
		goto IL_04c8;
		IL_04b1:
		Renderer.ShowHelpMessage("Cop's will be on full alert for a while, but they will eventually lose interest.");
		Renderer.ShowHelpMessage("The closer the Arrest Warrant is to expire, the less likely cops will recognise you.");
		goto IL_04c8;
		IL_0390:
		Renderer.ShowHelpMessage("You can also switch to another Character and wait for ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~'s Arrest Warrant to expire.");
		goto IL_04b1;
		IL_04c8:
		if (Config.Options.ArrestWarrants.ShowBigMessages)
		{
			Renderer.ShowBigMessage("ESCAPED", "", Renderer.HudColor.BLUE, Renderer.HudColor.BLACK);
		}
		Warrants.Add(warrant);
		ActiveWarrant = new Warrant();
		SaveWarrants();
		return warrant;
	}

	private void CheckWarrant(Warrant warrant)
	{
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		warrant.WarrantCheckTime = Game.GameTime;
		Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
		if (warrant.pedHash != 0)
		{
			if (warrant.vehicleHash != 0)
			{
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("The Arrest Warrant for ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~ and the ~y~" + Function.Call<string>(unchecked((Hash)(-5614393605194657767L)), (InputArgument[])(object)new InputArgument[1] { (Model)Helpers.GetModel(warrant.vehicleHash) }) + "~w~ will expire in ~y~" + WarrantExpireFormatted(warrant)) });
			}
			else
			{
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("The Arrest Warrant for ~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~ will expire in ~y~" + WarrantExpireFormatted(warrant)) });
			}
		}
		else if (warrant.vehicleHash != 0)
		{
			Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("The Arrest Warrant for the ~y~" + Function.Call<string>(unchecked((Hash)(-5614393605194657767L)), (InputArgument[])(object)new InputArgument[1] { (Model)Helpers.GetModel(warrant.vehicleHash) }) + "~w~ will expire in ~y~" + WarrantExpireFormatted(warrant)) });
		}
		Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
		{
			(InputArgument)"web_lossantospolicedept",
			(InputArgument)"web_lossantospolicedept",
			(InputArgument)true,
			(InputArgument)0,
			(InputArgument)"WARRANT EXPIRATION",
			(InputArgument)"~c~LSPD"
		});
	}

	private void ResumeWarrent(Warrant warrant, bool WasPedSpotted)
	{
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		ActiveWarrant = warrant;
		if (Config.Options.ArrestWarrants.RememberChase && warrant.WantedLevel > Game.Player.Wanted.WantedLevel)
		{
			Helpers.WantedLevel = warrant.WantedLevel;
		}
		else
		{
			Helpers.WantedLevel = 2;
		}
		BetterChases.ActiveChase = BetterChases.MergeChases(BetterChases.ActiveChase, warrant.Chase);
		if (WasPedSpotted)
		{
			if (Config.Options.ArrestWarrants.ShowNotifications)
			{
				Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)("~y~" + ((object)(PedHash)warrant.pedHash/*cast due to constrained. prefix*/).ToString() + "~w~ has been spotted on ~y~" + World.GetStreetName(((Entity)Game.Player.Character).Position) + "~w.~") });
				Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
				{
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)true,
					(InputArgument)0,
					(InputArgument)"WANTED PERSON SPOTTED",
					(InputArgument)"~c~LSPD"
				});
			}
			if (Config.Options.DisplayHints)
			{
				Renderer.ShowHelpMessage("You have been ~r~recognised~w~ by a cop. The chase will resume.");
			}
			if (Config.Options.ArrestWarrants.ShowBigMessages)
			{
				Renderer.ShowBigMessage("SPOTTED", "", Renderer.HudColor.RED, Renderer.HudColor.BLACK);
			}
		}
		else
		{
			if (Config.Options.ArrestWarrants.ShowNotifications)
			{
				Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"A wanted vehicle has been spotted." });
				Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
				{
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)true,
					(InputArgument)0,
					(InputArgument)"WANTED VEHICLE SPOTTED",
					(InputArgument)"~c~LSPD"
				});
			}
			if (Config.Options.DisplayHints)
			{
				Renderer.ShowHelpMessage("Your vehicle has been ~r~recognised~w~ by a cop. The chase will resume and the Arrest Warrant has been updated.");
			}
			if (Config.Options.ArrestWarrants.ShowBigMessages)
			{
				Renderer.ShowBigMessage("VEHICLE SPOTTED", "", Renderer.HudColor.RED, Renderer.HudColor.BLACK);
			}
		}
	}

	private static DateTime WarrantExpireTime(Warrant warrant)
	{
		int num = 0;
		switch (warrant.WantedLevel)
		{
		case 1:
			num = Config.Options.ArrestWarrants.WarrantLenghts.OneStar;
			break;
		case 2:
			num = Config.Options.ArrestWarrants.WarrantLenghts.OneStar + Config.Options.ArrestWarrants.WarrantLenghts.TwoStar;
			break;
		case 3:
			num = Config.Options.ArrestWarrants.WarrantLenghts.OneStar + Config.Options.ArrestWarrants.WarrantLenghts.TwoStar + Config.Options.ArrestWarrants.WarrantLenghts.ThreeStar;
			break;
		case 4:
			num = Config.Options.ArrestWarrants.WarrantLenghts.OneStar + Config.Options.ArrestWarrants.WarrantLenghts.TwoStar + Config.Options.ArrestWarrants.WarrantLenghts.ThreeStar + Config.Options.ArrestWarrants.WarrantLenghts.FourStar;
			break;
		case 5:
			num = Config.Options.ArrestWarrants.WarrantLenghts.OneStar + Config.Options.ArrestWarrants.WarrantLenghts.TwoStar + Config.Options.ArrestWarrants.WarrantLenghts.ThreeStar + Config.Options.ArrestWarrants.WarrantLenghts.FourStar + Config.Options.ArrestWarrants.WarrantLenghts.FiveStar;
			break;
		}
		return warrant.WarrantIssueTime.AddHours(num);
	}

	private static string WarrantExpireFormatted(Warrant warrant)
	{
		string text = "";
		TimeSpan timeSpan = WarrantExpireTime(warrant).Subtract(GameClockCompat.Now);
		if (timeSpan.Days > 0)
		{
			text = ((timeSpan.Days <= 1) ? (text + timeSpan.Days + " day ") : (text + timeSpan.Days + " days "));
		}
		if (timeSpan.Hours > 0)
		{
			text = ((timeSpan.Hours <= 1) ? (text + timeSpan.Hours + " hour ") : (text + timeSpan.Hours + " hours "));
		}
		if (timeSpan.Minutes > 0)
		{
			text = ((timeSpan.Minutes <= 1) ? (text + timeSpan.Minutes + " minute") : (text + timeSpan.Minutes + " minutes"));
		}
		return text;
	}

	private void CompareWarrantDescription(Warrant vehicleWarrant, Warrant pedWarrant)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
		if (vehicleWarrant.vehicleHash != 0 && Helpers.IsValid((Entity)(object)currentVehicle))
		{
			Model model = ((Entity)currentVehicle).Model;
			if (model.Hash == vehicleWarrant.vehicleHash)
			{
				float num = 0f;
				if (Function.Call<string>((Hash)8998698628399956494L, (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)currentVehicle) }) == vehicleWarrant.VehicleDescription.plate)
				{
					num += 0.25f;
				}
				VehicleMatch = num;
				goto IL_0097;
			}
		}
		VehicleMatch = 0f;
		goto IL_0097;
		IL_0097:
		if (pedWarrant.pedHash != 0)
		{
			float num2 = 0f;
			Ped character = Game.Player.Character;
			if (Helpers.IsMasked(character) || Helpers.IsWearingHelmet(character))
			{
				if (pedWarrant.PedDescription.hatExtraStyle == Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)0
				}))
				{
					num2 += 0.2f;
				}
				if (pedWarrant.PedDescription.hatExtraColor == Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)0
				}))
				{
					num2 += 0.15f;
				}
			}
			else
			{
				if (pedWarrant.PedDescription.beardStyle == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)1
				}))
				{
					num2 += 0.05f;
				}
				if (pedWarrant.PedDescription.beardColor == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)1
				}))
				{
					num2 += 0.05f;
				}
				if (pedWarrant.PedDescription.hairStyle == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)2
				}))
				{
					num2 += 0.05f;
				}
				if (pedWarrant.PedDescription.hairColor == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)2
				}))
				{
					num2 += 0.05f;
				}
				if (pedWarrant.PedDescription.hatExtraStyle == Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)0
				}))
				{
					num2 += 0.05f;
				}
				if (pedWarrant.PedDescription.hatExtraColor == Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)0
				}))
				{
					num2 += 0.05f;
				}
				if (pedWarrant.PedDescription.faceExtraStyle == Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)1
				}))
				{
					num2 += 0.0125f;
				}
				if (pedWarrant.PedDescription.faceExtraColor == Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)1
				}))
				{
					num2 += 0.0125f;
				}
				if (pedWarrant.PedDescription.earExtraStyle == Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)2
				}))
				{
					num2 += 0.0125f;
				}
				if (pedWarrant.PedDescription.earExtraColor == Function.Call<int>(unchecked((Hash)(-2219814444253832526L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character),
					(InputArgument)2
				}))
				{
					num2 += 0.0125f;
				}
			}
			if (pedWarrant.PedDescription.shirtStyle == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)3
			}))
			{
				num2 += 0.1f;
			}
			if (pedWarrant.PedDescription.shirtColor == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)3
			}))
			{
				num2 += 0.1f;
			}
			if (pedWarrant.PedDescription.pantStyle == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)4
			}))
			{
				num2 += 0.1f;
			}
			if (pedWarrant.PedDescription.pantColor == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)4
			}))
			{
				num2 += 0.1f;
			}
			if (pedWarrant.PedDescription.handStyle == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)5
			}))
			{
				num2 += 0.0125f;
			}
			if (pedWarrant.PedDescription.handColor == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)5
			}))
			{
				num2 += 0.0125f;
			}
			if (pedWarrant.PedDescription.footStyle == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)6
			}))
			{
				num2 += 0.0125f;
			}
			if (pedWarrant.PedDescription.footColor == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)6
			}))
			{
				num2 += 0.0125f;
			}
			if (pedWarrant.PedDescription.accessoryStyle == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)8
			}))
			{
				num2 += 0.05f;
			}
			if (pedWarrant.PedDescription.accessoryColor == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)8
			}))
			{
				num2 += 0.05f;
			}
			if (pedWarrant.PedDescription.accessory2Style == Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)9
			}))
			{
				num2 += 0.05f;
			}
			if (pedWarrant.PedDescription.accessory2Color == Function.Call<int>((Hash)334205219021784294L, (InputArgument[])(object)new InputArgument[2]
			{
				(Entity)((Entity)(object)character),
				(InputArgument)9
			}))
			{
				num2 += 0.05f;
			}
			PedMatch = num2;
		}
		else
		{
			PedMatch = 0f;
		}
	}

	public static string SimplifyColorString(string color)
	{
		string[] array = new string[30]
		{
			"Straw", "Anthracite", "Graphite", "Pueblo", "Golden", "Metallic", "Matte", "Pure", "PoliceCar", "Util",
			"Worn", "Dark", "Light", "Taxi", "Desert", "Foliage", "Hot", "Hunter", "Midnight", "Marine",
			"Formula", "Frost", "Garnet", "Epsilon", "Moss", "Olive", "Util", "Ultra", "Salmon", "Gasoline"
		};
		string[] array2 = array;
		foreach (string oldValue in array2)
		{
			color = color.Replace(oldValue, string.Empty);
		}
		return color;
	}

	public static VehicleColors GetVehicleColors(Vehicle vehicle)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		VehicleColors vehicleColors = new VehicleColors();
		OutputArgument val = new OutputArgument();
		OutputArgument val2 = new OutputArgument();
		Function.Call(unchecked((Hash)(-6803753825365622356L)), (InputArgument[])(object)new InputArgument[3]
		{
			(Entity)((Entity)(object)vehicle),
			(InputArgument)val,
			(InputArgument)val2
		});
		vehicleColors.primary = val.GetResult<VehicleColor>();
		vehicleColors.secondary = val2.GetResult<VehicleColor>();
		return vehicleColors;
	}

	public static void SaveWarrants()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		try
		{
			XmlPersistence.Save(StoragePaths.WarrantsFile, Warrants, "Warrants");
		}
		catch (Exception exception)
		{
			Diagnostics.Error("Saving warrants", exception);
		}
	}

	public static void LoadWarrants()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		try
		{
			if (XmlPersistence.TryLoad(StoragePaths.WarrantsFile, "Warrants", out List<Warrant> loaded))
			{
				Warrants = loaded;
			}
		}
		catch (Exception exception)
		{
			Diagnostics.Error("Loading warrants", exception);
		}
	}

	private void Shutdown(object sender, EventArgs e)
	{
		if (Config.Options.ArrestWarrants.RememberChase)
		{
			SaveWarrants();
		}
		Diagnostics.Info("Arrest warrant script stopped.");
	}
}
