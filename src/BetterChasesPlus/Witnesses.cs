using System;
using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;

namespace BetterChasesPlus;

public class Witnesses : Script
{
	public class Witness
	{
		public Ped Ped;

		public float distance;

		public double Spotted = 0.0;

		public double VehicleSpotted = 0.0;

		public double Recognition = 0.0;

		public double VehicleRecognition = 0.0;

		public Vector3 LastKnownLocation;
	}

	private static readonly bool ShowBlips = false;

	private static readonly int MaxCitizenWitnesses = 0;

	private static readonly int MaxCopWitnesses = 6;

	private static float MaxWitnessDistance = 0f;

	private static float vehicleSize = 0f;

	public static List<Witness> Citizens = new List<Witness>();

	public static List<Witness> Cops = new List<Witness>();

	public Witnesses()
	{
		base.Tick += OnTick;
		base.Aborted += Shutdown;
		base.Interval = 200;
	}

	private void OnTick(object sender, EventArgs e)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0892: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_082b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0836: Unknown result type (might be due to invalid IL or missing references)
		//IL_083d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		if (!ArrestWarrants.CopSearch && !BetterChases.CopSearch)
		{
			Citizens.Clear();
			Cops.Clear();
			return;
		}
		Ped Character = Game.Player.Character;
		Vehicle currentVehicle = Game.Player.Character.CurrentVehicle;
		int wantedLevel = Game.Player.Wanted.WantedLevel;
		vehicleSize = 0f;
		MaxWitnessDistance = 60f;
		if (Helpers.IsValid((Entity)(object)currentVehicle))
		{
			vehicleSize = 500f;
			MaxWitnessDistance = Math.Min(60f, vehicleSize * MaxWitnessDistance / 4f);
		}
		Model model;
		for (int num = Citizens.Count - 1; num >= 0; num--)
		{
			Witness witness = Citizens[num];
			if (!((Entity)witness.Ped).IsAlive)
			{
				Citizens.Remove(witness);
			}
			else
			{
				RaycastResult val = default(RaycastResult);
				if (Function.Call<bool>(unchecked((Hash)(-2948087700947445085L)), (InputArgument[])(object)new InputArgument[3]
				{
					(Entity)((Entity)(object)witness.Ped),
					(Entity)((Entity)(object)Character),
					(InputArgument)MaxWitnessDistance
				}))
				{
					if (Helpers.IsValid((Entity)(object)witness.Ped.CurrentVehicle))
					{
						model = ((Entity)witness.Ped.CurrentVehicle).Model;
						val = ((!model.IsHelicopter) ? World.Raycast(((Entity)witness.Ped.CurrentVehicle).Position + new Vector3(0f, 0f, 11f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null) : World.Raycast(((Entity)witness.Ped.CurrentVehicle).Position + new Vector3(0f, 0f, -10f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null));
					}
					else
					{
						val = World.Raycast(((Entity)witness.Ped).Position + new Vector3(0f, 0f, 5f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null);
					}
				}
				if (val.HitEntity != (Entity)null && ((Helpers.IsValid((Entity)(object)Character.CurrentVehicle) && ((PoolObject)val.HitEntity).Handle == ((PoolObject)Character.CurrentVehicle).Handle) || ((PoolObject)val.HitEntity).Handle == ((PoolObject)Character).Handle))
				{
					GetRecognitionPercent(witness.Ped, Character, witness);
				}
				else
				{
					witness.Recognition = 0.0;
					witness.VehicleRecognition = 0.0;
				}
				CalcSpotted(witness);
				if (Math.Max(witness.Spotted, witness.VehicleSpotted) <= 0.0)
				{
					Citizens.Remove(witness);
				}
			}
		}
		for (int num2 = Cops.Count - 1; num2 >= 0; num2--)
		{
			Witness witness2 = Cops[num2];
			if (!((Entity)witness2.Ped).IsAlive)
			{
				Cops.Remove(witness2);
			}
			else
			{
				RaycastResult val2 = default(RaycastResult);
				if (Function.Call<bool>(unchecked((Hash)(-2948087700947445085L)), (InputArgument[])(object)new InputArgument[3]
				{
					(Entity)((Entity)(object)witness2.Ped),
					(Entity)((Entity)(object)Character),
					(InputArgument)MaxWitnessDistance
				}))
				{
					if (Helpers.IsValid((Entity)(object)witness2.Ped.CurrentVehicle))
					{
						model = ((Entity)witness2.Ped.CurrentVehicle).Model;
						val2 = ((!model.IsHelicopter) ? World.Raycast(((Entity)witness2.Ped.CurrentVehicle).Position + new Vector3(0f, 0f, 11f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null) : World.Raycast(((Entity)witness2.Ped.CurrentVehicle).Position + new Vector3(0f, 0f, -10f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null));
					}
					else
					{
						val2 = World.Raycast(((Entity)witness2.Ped).Position + new Vector3(0f, 0f, 5f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null);
					}
				}
				if (val2.HitEntity != (Entity)null && ((Helpers.IsValid((Entity)(object)Character.CurrentVehicle) && ((PoolObject)val2.HitEntity).Handle == ((PoolObject)Character.CurrentVehicle).Handle) || ((PoolObject)val2.HitEntity).Handle == ((PoolObject)Character).Handle))
				{
					GetRecognitionPercent(witness2.Ped, Character, witness2);
				}
				else
				{
					witness2.Recognition = 0.0;
					witness2.VehicleRecognition = 0.0;
				}
				CalcSpotted(witness2);
				if (Math.Max(witness2.Spotted, witness2.VehicleSpotted) <= 0.0)
				{
					Cops.Remove(witness2);
				}
			}
		}
		List<Ped> list = new List<Ped>();
		if (Citizens.Count >= MaxCitizenWitnesses && Cops.Count >= MaxCopWitnesses)
		{
			return;
		}
		list = new List<Ped>(World.GetNearbyPeds(Character, MaxWitnessDistance, Array.Empty<Model>()));
		list = list.OrderByDescending(delegate(Ped Ped)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			Vector3 val4 = ((Entity)Character).Position - ((Entity)Ped).Position;
			return val4.Length();
		}).ToList();
		foreach (Ped ped in list)
		{
			if (Citizens.Count >= MaxCitizenWitnesses && Cops.Count >= MaxCopWitnesses)
			{
				break;
			}
			if (!((Entity)ped).IsAlive || !ped.IsHuman || Citizens.FindIndex((Witness Citizen) => ((PoolObject)Citizen.Ped).Handle == ((PoolObject)ped).Handle) > -1 || Cops.FindIndex((Witness Cop) => ((PoolObject)Cop.Ped).Handle == ((PoolObject)ped).Handle) > -1)
			{
				continue;
			}
			bool flag = Enumerable.Contains(Helpers.PedCopTypes, Function.Call<int>(unchecked((Hash)(-70476366192974276L)), (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)ped) }));
			if ((!flag && Citizens.Count >= MaxCitizenWitnesses) || (flag && Cops.Count >= MaxCopWitnesses) || !flag)
			{
				continue;
			}
			RaycastResult val3 = default(RaycastResult);
			if (Function.Call<bool>(unchecked((Hash)(-2948087700947445085L)), (InputArgument[])(object)new InputArgument[3]
			{
				(Entity)((Entity)(object)ped),
				(Entity)((Entity)(object)Character),
				(InputArgument)MaxWitnessDistance
			}))
			{
				if (Helpers.IsValid((Entity)(object)ped.CurrentVehicle))
				{
					model = ((Entity)ped.CurrentVehicle).Model;
					val3 = ((!model.IsHelicopter) ? World.Raycast(((Entity)ped.CurrentVehicle).Position + new Vector3(0f, 0f, 11f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null) : World.Raycast(((Entity)ped.CurrentVehicle).Position + new Vector3(0f, 0f, -10f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null));
				}
				else
				{
					val3 = World.Raycast(((Entity)ped).Position + new Vector3(0f, 0f, 5f), ((Entity)Character).Position, unchecked((IntersectFlags)(-1)), (Entity)null);
				}
			}
			if (!(val3.HitEntity != (Entity)null) || ((!Helpers.IsValid((Entity)(object)Character.CurrentVehicle) || ((PoolObject)val3.HitEntity).Handle != ((PoolObject)Character.CurrentVehicle).Handle) && ((PoolObject)val3.HitEntity).Handle != ((PoolObject)Character).Handle))
			{
				continue;
			}
			if (flag)
			{
				Witness item = new Witness
				{
					Ped = ped
				};
				Cops.Add(item);
				if (ShowBlips)
				{
					((Entity)ped).AddBlip();
				}
			}
			else
			{
				Witness item2 = new Witness
				{
					Ped = ped
				};
				Citizens.Add(item2);
				if (ShowBlips)
				{
					((Entity)ped).AddBlip();
				}
			}
		}
	}

	public static double GetMaxRecognition(List<Witness> witnesses, bool vehicle = false)
	{
		double num = 0.0;
		foreach (Witness witness in witnesses)
		{
			num = ((!vehicle) ? Math.Max(num, witness.Recognition) : Math.Max(num, witness.VehicleRecognition));
		}
		return num;
	}

	private void GetRecognitionPercent(Ped ped, Ped ped2, Witness witness)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ((Entity)ped2).Velocity;
		float num = val.Length();
		val = ((Entity)ped).Velocity;
		double num2 = Math.Round(Math.Abs(num - val.Length()), 0);
		val = ((Entity)ped2).Position - ((Entity)ped).Position;
		float num3 = (witness.distance = val.Length());
		witness.Recognition = 0.0;
		witness.VehicleRecognition = 0.0;
		if (Helpers.IsValid((Entity)(object)ped2.CurrentVehicle))
		{
			double val2 = ((num3 <= MaxWitnessDistance / 6f) ? 100.0 : ((num3 < MaxWitnessDistance) ? Math.Round(Math.Abs(((num3 - 30f) / (MaxWitnessDistance - MaxWitnessDistance / 6f) - 1f) * 100f), 0) : 0.0));
			double val3 = ((num2 <= 8.0) ? 100.0 : ((num2 < 50.0) ? Math.Round(Math.Abs(((num2 - 8.0) / 42.0 - 1.0) * 100.0), 0) : 0.0));
			witness.VehicleRecognition = Math.Min(val2, val3);
			if (CanBeIdentified(ped2) || num3 <= 3f)
			{
				val2 = ((num3 <= 8f) ? 100.0 : ((num3 < 30f) ? Math.Round(Math.Abs(((num3 - 8f) / 22f - 1f) * 100f), 0) : 0.0));
				val3 = ((num2 <= 4.0) ? 100.0 : ((num2 < 15.0) ? Math.Round(Math.Abs(((num2 - 4.0) / 11.0 - 1.0) * 100.0), 0) : 0.0));
				witness.Recognition = Math.Min(val2, val3);
			}
		}
		else if (CanBeIdentified(ped2))
		{
			double val2 = ((num3 <= 15f) ? 100.0 : ((num3 < 60f) ? Math.Round(Math.Abs(((num3 - 15f) / 45f - 1f) * 100f), 0) : 0.0));
			double val3 = ((num2 <= 4.0) ? 100.0 : ((num2 < 15.0) ? Math.Round(Math.Abs(((num2 - 4.0) / 11.0 - 1.0) * 100.0), 0) : 0.0));
			witness.Recognition = Math.Min(val2, val3);
		}
	}

	private void CalcSpotted(Witness witness)
	{
		float num = 1f;
		if (Config.Options.ArrestWarrants.Enabled)
		{
			num = (float)Config.Options.ArrestWarrants.SpotSpeed / 100f;
		}
		if (ArrestWarrants.IsWarrantSearchActive)
		{
			num = ArrestWarrants.PedMatch * num;
		}
		if (witness.Recognition >= 0.0 && witness.Recognition <= 25.0)
		{
			witness.Spotted += -1f * num;
		}
		else if (witness.Recognition > 25.0 && witness.Recognition < 50.0)
		{
			witness.Spotted += -0.25 * (double)num;
		}
		else if (witness.Recognition >= 50.0 && witness.Recognition < 75.0)
		{
			if (witness.Spotted < 20.0)
			{
				witness.Spotted += 0.25 * (double)num;
			}
		}
		else if (witness.Recognition >= 75.0 && witness.Recognition < 100.0)
		{
			witness.Spotted += 0.5 * (double)num;
		}
		else if (witness.Recognition >= 100.0)
		{
			witness.Spotted += 5f * num;
		}
		if (witness.Spotted > 100.0)
		{
			witness.Spotted = 100.0;
		}
		if (witness.Spotted < 0.0)
		{
			witness.Spotted = 0.0;
		}
		num = 1f;
		if (Config.Options.ArrestWarrants.Enabled)
		{
			num = (float)Config.Options.ArrestWarrants.SpotSpeed / 100f;
		}
		if (ArrestWarrants.IsWarrantSearchActive)
		{
			num = ArrestWarrants.VehicleMatch * num;
		}
		if (witness.VehicleRecognition >= 0.0 && witness.VehicleRecognition <= 25.0)
		{
			witness.VehicleSpotted += -1f * num;
		}
		else if (witness.VehicleRecognition > 25.0 && witness.VehicleRecognition < 50.0)
		{
			witness.VehicleSpotted += -0.25 * (double)num;
		}
		else if (witness.VehicleRecognition >= 50.0 && witness.VehicleRecognition < 75.0)
		{
			if (witness.VehicleSpotted < 20.0)
			{
				witness.VehicleSpotted += 0.25 * (double)num;
			}
		}
		else if (witness.VehicleRecognition >= 75.0 && witness.VehicleRecognition < 100.0)
		{
			witness.VehicleSpotted += 0.5 * (double)num;
		}
		else if (witness.VehicleRecognition >= 100.0)
		{
			witness.VehicleSpotted += 5f * num;
		}
		if (witness.VehicleSpotted > 100.0)
		{
			witness.VehicleSpotted = 100.0;
		}
		if (witness.VehicleSpotted < 0.0)
		{
			witness.VehicleSpotted = 0.0;
		}
	}

	private bool CanBeIdentified(Ped ped)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Invalid comparison between Unknown and I4
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Invalid comparison between Unknown and I4
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Invalid comparison between Unknown and I4
		if (Helpers.IsValid((Entity)(object)ped.CurrentVehicle))
		{
			Vehicle currentVehicle = ped.CurrentVehicle;
			if (((Entity)currentVehicle).Model == (Model)((VehicleHash)782665360))
			{
				return false;
			}
			Model model = ((Entity)currentVehicle).Model;
			if (!model.IsBike)
			{
				model = ((Entity)currentVehicle).Model;
				if (!model.IsBicycle)
				{
					model = ((Entity)currentVehicle).Model;
					if (!model.IsQuadBike && (!currentVehicle.HasRoof || (int)currentVehicle.RoofState != 2))
					{
						if (currentVehicle.Windows.AllWindowsIntact && (int)Function.Call<VehicleWindowTint>((Hash)1072440087508450453L, (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)currentVehicle) }) != 0 && (int)Function.Call<VehicleWindowTint>((Hash)1072440087508450453L, (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)currentVehicle) }) != 4 && (int)Function.Call<VehicleWindowTint>((Hash)1072440087508450453L, (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)currentVehicle) }) != -1)
						{
							return false;
						}
						if (Game.IsControlPressed((Control)73))
						{
							return false;
						}
						goto IL_0120;
					}
				}
			}
			return true;
		}
		goto IL_0120;
		IL_0120:
		return true;
	}

	public static Vector3 GetDimensions(Model model)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		return Vector3.Subtract(model.Dimensions.Item2, model.Dimensions.Item1);
	}

	public static float GetVolume(Vector3 dimensions)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return dimensions.X * dimensions.Y * dimensions.Z;
	}

	private void Shutdown(object sender, EventArgs e)
	{
	}
}
