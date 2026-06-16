using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using GTA;
using GTA.Math;
using GTA.Native;

namespace BetterChasesPlus;

public class BetterChases
{
	public class Chase
	{
		private TimeSpan _Duration = default(TimeSpan);

		public bool DeadlyForce = false;

		public bool PITAuthorized = false;

		public DateTime StartTime = default(DateTime);

		public Crimes Crimes = new Crimes();

		[XmlIgnore]
		public TimeSpan Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				_Duration = value;
			}
		}

		[XmlElement("Duration")]
		public long DuractionTicks
		{
			get
			{
				return _Duration.Ticks;
			}
			set
			{
				_Duration = new TimeSpan(value);
			}
		}
	}

	public class Crimes
	{
		public bool Fleeing1 = false;

		public bool Fleeing2 = false;

		public bool Fleeing3 = false;

		public bool Fleeing4 = false;

		public bool GrandTheftAuto = false;

		public bool Stolen = false;

		public bool Speeding = false;

		public bool Reckless = false;

		public bool Armed = false;

		public bool Aiming = false;

		public bool Assault = false;

		public bool PoliceAssault = false;

		public bool Shooting = false;

		public bool Murder = false;

		public bool PoliceMurder = false;
	}

	public class BetterChasesPassive : Script
	{
		private static bool IsWanted;

		private static bool WasWanted;

		private static int BustStars;

		private static bool AreHandsUp;

		private static bool IsGettingBusted;

		private static int AdditionalGroundUnits;

		private static int AdditionalAirUnits;

		private static DateTime ChaseTime = default(DateTime);

		private static List<Ped> PedsKilled = new List<Ped>();

		private static List<Ped> CopsKilled = new List<Ped>();

		private static List<Ped> ModifiedCops = new List<Ped>();

		private static List<Ped> CopsMovingToCar = new List<Ped>();

		private static List<Ped> CopsAimingAtCar = new List<Ped>();

		public BetterChasesPassive()
		{
			base.Tick += OnTick;
			base.Interval = 250;
		}

		private void OnTick(object sender, EventArgs e)
		{
			//IL_0577: Unknown result type (might be due to invalid IL or missing references)
			//IL_057c: Unknown result type (might be due to invalid IL or missing references)
			//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03fa: Invalid comparison between Unknown and I4
			//IL_0589: Unknown result type (might be due to invalid IL or missing references)
			//IL_058e: Unknown result type (might be due to invalid IL or missing references)
			//IL_059b: Unknown result type (might be due to invalid IL or missing references)
			//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b0: Invalid comparison between Unknown and I4
			//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_082d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0832: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_083f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0844: Unknown result type (might be due to invalid IL or missing references)
			//IL_0851: Unknown result type (might be due to invalid IL or missing references)
			//IL_0856: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b39: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d13: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d18: Unknown result type (might be due to invalid IL or missing references)
			//IL_3873: Unknown result type (might be due to invalid IL or missing references)
			//IL_3636: Unknown result type (might be due to invalid IL or missing references)
			//IL_37dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_372f: Unknown result type (might be due to invalid IL or missing references)
			//IL_366d: Unknown result type (might be due to invalid IL or missing references)
			//IL_380a: Unknown result type (might be due to invalid IL or missing references)
			//IL_375c: Unknown result type (might be due to invalid IL or missing references)
			//IL_36a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_3837: Unknown result type (might be due to invalid IL or missing references)
			//IL_3789: Unknown result type (might be due to invalid IL or missing references)
			if (!Config.Options.BetterChases.Enabled)
			{
				return;
			}
			Ped character = Game.Player.Character;
			IsWanted = ((Game.Player.Wanted.WantedLevel > 0 || Function.Call<bool>((Hash)751735369465373403L, (InputArgument[])(object)new InputArgument[1] { (Player)Game.Player })) ? true : false);
			if (WasWanted && !IsWanted)
			{
				ActiveChase = new Chase();
				AdditionalGroundUnits = 0;
				AdditionalAirUnits = 0;
				PedsKilled = new List<Ped>();
				CopsKilled = new List<Ped>();
			}
			else if (IsWanted && !WasWanted)
			{
				ChaseTime = GameClockCompat.Now;
				if (ActiveChase.StartTime.CompareTo(default(DateTime)) == 0)
				{
					ActiveChase.StartTime = GameClockCompat.Now;
				}
			}
			if (IsWanted)
			{
				CopSearch = true;
			}
			else
			{
				CopSearch = false;
			}
			string text = "";
			string text2 = "";
			int wantedLevel = Game.Player.Wanted.WantedLevel;
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			Peds.Clear();
			PedVehicles.Clear();
			Cops.Clear();
			CopVehicles.Clear();
			Ped[] allPeds = World.GetAllPeds(Array.Empty<Model>());
			foreach (Ped val in allPeds)
			{
				if (!val.IsHuman)
				{
					continue;
				}
				if (Enumerable.Contains(Helpers.PedCopTypes, Function.Call<int>(unchecked((Hash)(-70476366192974276L)), (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)val) })))
				{
					Cops.Add(val);
					if (Helpers.IsValid((Entity)(object)val.CurrentVehicle) && Helpers.IsValid((Entity)(object)val.CurrentVehicle.Driver) && val.CurrentVehicle.IsDriveable && ((PoolObject)val.CurrentVehicle.Driver).Handle == ((PoolObject)val).Handle)
					{
						CopVehicles.Add(val.CurrentVehicle);
					}
					else if (Helpers.IsValid((Entity)(object)val.LastVehicle) && val.LastVehicle.IsDriveable && !CopVehicles.Contains(val.LastVehicle))
					{
						CopVehicles.Add(val.LastVehicle);
					}
				}
				else
				{
					Peds.Add(val);
					if (Helpers.IsValid((Entity)(object)val.CurrentVehicle) && Helpers.IsValid((Entity)(object)val.CurrentVehicle.Driver) && ((PoolObject)val.CurrentVehicle.Driver).Handle == ((PoolObject)val).Handle)
					{
						PedVehicles.Add(val.CurrentVehicle);
					}
				}
			}
			if (Config.Options.BetterChases.DisallowCopCommandeering && IsWanted)
			{
				foreach (Ped cop in Cops)
				{
					Function.Call(unchecked((Hash)(-6955927877681029095L)), (InputArgument[])(object)new InputArgument[3]
					{
						(Entity)((Entity)(object)cop),
						(InputArgument)41,
						(InputArgument)false
					});
				}
			}
			if (Config.Options.BetterChases.RequireLethalForceAuthorization)
			{
				if (IsWanted && !ActiveChase.DeadlyForce)
				{
					foreach (Ped cop2 in Cops)
					{
						if ((int)cop2.Weapons.Current.Hash != 911657153 && cop2.IsInCombatAgainst(character))
						{
							cop2.Weapons.Give((WeaponHash)911657153, 10, true, true);
							cop2.CanSwitchWeapons = false;
							ModifiedCops.Add(cop2);
						}
					}
				}
				else if (ModifiedCops.Count > 0)
				{
					foreach (Ped item in ModifiedCops.ToList())
					{
						if (Helpers.IsValid((Entity)(object)item) && (int)item.Weapons.Current.Hash == 911657153)
						{
							item.CanSwitchWeapons = true;
							item.Weapons.Select(item.Weapons.BestWeapon.Hash, true);
							ModifiedCops.Remove(item);
						}
						else
						{
							ModifiedCops.Remove(item);
						}
					}
				}
			}
			Model model;
			if (Config.Options.BetterChases.CopDispatch.Enabled && IsWanted)
			{
				List<Vehicle> list = new List<Vehicle>();
				List<Vehicle> list2 = new List<Vehicle>();
				foreach (Vehicle copVehicle in CopVehicles)
				{
					model = ((Entity)copVehicle).Model;
					if (!model.IsCar)
					{
						model = ((Entity)copVehicle).Model;
						if (!model.IsBike)
						{
							model = ((Entity)copVehicle).Model;
							if (!model.IsQuadBike)
							{
								model = ((Entity)copVehicle).Model;
								if (!model.IsPlane)
								{
									model = ((Entity)copVehicle).Model;
									if (!model.IsHelicopter)
									{
										continue;
									}
								}
								list2.Add(copVehicle);
								continue;
							}
						}
					}
					list.Add(copVehicle);
				}
				if (list.Count < Helpers.MinGroundUnits + AdditionalGroundUnits)
				{
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)1,
						(InputArgument)true
					});
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)4,
						(InputArgument)true
					});
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)6,
						(InputArgument)true
					});
				}
				else
				{
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)1,
						(InputArgument)false
					});
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)4,
						(InputArgument)false
					});
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)6,
						(InputArgument)false
					});
				}
				if (list2.Count < Helpers.MinAirUnits + AdditionalAirUnits)
				{
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)2,
						(InputArgument)true
					});
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)12,
						(InputArgument)true
					});
				}
				else
				{
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)2,
						(InputArgument)false
					});
					Function.Call(unchecked((Hash)(-2589708906090604458L)), (InputArgument[])(object)new InputArgument[2]
					{
						(InputArgument)12,
						(InputArgument)false
					});
				}
			}
			if (Config.Options.BetterChases.WreckedCopsStopChasing && IsWanted)
			{
				foreach (Vehicle copVehicle2 in CopVehicles)
				{
					if (!(copVehicle2.EngineHealth > 0f))
					{
						continue;
					}
					model = ((Entity)copVehicle2).Model;
					if (!model.IsCar)
					{
						model = ((Entity)copVehicle2).Model;
						if (!model.IsBike)
						{
							model = ((Entity)copVehicle2).Model;
							if (!model.IsBoat)
							{
								continue;
							}
						}
					}
					if (copVehicle2.BodyHealth < 600f || copVehicle2.EngineHealth < 500f || Helpers.IsAnyTireBlown(copVehicle2))
					{
						copVehicle2.IsUndriveable = true;
					}
				}
			}
			if (Config.Options.BetterChases.RequireLethalForceAuthorization && IsWanted && !ActiveChase.DeadlyForce)
			{
				if (character.IsInVehicle() || character.IsBeingStunned || character.IsRagdoll || character.IsProne)
				{
					StopShooting = true;
				}
				else if (StopShooting)
				{
					StopShooting = false;
					foreach (Ped cop3 in Cops)
					{
						Function.Call((Hash)1505728147329083929L, (InputArgument[])(object)new InputArgument[3]
						{
							(Entity)((Entity)(object)cop3),
							(InputArgument)((Enum)(object)(WeaponHash)911657153),
							(InputArgument)10
						});
						Function.Call(unchecked((Hash)(-2534777595856374633L)), (InputArgument[])(object)new InputArgument[3]
						{
							(Entity)((Entity)(object)cop3),
							(InputArgument)((Enum)(object)(WeaponHash)911657153),
							(InputArgument)10
						});
					}
				}
			}
			else if (StopShooting)
			{
				StopShooting = false;
				foreach (Ped cop4 in Cops)
				{
					Function.Call((Hash)1505728147329083929L, (InputArgument[])(object)new InputArgument[3]
					{
						(Entity)((Entity)(object)cop4),
						(InputArgument)((Enum)(object)(WeaponHash)911657153),
						(InputArgument)10
					});
					Function.Call(unchecked((Hash)(-2534777595856374633L)), (InputArgument[])(object)new InputArgument[3]
					{
						(Entity)((Entity)(object)cop4),
						(InputArgument)((Enum)(object)(WeaponHash)911657153),
						(InputArgument)10
					});
				}
			}
			if (Config.Options.BetterChases.AllowBustOpportunity && IsWanted && Helpers.WantedLevel < 5 && !character.IsInVehicle() && !character.IsSwimming && !character.IsSwimmingUnderWater && !character.IsFalling && !character.IsJumping && !character.IsWalking && ((Entity)character).Speed < 2f && !character.IsInCover && !Game.Player.IsAiming)
			{
				if ((Game.IsKeyPressed(Config.Options.SurrenderKey) || Game.IsControlPressed(Config.Options.SurrenderButton)) && Helpers.IsCopNearby(((Entity)character).Position, 20f))
				{
					IsGettingBusted = true;
				}
				else if (IsGettingBusted)
				{
					IsGettingBusted = false;
				}
				if (IsGettingBusted && Game.Player.Wanted.WantedLevel > 1)
				{
					BustStars = Game.Player.Wanted.WantedLevel;
					Helpers.WantedLevel = 1;
					Helpers.MaxWantedLevel = 1;
				}
				else if (!IsGettingBusted && BustStars > 1)
				{
					Helpers.MaxWantedLevel = 5;
					Helpers.WantedLevel = BustStars;
					BustStars = 0;
				}
				if (IsGettingBusted && !character.IsBeingStunned && !character.IsRagdoll && !character.IsProne)
				{
					if (!AreHandsUp)
					{
						AreHandsUp = true;
						character.Task.PlayAnimation("mp_am_hold_up", "handsup_base", 2f, 1f, -1, (AnimationFlags)1, 0f);
					}
				}
				else if (!IsGettingBusted && AreHandsUp)
				{
					AreHandsUp = false;
					character.Task.StopScriptedAnimationTask(new CrClipAsset("mp_am_hold_up", "handsup_base"));
				}
			}
			if (Config.Options.BetterChases.ChaseEscalates.Enabled && IsWanted && !IsGettingBusted)
			{
				if (Config.Options.BetterChases.ChaseEscalates.PhaseOne.Enabled && !ActiveChase.Crimes.Fleeing1 && ActiveChase.Duration.TotalMinutes > (double)Config.Options.BetterChases.ChaseEscalates.PhaseOne.Length)
				{
					if (Config.Options.BetterChases.ChaseEscalates.PhaseOne.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.ChaseEscalates.PhaseOne.WantedLevel)
					{
						ActiveChase.Crimes.Fleeing1 = true;
						wantedLevel = Config.Options.BetterChases.ChaseEscalates.PhaseOne.WantedLevel;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseOne.PITAuthorized && !ActiveChase.PITAuthorized)
					{
						ActiveChase.Crimes.Fleeing1 = true;
						flag = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseOne.LethalForceAuthorized && !ActiveChase.DeadlyForce)
					{
						ActiveChase.Crimes.Fleeing1 = true;
						flag2 = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseOne.RequestBackup)
					{
						ActiveChase.Crimes.Fleeing1 = true;
						num++;
					}
					if (ActiveChase.Crimes.Fleeing1)
					{
						text = "Fleeing";
						text2 = "Suspect is refusing to stop";
						if (Config.Options.DisplayHints)
						{
							Renderer.ShowHelpMessage("The chase has gone on too long and is escalating.");
						}
					}
				}
				else if (Config.Options.BetterChases.ChaseEscalates.PhaseTwo.Enabled && !ActiveChase.Crimes.Fleeing2 && ActiveChase.Duration.TotalMinutes > (double)(Config.Options.BetterChases.ChaseEscalates.PhaseOne.Length + Config.Options.BetterChases.ChaseEscalates.PhaseTwo.Length))
				{
					if (Config.Options.BetterChases.ChaseEscalates.PhaseTwo.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.ChaseEscalates.PhaseTwo.WantedLevel)
					{
						ActiveChase.Crimes.Fleeing2 = true;
						wantedLevel = Config.Options.BetterChases.ChaseEscalates.PhaseTwo.WantedLevel;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseTwo.PITAuthorized && !ActiveChase.PITAuthorized)
					{
						ActiveChase.Crimes.Fleeing2 = true;
						flag = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseTwo.LethalForceAuthorized && !ActiveChase.DeadlyForce)
					{
						ActiveChase.Crimes.Fleeing2 = true;
						flag2 = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseTwo.RequestBackup)
					{
						ActiveChase.Crimes.Fleeing2 = true;
						num++;
					}
					if (ActiveChase.Crimes.Fleeing2)
					{
						text = "Fleeing";
						text2 = "Suspect is continuing to refuse to stop";
						if (Config.Options.DisplayHints)
						{
							Renderer.ShowHelpMessage("The chase has gone on too long and is continuing to escalate.");
						}
					}
				}
				else if (Config.Options.BetterChases.ChaseEscalates.PhaseThree.Enabled && !ActiveChase.Crimes.Fleeing3 && ActiveChase.Duration.TotalMinutes > (double)(Config.Options.BetterChases.ChaseEscalates.PhaseOne.Length + Config.Options.BetterChases.ChaseEscalates.PhaseTwo.Length + Config.Options.BetterChases.ChaseEscalates.PhaseThree.Length))
				{
					if (Config.Options.BetterChases.ChaseEscalates.PhaseThree.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.ChaseEscalates.PhaseThree.WantedLevel)
					{
						ActiveChase.Crimes.Fleeing3 = true;
						wantedLevel = Config.Options.BetterChases.ChaseEscalates.PhaseThree.WantedLevel;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseThree.PITAuthorized && !ActiveChase.PITAuthorized)
					{
						ActiveChase.Crimes.Fleeing3 = true;
						flag = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseThree.LethalForceAuthorized && !ActiveChase.DeadlyForce)
					{
						ActiveChase.Crimes.Fleeing3 = true;
						flag2 = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseThree.RequestBackup)
					{
						ActiveChase.Crimes.Fleeing3 = true;
						num++;
					}
					if (ActiveChase.Crimes.Fleeing3)
					{
						text = "Fleeing";
						text2 = "Suspect shows no sign of stopping";
						if (Config.Options.DisplayHints)
						{
							Renderer.ShowHelpMessage("You have continued to elude the police with no sign of stopping.");
						}
					}
				}
				else if (Config.Options.BetterChases.ChaseEscalates.PhaseFour.Enabled && !ActiveChase.Crimes.Fleeing4 && ActiveChase.Duration.TotalMinutes > (double)(Config.Options.BetterChases.ChaseEscalates.PhaseOne.Length + Config.Options.BetterChases.ChaseEscalates.PhaseTwo.Length + Config.Options.BetterChases.ChaseEscalates.PhaseThree.Length + Config.Options.BetterChases.ChaseEscalates.PhaseFour.Length))
				{
					if (Config.Options.BetterChases.ChaseEscalates.PhaseFour.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.ChaseEscalates.PhaseFour.WantedLevel)
					{
						ActiveChase.Crimes.Fleeing4 = true;
						wantedLevel = Config.Options.BetterChases.ChaseEscalates.PhaseFour.WantedLevel;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseFour.PITAuthorized && !ActiveChase.PITAuthorized)
					{
						ActiveChase.Crimes.Fleeing4 = true;
						flag = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseFour.LethalForceAuthorized && !ActiveChase.DeadlyForce)
					{
						ActiveChase.Crimes.Fleeing4 = true;
						flag2 = true;
					}
					if (Config.Options.BetterChases.ChaseEscalates.PhaseFour.RequestBackup)
					{
						ActiveChase.Crimes.Fleeing4 = true;
						num++;
					}
					if (ActiveChase.Crimes.Fleeing4)
					{
						text = "Fleeing";
						text2 = "Suspect stil shows no sign of stopping";
						if (Config.Options.DisplayHints)
						{
							Renderer.ShowHelpMessage("You have continued to elude the police with no sign of stopping.");
						}
					}
				}
			}
			if (Config.Options.BetterChases.Crimes.Stolen.Enabled && IsWanted && !ActiveChase.Crimes.GrandTheftAuto && !ActiveChase.Crimes.Stolen && Helpers.IsValid((Entity)(object)character.CurrentVehicle) && character.CurrentVehicle.IsStolen && (Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel == 0 || wantedLevel <= Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel) && (Config.Options.BetterChases.Crimes.Stolen.PoliceWitnessThreshold == 0 || Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Stolen.PoliceWitnessThreshold))
			{
				if (Config.Options.BetterChases.Crimes.Stolen.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Stolen.WantedLevel)
				{
					ActiveChase.Crimes.Stolen = true;
					wantedLevel = Config.Options.BetterChases.Crimes.Stolen.WantedLevel;
				}
				if (Config.Options.BetterChases.Crimes.Stolen.PITAuthorized && !ActiveChase.PITAuthorized)
				{
					ActiveChase.Crimes.Stolen = true;
					flag = true;
				}
				if (Config.Options.BetterChases.Crimes.Stolen.LethalForceAuthorized && !ActiveChase.DeadlyForce)
				{
					ActiveChase.Crimes.Stolen = true;
					flag2 = true;
				}
				if (Config.Options.BetterChases.Crimes.Stolen.RequestBackup)
				{
					ActiveChase.Crimes.Stolen = true;
					num++;
				}
				if (ActiveChase.Crimes.Stolen)
				{
					text = "Stolen Vehicle";
					text2 = "Suspect is driving a stolen vehicle";
					if (Config.Options.DisplayHints)
					{
						Renderer.ShowHelpMessage("You were spotted driving a stolen vehicle during a chase.");
					}
				}
			}
			if (Config.Options.BetterChases.Crimes.GTA.Enabled && IsWanted && !ActiveChase.Crimes.GrandTheftAuto && (character.IsJacking || character.IsTryingToEnterALockedVehicle) && (Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel == 0 || wantedLevel <= Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel) && (Config.Options.BetterChases.Crimes.GTA.PoliceWitnessThreshold == 0 || Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.GTA.PoliceWitnessThreshold))
			{
				if (Config.Options.BetterChases.Crimes.GTA.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.GTA.WantedLevel)
				{
					ActiveChase.Crimes.GrandTheftAuto = true;
					wantedLevel = Config.Options.BetterChases.Crimes.GTA.WantedLevel;
				}
				if (Config.Options.BetterChases.Crimes.GTA.PITAuthorized && !ActiveChase.PITAuthorized)
				{
					ActiveChase.Crimes.GrandTheftAuto = true;
					flag = true;
				}
				if (Config.Options.BetterChases.Crimes.GTA.LethalForceAuthorized && !ActiveChase.DeadlyForce)
				{
					ActiveChase.Crimes.GrandTheftAuto = true;
					flag2 = true;
				}
				if (Config.Options.BetterChases.Crimes.GTA.RequestBackup)
				{
					ActiveChase.Crimes.GrandTheftAuto = true;
					num++;
				}
				if (ActiveChase.Crimes.GrandTheftAuto)
				{
					text = "Grand Theft Auto";
					text2 = "Suspect is stealing a vehicle";
					if (Config.Options.DisplayHints)
					{
						Renderer.ShowHelpMessage("You were spotted stealing a vehicle during a chase.");
					}
				}
			}
			if (Config.Options.BetterChases.Crimes.Speeding.Enabled && IsWanted && !ActiveChase.Crimes.Speeding && Helpers.IsValid((Entity)(object)character.CurrentVehicle) && ((Entity)character.CurrentVehicle).Speed > (float)Config.Options.BetterChases.Crimes.Speeding.Speed && (Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel == 0 || wantedLevel <= Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel) && (Config.Options.BetterChases.Crimes.Speeding.PoliceWitnessThreshold == 0 || Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Speeding.PoliceWitnessThreshold))
			{
				if (Config.Options.BetterChases.Crimes.Speeding.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Speeding.WantedLevel)
				{
					ActiveChase.Crimes.Speeding = true;
					wantedLevel = Config.Options.BetterChases.Crimes.Speeding.WantedLevel;
				}
				if (Config.Options.BetterChases.Crimes.Speeding.PITAuthorized && !ActiveChase.PITAuthorized)
				{
					ActiveChase.Crimes.Speeding = true;
					flag = true;
				}
				if (Config.Options.BetterChases.Crimes.Speeding.LethalForceAuthorized && !ActiveChase.DeadlyForce)
				{
					ActiveChase.Crimes.Speeding = true;
					flag2 = true;
				}
				if (Config.Options.BetterChases.Crimes.Speeding.RequestBackup)
				{
					ActiveChase.Crimes.Speeding = true;
					num++;
				}
				if (ActiveChase.Crimes.Speeding)
				{
					string text3 = ((!Function.Call<bool>(unchecked((Hash)(-3183669636887824493L)), Array.Empty<InputArgument>())) ? (Math.Round((double)((Entity)character.CurrentVehicle).Speed * 2.237, 0) + "MPH") : (Math.Round((double)((Entity)character.CurrentVehicle).Speed * 3.6, 0) + "KPH"));
					text = "Excessive Speeding";
					text2 = "Suspect is speeding excessively at over ~y~" + text3 + "~w~";
					if (Config.Options.DisplayHints)
					{
						Renderer.ShowHelpMessage("You were spotted driving very fast during a chase.");
					}
				}
			}
			if (Config.Options.BetterChases.Crimes.Reckless.Enabled && IsWanted && !ActiveChase.Crimes.Reckless && Helpers.IsValid((Entity)(object)character.CurrentVehicle))
			{
				model = ((Entity)character.CurrentVehicle).Model;
				if (!model.IsBicycle && ((Entity)character.CurrentVehicle).Speed > (float)Config.Options.BetterChases.Crimes.Reckless.Speed && ((Function.Call<int>(unchecked((Hash)(-2627470921521327742L)), (InputArgument[])(object)new InputArgument[1] { (Player)Game.Player }) > -1 && Function.Call<int>(unchecked((Hash)(-2627470921521327742L)), (InputArgument[])(object)new InputArgument[1] { (Player)Game.Player }) < 1000) || (Function.Call<int>(unchecked((Hash)(-3073193554563594949L)), (InputArgument[])(object)new InputArgument[1] { (Player)Game.Player }) > -1 && Function.Call<int>(unchecked((Hash)(-3073193554563594949L)), (InputArgument[])(object)new InputArgument[1] { (Player)Game.Player }) < 1000)) && (Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel == 0 || wantedLevel <= Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel) && (Config.Options.BetterChases.Crimes.Reckless.PoliceWitnessThreshold == 0 || Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Reckless.PoliceWitnessThreshold))
				{
					if (Config.Options.BetterChases.Crimes.Reckless.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Reckless.WantedLevel)
					{
						ActiveChase.Crimes.Reckless = true;
						wantedLevel = Config.Options.BetterChases.Crimes.Reckless.WantedLevel;
					}
					if (Config.Options.BetterChases.Crimes.Reckless.PITAuthorized && !ActiveChase.PITAuthorized)
					{
						ActiveChase.Crimes.Reckless = true;
						flag = true;
					}
					if (Config.Options.BetterChases.Crimes.Reckless.LethalForceAuthorized && !ActiveChase.DeadlyForce)
					{
						ActiveChase.Crimes.Reckless = true;
						flag2 = true;
					}
					if (Config.Options.BetterChases.Crimes.Reckless.RequestBackup)
					{
						ActiveChase.Crimes.Reckless = true;
						num++;
					}
					if (ActiveChase.Crimes.Reckless)
					{
						text = "Reckless Driving";
						text2 = "Suspect is driving recklessly";
						if (Config.Options.DisplayHints)
						{
							Renderer.ShowHelpMessage("You have been driving on sidewalks or the wrong way.");
						}
					}
				}
			}
			if (Config.Options.BetterChases.Crimes.Armed.Enabled && IsWanted && !ActiveChase.Crimes.Armed && !IsGettingBusted && Helpers.IsArmed && !character.IsSittingInVehicle() && (Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel == 0 || wantedLevel <= Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel) && (Config.Options.BetterChases.Crimes.Armed.PoliceWitnessThreshold == 0 || Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Armed.PoliceWitnessThreshold))
			{
				if (Config.Options.BetterChases.Crimes.Armed.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Armed.WantedLevel)
				{
					ActiveChase.Crimes.Armed = true;
					wantedLevel = Config.Options.BetterChases.Crimes.Armed.WantedLevel;
				}
				if (Config.Options.BetterChases.Crimes.Armed.PITAuthorized && !ActiveChase.PITAuthorized)
				{
					ActiveChase.Crimes.Armed = true;
					flag = true;
				}
				if (Config.Options.BetterChases.Crimes.Armed.LethalForceAuthorized && !ActiveChase.DeadlyForce)
				{
					ActiveChase.Crimes.Armed = true;
					flag2 = true;
				}
				if (Config.Options.BetterChases.Crimes.Armed.RequestBackup)
				{
					ActiveChase.Crimes.Armed = true;
					num++;
				}
				if (ActiveChase.Crimes.Armed)
				{
					text = "Armed Suspect";
					text2 = "Suspect is armed with a weapon";
					if (Config.Options.DisplayHints)
					{
						Renderer.ShowHelpMessage("You were spotted brandishing a weapon during a chase.");
					}
				}
			}
			if (Config.Options.BetterChases.Crimes.Aiming.Enabled && IsWanted && !ActiveChase.Crimes.Aiming && Helpers.IsArmed && character.IsAiming && (Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel == 0 || wantedLevel <= Config.Options.BetterChases.Crimes.Stolen.MaxWantedLevel) && (Config.Options.BetterChases.Crimes.Aiming.PoliceWitnessThreshold == 0 || Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Aiming.PoliceWitnessThreshold))
			{
				if (Config.Options.BetterChases.Crimes.Aiming.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Aiming.WantedLevel)
				{
					ActiveChase.Crimes.Aiming = true;
					wantedLevel = Config.Options.BetterChases.Crimes.Aiming.WantedLevel;
				}
				if (Config.Options.BetterChases.Crimes.Aiming.PITAuthorized && !ActiveChase.PITAuthorized)
				{
					ActiveChase.Crimes.Aiming = true;
					flag = true;
				}
				if (Config.Options.BetterChases.Crimes.Aiming.LethalForceAuthorized && !ActiveChase.DeadlyForce)
				{
					ActiveChase.Crimes.Aiming = true;
					flag2 = true;
				}
				if (Config.Options.BetterChases.Crimes.Aiming.RequestBackup)
				{
					ActiveChase.Crimes.Aiming = true;
					num++;
				}
				if (ActiveChase.Crimes.Aiming)
				{
					text = "Raised Weapon";
					text2 = "Suspect is aiming a weapon";
					if (Config.Options.DisplayHints)
					{
						Renderer.ShowHelpMessage("You were spotted aiming a weapon during a chase.");
					}
				}
			}
			if (IsWanted)
			{
				if (Config.Options.BetterChases.Crimes.Assault.Enabled && !ActiveChase.Crimes.Assault)
				{
					foreach (Ped ped in Peds)
					{
						if ((!((Entity)ped).HasBeenDamagedBy((Entity)(object)character) && (!Helpers.IsValid((Entity)(object)character.CurrentVehicle) || !((Entity)ped).HasBeenDamagedBy((Entity)(object)character.CurrentVehicle))) || (Config.Options.BetterChases.Crimes.Assault.MaxWantedLevel != 0 && wantedLevel > Config.Options.BetterChases.Crimes.Assault.MaxWantedLevel) || (Config.Options.BetterChases.Crimes.Assault.PoliceWitnessThreshold != 0 && !(Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Assault.PoliceWitnessThreshold)))
						{
							continue;
						}
						if (Config.Options.BetterChases.Crimes.Assault.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Assault.WantedLevel)
						{
							ActiveChase.Crimes.Assault = true;
							wantedLevel = Config.Options.BetterChases.Crimes.Assault.WantedLevel;
						}
						if (Config.Options.BetterChases.Crimes.Assault.PITAuthorized && !ActiveChase.PITAuthorized)
						{
							ActiveChase.Crimes.Assault = true;
							flag = true;
						}
						if (Config.Options.BetterChases.Crimes.Assault.LethalForceAuthorized && !ActiveChase.DeadlyForce)
						{
							ActiveChase.Crimes.Assault = true;
							flag2 = true;
						}
						if (Config.Options.BetterChases.Crimes.Assault.RequestBackup)
						{
							ActiveChase.Crimes.Assault = true;
							num++;
						}
						if (ActiveChase.Crimes.Assault)
						{
							text = "Assault";
							text2 = "Suspect assaulted a civilian";
							if (Config.Options.DisplayHints)
							{
								Renderer.ShowHelpMessage("You were spotted harming a civilian during a chase.");
							}
							break;
						}
					}
				}
				if (Config.Options.BetterChases.Crimes.PoliceAssault.Enabled && !ActiveChase.Crimes.PoliceAssault)
				{
					foreach (Ped cop5 in Cops)
					{
						if ((!((Entity)cop5).HasBeenDamagedBy((Entity)(object)character) && (!Helpers.IsValid((Entity)(object)character.CurrentVehicle) || !((Entity)cop5).HasBeenDamagedBy((Entity)(object)character.CurrentVehicle))) || (Config.Options.BetterChases.Crimes.PoliceAssault.MaxWantedLevel != 0 && wantedLevel > Config.Options.BetterChases.Crimes.PoliceAssault.MaxWantedLevel) || (Config.Options.BetterChases.Crimes.PoliceAssault.PoliceWitnessThreshold != 0 && !(Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.PoliceAssault.PoliceWitnessThreshold)))
						{
							continue;
						}
						if (Config.Options.BetterChases.Crimes.PoliceAssault.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.PoliceAssault.WantedLevel)
						{
							ActiveChase.Crimes.PoliceAssault = true;
							wantedLevel = Config.Options.BetterChases.Crimes.PoliceAssault.WantedLevel;
						}
						if (Config.Options.BetterChases.Crimes.PoliceAssault.PITAuthorized && !ActiveChase.PITAuthorized)
						{
							ActiveChase.Crimes.PoliceAssault = true;
							flag = true;
						}
						if (Config.Options.BetterChases.Crimes.PoliceAssault.LethalForceAuthorized && !ActiveChase.DeadlyForce)
						{
							ActiveChase.Crimes.PoliceAssault = true;
							flag2 = true;
						}
						if (Config.Options.BetterChases.Crimes.PoliceAssault.RequestBackup)
						{
							ActiveChase.Crimes.PoliceAssault = true;
							num++;
						}
						if (ActiveChase.Crimes.PoliceAssault)
						{
							text = "Assaulting Police";
							text2 = "Suspect assaulted a police officer";
							if (Config.Options.DisplayHints)
							{
								Renderer.ShowHelpMessage("You were spotted harming a police officer during a chase.");
							}
							break;
						}
					}
				}
			}
			if (Config.Options.BetterChases.Crimes.Shooting.Enabled && IsWanted && !ActiveChase.Crimes.Shooting && Helpers.IsArmed && IsShooting && (Config.Options.BetterChases.Crimes.Shooting.MaxWantedLevel == 0 || wantedLevel <= Config.Options.BetterChases.Crimes.Shooting.MaxWantedLevel) && (Config.Options.BetterChases.Crimes.Shooting.PoliceWitnessThreshold == 0 || Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Shooting.PoliceWitnessThreshold))
			{
				if (Config.Options.BetterChases.Crimes.Shooting.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Shooting.WantedLevel)
				{
					ActiveChase.Crimes.Shooting = true;
					wantedLevel = Config.Options.BetterChases.Crimes.Shooting.WantedLevel;
				}
				if (Config.Options.BetterChases.Crimes.Shooting.PITAuthorized && !ActiveChase.PITAuthorized)
				{
					ActiveChase.Crimes.Shooting = true;
					flag = true;
				}
				if (Config.Options.BetterChases.Crimes.Shooting.LethalForceAuthorized && !ActiveChase.DeadlyForce)
				{
					ActiveChase.Crimes.Shooting = true;
					flag2 = true;
				}
				if (Config.Options.BetterChases.Crimes.Shooting.RequestBackup)
				{
					ActiveChase.Crimes.Shooting = true;
					num++;
				}
				if (ActiveChase.Crimes.Shooting)
				{
					text = "Shots fired";
					text2 = "Shots have been fired";
					if (Config.Options.DisplayHints)
					{
						Renderer.ShowHelpMessage("You were spotted shooting a weapon during a chase.");
					}
				}
			}
			if (IsWanted)
			{
				if (Config.Options.BetterChases.Crimes.Murder.Enabled && !ActiveChase.Crimes.Murder)
				{
					foreach (Ped ped2 in Peds)
					{
						if (((Entity)ped2).IsAlive || PedsKilled.Contains(ped2) || !Helpers.IsValid(ped2.Killer) || (((PoolObject)ped2.Killer).Handle != ((PoolObject)character).Handle && (!Helpers.IsValid((Entity)(object)character.CurrentVehicle) || ((PoolObject)ped2.Killer).Handle != ((PoolObject)character.CurrentVehicle).Handle)))
						{
							continue;
						}
						PedsKilled.Add(ped2);
						if ((Config.Options.BetterChases.Crimes.Murder.MaxWantedLevel != 0 && wantedLevel > Config.Options.BetterChases.Crimes.Murder.MaxWantedLevel) || (Config.Options.BetterChases.Crimes.Murder.PoliceWitnessThreshold != 0 && !(Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.Murder.PoliceWitnessThreshold)))
						{
							continue;
						}
						if (Config.Options.BetterChases.Crimes.Murder.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.Murder.WantedLevel)
						{
							ActiveChase.Crimes.Murder = true;
							wantedLevel = Config.Options.BetterChases.Crimes.Murder.WantedLevel;
						}
						if (Config.Options.BetterChases.Crimes.Murder.PITAuthorized && !ActiveChase.PITAuthorized)
						{
							ActiveChase.Crimes.Murder = true;
							flag = true;
						}
						if (Config.Options.BetterChases.Crimes.Murder.LethalForceAuthorized && !ActiveChase.DeadlyForce)
						{
							ActiveChase.Crimes.Murder = true;
							flag2 = true;
						}
						if (Config.Options.BetterChases.Crimes.Murder.RequestBackup)
						{
							ActiveChase.Crimes.Murder = true;
							num++;
						}
						if (ActiveChase.Crimes.Murder)
						{
							text = "Murder";
							text2 = "Suspect killed a civilian";
							if (Config.Options.DisplayHints)
							{
								Renderer.ShowHelpMessage("You were spotted killing a civilian during a chase.");
							}
							break;
						}
					}
				}
				if (Config.Options.BetterChases.Crimes.PoliceMurder.Enabled && !ActiveChase.Crimes.PoliceMurder)
				{
					foreach (Ped cop6 in Cops)
					{
						if (((Entity)cop6).IsAlive || CopsKilled.Contains(cop6) || !Helpers.IsValid(cop6.Killer) || (((PoolObject)cop6.Killer).Handle != ((PoolObject)character).Handle && (!Helpers.IsValid((Entity)(object)character.CurrentVehicle) || ((PoolObject)cop6.Killer).Handle != ((PoolObject)character.CurrentVehicle).Handle)))
						{
							continue;
						}
						CopsKilled.Add(cop6);
						if ((Config.Options.BetterChases.Crimes.PoliceMurder.MaxWantedLevel != 0 && wantedLevel > Config.Options.BetterChases.Crimes.PoliceMurder.MaxWantedLevel) || (Config.Options.BetterChases.Crimes.PoliceMurder.PoliceWitnessThreshold != 0 && !(Witnesses.GetMaxRecognition(Witnesses.Cops) >= (double)Config.Options.BetterChases.Crimes.PoliceMurder.PoliceWitnessThreshold)))
						{
							continue;
						}
						if (Config.Options.BetterChases.Crimes.PoliceMurder.WantedLevel > 0 && wantedLevel < Config.Options.BetterChases.Crimes.PoliceMurder.WantedLevel)
						{
							ActiveChase.Crimes.PoliceMurder = true;
							wantedLevel = Config.Options.BetterChases.Crimes.PoliceMurder.WantedLevel;
						}
						if (Config.Options.BetterChases.Crimes.PoliceMurder.PITAuthorized && !ActiveChase.PITAuthorized)
						{
							ActiveChase.Crimes.PoliceMurder = true;
							flag = true;
						}
						if (Config.Options.BetterChases.Crimes.PoliceMurder.LethalForceAuthorized && !ActiveChase.DeadlyForce)
						{
							ActiveChase.Crimes.PoliceMurder = true;
							flag2 = true;
						}
						if (Config.Options.BetterChases.Crimes.PoliceMurder.RequestBackup)
						{
							ActiveChase.Crimes.PoliceMurder = true;
							num++;
						}
						if (ActiveChase.Crimes.PoliceMurder)
						{
							text = "Police Murder";
							text2 = "Suspect killed a police officer";
							if (Config.Options.DisplayHints)
							{
								Renderer.ShowHelpMessage("You were spotted killing a police officer during a chase.");
							}
							break;
						}
					}
				}
			}
			if (!ActiveChase.PITAuthorized && !flag)
			{
				switch (Game.Player.Wanted.WantedLevel)
				{
				case 1:
					flag = Config.Options.BetterChases.CopDispatch.OneStar.PITAuthorized;
					break;
				case 2:
					flag = Config.Options.BetterChases.CopDispatch.TwoStar.PITAuthorized;
					break;
				case 3:
					flag = Config.Options.BetterChases.CopDispatch.ThreeStar.PITAuthorized;
					break;
				case 4:
					flag = Config.Options.BetterChases.CopDispatch.FourStar.PITAuthorized;
					break;
				case 5:
					flag = Config.Options.BetterChases.CopDispatch.FiveStar.PITAuthorized;
					break;
				}
				if (flag && text == "")
				{
					text = "Dangerous Suspect";
					text2 = "Suspect is behaving dangerously";
				}
			}
			else if (!ActiveChase.DeadlyForce && !flag2)
			{
				switch (Game.Player.Wanted.WantedLevel)
				{
				case 1:
					flag2 = Config.Options.BetterChases.CopDispatch.OneStar.LethalForceAuthorized;
					break;
				case 2:
					flag2 = Config.Options.BetterChases.CopDispatch.TwoStar.LethalForceAuthorized;
					break;
				case 3:
					flag2 = Config.Options.BetterChases.CopDispatch.ThreeStar.LethalForceAuthorized;
					break;
				case 4:
					flag2 = Config.Options.BetterChases.CopDispatch.FourStar.LethalForceAuthorized;
					break;
				case 5:
					flag2 = Config.Options.BetterChases.CopDispatch.FiveStar.LethalForceAuthorized;
					break;
				}
				if (flag2 && text == "")
				{
					text = "Dangerous Suspect";
					text2 = "Suspect is behaving dangerously";
				}
			}
			if (text != "")
			{
				string text4 = text2;
				if (flag2)
				{
					ActiveChase.DeadlyForce = true;
					text4 += ", ~r~deadly force~w~ has been authorized";
				}
				if (flag)
				{
					ActiveChase.PITAuthorized = true;
					text4 += ", ~y~PIT~w~ has been authorized";
					if (Config.Options.DisplayHints)
					{
						Renderer.ShowHelpMessage("~y~PIT~w~ has been authorized.");
						Renderer.ShowHelpMessage("Cops will now ~y~ram you~w~ at will.");
						Renderer.ShowHelpMessage("However, they will still refrain from doing so in ~y~populated areas~w~.");
					}
					if (Config.Options.BetterChases.ShowBigMessages)
					{
						Renderer.ShowBigMessage("PIT AUTHORIZED", "", Renderer.HudColor.GOLD, Renderer.HudColor.BLACK);
					}
				}
				if (num > 0 || num2 > 0 || wantedLevel > Game.Player.Wanted.WantedLevel)
				{
					int num3 = Helpers.MinGroundUnits + num + AdditionalGroundUnits;
					int num4 = Helpers.MinAirUnits + num2 + AdditionalAirUnits;
					if (wantedLevel > Game.Player.Wanted.WantedLevel)
					{
						Helpers.WantedLevel = wantedLevel;
						AdditionalGroundUnits = Math.Max(num3 - Helpers.MinGroundUnits, 0);
						AdditionalAirUnits = Math.Max(num4 - Helpers.MinAirUnits, 0);
						if (AdditionalGroundUnits + Helpers.MinGroundUnits > num3 && AdditionalAirUnits + Helpers.MinAirUnits > num4)
						{
							text4 = text4 + ", backup required at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~, requesting air unit same location";
						}
						else if (AdditionalGroundUnits + Helpers.MinGroundUnits > num3)
						{
							text4 = text4 + ", backup required at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~";
						}
						else if (AdditionalAirUnits + Helpers.MinAirUnits > num4)
						{
							text4 = text4 + ", requesting air unit at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~";
						}
					}
					else if (Helpers.MinGroundUnits + num + AdditionalGroundUnits > Helpers.MaxGroundUnits)
					{
						Helpers.WantedLevel++;
						AdditionalGroundUnits = Math.Max(num3 - Helpers.MinGroundUnits, 0);
						AdditionalAirUnits = Math.Max(num4 - Helpers.MinAirUnits, 0);
						if (num > 0 && num2 > 0)
						{
							text4 = text4 + ", backup required at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~, requesting air unit same location";
						}
						else if (num > 0)
						{
							text4 = text4 + ", backup required at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~";
						}
						else if (num2 > 0)
						{
							text4 = text4 + ", requesting air unit at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~";
						}
					}
					else
					{
						AdditionalGroundUnits += num;
						AdditionalAirUnits += num2;
						if (num > 0 && num2 > 0)
						{
							text4 = text4 + ", backup required at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~, requesting air unit same location";
						}
						else if (num > 0)
						{
							text4 = text4 + ", backup required at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~";
						}
						else if (num2 > 0)
						{
							text4 = text4 + ", requesting air unit at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~";
						}
					}
				}
				if (wantedLevel > Game.Player.Wanted.WantedLevel)
				{
					Helpers.WantedLevel = wantedLevel;
					text4 = text4 + ", backup required at ~y~" + World.GetStreetName(((Entity)character).Position) + "~w~";
				}
				Function.Call((Hash)2316831480196236324L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)"STRING" });
				Function.Call((Hash)7789129354908300458L, (InputArgument[])(object)new InputArgument[1] { (InputArgument)(text4 + "~w~.") });
				Function.Call((Hash)2075484565200204495L, (InputArgument[])(object)new InputArgument[6]
				{
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)"WEB_LOSSANTOSPOLICEDEPT",
					(InputArgument)true,
					(InputArgument)0,
					(InputArgument)text.ToUpper(),
					(InputArgument)"~c~LSPD"
				});
			}
			if (IsWanted)
			{
				ActiveChase.Duration += GameClockCompat.Now - ChaseTime - ActiveChase.Duration;
				ArrestWarrants.ActiveWarrant.Chase = ActiveChase;
			}
			IsShooting = false;
			WasWanted = IsWanted;
		}
	}

	public class BetterChasesActive : Script
	{
		private static bool IsWanted;

		private static bool WasWanted;

		public BetterChasesActive()
		{
			base.Tick += OnTick;
			base.Interval = 1;
		}

		private void OnTick(object sender, EventArgs e)
		{
			//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0306: Unknown result type (might be due to invalid IL or missing references)
			//IL_030b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0318: Unknown result type (might be due to invalid IL or missing references)
			//IL_031d: Unknown result type (might be due to invalid IL or missing references)
			//IL_039b: Unknown result type (might be due to invalid IL or missing references)
			//IL_05af: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_0368: Unknown result type (might be due to invalid IL or missing references)
			//IL_036e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0373: Unknown result type (might be due to invalid IL or missing references)
			//IL_0405: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0429: Unknown result type (might be due to invalid IL or missing references)
			//IL_042e: Unknown result type (might be due to invalid IL or missing references)
			//IL_044b: Unknown result type (might be due to invalid IL or missing references)
			//IL_045a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0469: Unknown result type (might be due to invalid IL or missing references)
			//IL_0476: Unknown result type (might be due to invalid IL or missing references)
			//IL_047b: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_049b: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0800: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0627: Unknown result type (might be due to invalid IL or missing references)
			//IL_062c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0649: Unknown result type (might be due to invalid IL or missing references)
			//IL_0658: Unknown result type (might be due to invalid IL or missing references)
			//IL_0667: Unknown result type (might be due to invalid IL or missing references)
			//IL_0674: Unknown result type (might be due to invalid IL or missing references)
			//IL_0679: Unknown result type (might be due to invalid IL or missing references)
			//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0871: Unknown result type (might be due to invalid IL or missing references)
			//IL_0876: Unknown result type (might be due to invalid IL or missing references)
			//IL_0893: Unknown result type (might be due to invalid IL or missing references)
			//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_08be: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0904: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0912: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0920: Unknown result type (might be due to invalid IL or missing references)
			//IL_092e: Unknown result type (might be due to invalid IL or missing references)
			//IL_093c: Unknown result type (might be due to invalid IL or missing references)
			if (!Config.Options.BetterChases.Enabled)
			{
				return;
			}
			Ped character = Game.Player.Character;
			IsWanted = ((Game.Player.Wanted.WantedLevel > 0 || Function.Call<bool>((Hash)751735369465373403L, (InputArgument[])(object)new InputArgument[1] { (Player)Game.Player })) ? true : false);
			if (Config.Options.BetterChases.WantedLevelControl == "Full")
			{
				if (IsWanted && !WasWanted)
				{
					Helpers.WantedLevel = Game.Player.Wanted.WantedLevel;
					Helpers.MaxWantedLevel = Game.Player.Wanted.WantedLevel;
				}
				else if (!IsWanted && WasWanted)
				{
					Helpers.WantedLevel = 0;
					Helpers.MaxWantedLevel = 5;
				}
				else if (IsWanted && Function.Call<int>((Hash)5056994522776984671L, Array.Empty<InputArgument>()) != Helpers.MaxWantedLevel)
				{
					Helpers.MaxWantedLevel = Helpers.MaxWantedLevel;
				}
				else if (IsWanted && Game.Player.Wanted.WantedLevel != Helpers.WantedLevel)
				{
					Helpers.WantedLevel = Helpers.WantedLevel;
				}
			}
			if (IsWanted && character.IsShooting)
			{
				IsShooting = true;
			}
			if (Config.Options.BetterChases.RequireLethalForceAuthorization && !ActiveChase.DeadlyForce && StopShooting)
			{
				foreach (Ped cop in Cops)
				{
					cop.Weapons.Select((WeaponHash)911657153, true);
					cop.CanSwitchWeapons = false;
					Function.Call((Hash)4529689184233022011L, (InputArgument[])(object)new InputArgument[3]
					{
						(Entity)((Entity)(object)cop),
						(InputArgument)false,
						(InputArgument)((Enum)(object)(WeaponHash)911657153)
					});
					Function.Call((Hash)1746743299266654598L, (InputArgument[])(object)new InputArgument[2]
					{
						(Entity)((Entity)(object)cop),
						(InputArgument)false
					});
					Function.Call((Hash)1505728147329083929L, (InputArgument[])(object)new InputArgument[3]
					{
						(Entity)((Entity)(object)cop),
						(InputArgument)((Enum)(object)(WeaponHash)911657153),
						(InputArgument)(-1)
					});
					Function.Call(unchecked((Hash)(-2534777595856374633L)), (InputArgument[])(object)new InputArgument[3]
					{
						(Entity)((Entity)(object)cop),
						(InputArgument)((Enum)(object)(WeaponHash)911657153),
						(InputArgument)(-1)
					});
				}
			}
			foreach (Vehicle copVehicle in CopVehicles)
			{
				if (!copVehicle.IsOnAllWheels)
				{
					continue;
				}
				Model model = ((Entity)copVehicle).Model;
				if (!model.IsCar)
				{
					model = ((Entity)copVehicle).Model;
					if (!model.IsBike)
					{
						model = ((Entity)copVehicle).Model;
						if (!model.IsBoat)
						{
							continue;
						}
					}
				}
				if (Config.Options.BetterChases.RequirePITAuthorization && IsWanted && Helpers.IsValid((Entity)(object)character.CurrentVehicle) && (!ActiveChase.PITAuthorized || Helpers.IsPopulatedArea(((Entity)character).Position + ((Entity)character).Velocity, 40f)) && ((Entity)copVehicle).IsInRange(((Entity)character.CurrentVehicle).Position, 14f) && Function.Call<Vector3>(unchecked((Hash)(-7310063430528173299L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)copVehicle),
					(InputArgument)true
				}).Y > 0f && Function.Call<Vector3>(unchecked((Hash)(-7310063430528173299L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)character.CurrentVehicle),
					(InputArgument)true
				}).Y > 0f)
				{
					Vector3 position = ((Entity)character.CurrentVehicle).Position;
					Vector3 val = Function.Call<Vector3>((Hash)2482816124249826099L, (InputArgument[])(object)new InputArgument[4]
					{
						(Entity)((Entity)(object)copVehicle),
						(InputArgument)position.X,
						(InputArgument)position.Y,
						(InputArgument)position.Z
					});
					float num = ((Entity)copVehicle).Speed - ((Entity)character.CurrentVehicle).Speed;
					if (num > 0f && val.Y > 0f && val.Z < 3f && val.Z > -3f && val.X < 2f && val.X > -2f)
					{
						float num2 = ((num < 0.6f) ? (num * -1f) : (-0.6f));
						Function.Call((Hash)1801159460433909150L, (InputArgument[])(object)new InputArgument[9]
						{
							(Entity)((Entity)(object)copVehicle),
							(InputArgument)1,
							(InputArgument)0f,
							(InputArgument)num2,
							(InputArgument)0f,
							(InputArgument)true,
							(InputArgument)true,
							(InputArgument)true,
							(InputArgument)true
						});
					}
				}
				if (Config.Options.BetterChases.CopsManageTraffic && Function.Call<Vector3>(unchecked((Hash)(-7310063430528173299L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)copVehicle),
					(InputArgument)true
				}).Y > 5f)
				{
					Ped[] nearbyPeds = World.GetNearbyPeds(((Entity)copVehicle).Position, 14f, Array.Empty<Model>());
					Ped[] array = nearbyPeds;
					foreach (Ped val2 in array)
					{
						if (((PoolObject)val2).Handle != ((PoolObject)character).Handle && !Helpers.IsValid((Entity)(object)val2.CurrentVehicle))
						{
							Vector3 position2 = ((Entity)val2).Position;
							Vector3 val3 = Function.Call<Vector3>((Hash)2482816124249826099L, (InputArgument[])(object)new InputArgument[4]
							{
								(Entity)((Entity)(object)copVehicle),
								(InputArgument)position2.X,
								(InputArgument)position2.Y,
								(InputArgument)position2.Z
							});
							float num3 = ((Entity)copVehicle).Speed - Function.Call<Vector3>(unchecked((Hash)(-7310063430528173299L)), (InputArgument[])(object)new InputArgument[2]
							{
								(Entity)((Entity)(object)val2),
								(InputArgument)true
							}).Y;
							if (num3 > 0f && val3.Y > 0f && val3.Z < 3f && val3.Z > -3f && val3.X < 2f && val3.X > -2f)
							{
								float num4 = ((num3 < 0.8f) ? (num3 * -1f) : (-0.8f));
								Function.Call((Hash)1801159460433909150L, (InputArgument[])(object)new InputArgument[9]
								{
									(Entity)((Entity)(object)copVehicle),
									(InputArgument)1,
									(InputArgument)0f,
									(InputArgument)num4,
									(InputArgument)0f,
									(InputArgument)true,
									(InputArgument)true,
									(InputArgument)true,
									(InputArgument)true
								});
							}
						}
					}
				}
				if (!Config.Options.BetterChases.CopsManageTraffic || !(Function.Call<Vector3>(unchecked((Hash)(-7310063430528173299L)), (InputArgument[])(object)new InputArgument[2]
				{
					(Entity)((Entity)(object)copVehicle),
					(InputArgument)true
				}).Y > 5f))
				{
					continue;
				}
				Vehicle[] nearbyVehicles = World.GetNearbyVehicles(((Entity)copVehicle).Position, 14f, Array.Empty<Model>());
				Vehicle[] array2 = nearbyVehicles;
				foreach (Vehicle val4 in array2)
				{
					if (((PoolObject)val4).Handle != ((PoolObject)copVehicle).Handle && (!Helpers.IsValid((Entity)(object)character.CurrentVehicle) || ((PoolObject)character.CurrentVehicle).Handle != ((PoolObject)val4).Handle))
					{
						Vector3 position3 = ((Entity)val4).Position;
						Vector3 val5 = Function.Call<Vector3>((Hash)2482816124249826099L, (InputArgument[])(object)new InputArgument[4]
						{
							(Entity)((Entity)(object)copVehicle),
							(InputArgument)position3.X,
							(InputArgument)position3.Y,
							(InputArgument)position3.Z
						});
						float num5 = ((Entity)copVehicle).Speed - Function.Call<Vector3>(unchecked((Hash)(-7310063430528173299L)), (InputArgument[])(object)new InputArgument[2]
						{
							(Entity)((Entity)(object)val4),
							(InputArgument)true
						}).Y;
						if (num5 > 0f && val5.Y > 0f && val5.Z < 3f && val5.Z > -3f && val5.X < 2f && val5.X > -2f)
						{
							float num6 = ((num5 < 0.8f) ? (num5 * -1f) : (-0.8f));
							Function.Call((Hash)1801159460433909150L, (InputArgument[])(object)new InputArgument[9]
							{
								(Entity)((Entity)(object)copVehicle),
								(InputArgument)1,
								(InputArgument)0f,
								(InputArgument)num6,
								(InputArgument)0f,
								(InputArgument)true,
								(InputArgument)true,
								(InputArgument)true,
								(InputArgument)true
							});
						}
					}
				}
			}
			WasWanted = IsWanted;
		}
	}

	public static bool CopSearch;

	public static Chase ActiveChase = new Chase();

	public static List<Ped> Peds = new List<Ped>();

	public static List<Vehicle> PedVehicles = new List<Vehicle>();

	public static List<Ped> Cops = new List<Ped>();

	public static List<Vehicle> CopVehicles = new List<Vehicle>();

	private static bool IsShooting = false;

	private static bool StopShooting = false;

	public static Chase MergeChases(Chase chase1, Chase chase2)
	{
		Chase chase3 = new Chase
		{
			DeadlyForce = (chase1.DeadlyForce || chase2.DeadlyForce),
			PITAuthorized = (chase1.PITAuthorized || chase2.PITAuthorized),
			Duration = chase1.Duration + chase2.Duration,
			StartTime = ((chase1.StartTime.CompareTo(chase2.StartTime) == -1) ? chase1.StartTime : chase2.StartTime)
		};
		chase3.Crimes.Fleeing1 = chase1.Crimes.Fleeing1 || chase2.Crimes.Fleeing1;
		chase3.Crimes.Fleeing2 = chase1.Crimes.Fleeing2 || chase2.Crimes.Fleeing2;
		chase3.Crimes.Fleeing3 = chase1.Crimes.Fleeing3 || chase2.Crimes.Fleeing3;
		chase3.Crimes.Fleeing4 = chase1.Crimes.Fleeing4 || chase2.Crimes.Fleeing4;
		chase3.Crimes.GrandTheftAuto = chase1.Crimes.GrandTheftAuto || chase2.Crimes.GrandTheftAuto;
		chase3.Crimes.Stolen = chase1.Crimes.Stolen || chase2.Crimes.Stolen;
		chase3.Crimes.Speeding = chase1.Crimes.Speeding || chase2.Crimes.Speeding;
		chase3.Crimes.Reckless = chase1.Crimes.Reckless || chase2.Crimes.Reckless;
		chase3.Crimes.Armed = chase1.Crimes.Armed || chase2.Crimes.Armed;
		chase3.Crimes.Aiming = chase1.Crimes.Aiming || chase2.Crimes.Aiming;
		chase3.Crimes.Assault = chase1.Crimes.Assault || chase2.Crimes.Assault;
		chase3.Crimes.PoliceAssault = chase1.Crimes.PoliceAssault || chase2.Crimes.PoliceAssault;
		chase3.Crimes.Shooting = chase1.Crimes.Shooting || chase2.Crimes.Shooting;
		chase3.Crimes.Murder = chase1.Crimes.Murder || chase2.Crimes.Murder;
		chase3.Crimes.PoliceMurder = chase1.Crimes.PoliceMurder || chase2.Crimes.PoliceMurder;
		return chase3;
	}
}
