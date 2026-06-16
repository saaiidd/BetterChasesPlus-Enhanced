using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GTA;
using GTA.Math;
using GTA.Native;

namespace BetterChasesPlus;

public class Helpers
{
	private static int wantedLevel = 0;

	private static int maxWantedLevel = 5;

	public static int[] PedCopTypes = new int[2] { 6, 27 };

	public static WeaponHash[] NotWeapons;

	public static int WantedLevel
	{
		get
		{
			if (Config.Options.BetterChases.WantedLevelControl != "Full")
			{
				return Game.Player.Wanted.WantedLevel;
			}
			return wantedLevel;
		}
		set
		{
			if (Config.Options.BetterChases.WantedLevelControl != "Full")
			{
				EnhancedWanted.Set(value);
				return;
			}
			if (value > maxWantedLevel)
			{
				MaxWantedLevel = value;
			}
			wantedLevel = value;
			EnhancedWanted.Set(value);
		}
	}

	public static int MaxWantedLevel
	{
		get
		{
			return maxWantedLevel;
		}
		set
		{
			maxWantedLevel = value;
			Function.Call(unchecked((Hash)(-6170209823631145799L)), (InputArgument[])(object)new InputArgument[1] { (InputArgument)value });
		}
	}

	public static int MinGroundUnits
	{
		get
		{
			int result = 0;
			switch (wantedLevel)
			{
			case 1:
				result = Config.Options.BetterChases.CopDispatch.OneStar.GroundMin;
				break;
			case 2:
				result = Config.Options.BetterChases.CopDispatch.TwoStar.GroundMin;
				break;
			case 3:
				result = Config.Options.BetterChases.CopDispatch.ThreeStar.GroundMin;
				break;
			case 4:
				result = Config.Options.BetterChases.CopDispatch.FourStar.GroundMin;
				break;
			case 5:
				result = Config.Options.BetterChases.CopDispatch.FiveStar.GroundMin;
				break;
			}
			return result;
		}
	}

	public static int MaxGroundUnits
	{
		get
		{
			int result = 0;
			switch (wantedLevel)
			{
			case 1:
				result = Config.Options.BetterChases.CopDispatch.OneStar.GroundMax;
				break;
			case 2:
				result = Config.Options.BetterChases.CopDispatch.TwoStar.GroundMax;
				break;
			case 3:
				result = Config.Options.BetterChases.CopDispatch.ThreeStar.GroundMax;
				break;
			case 4:
				result = Config.Options.BetterChases.CopDispatch.FourStar.GroundMax;
				break;
			case 5:
				result = Config.Options.BetterChases.CopDispatch.FiveStar.GroundMax;
				break;
			}
			return result;
		}
	}

	public static int MinAirUnits
	{
		get
		{
			int result = 0;
			switch (wantedLevel)
			{
			case 1:
				result = Config.Options.BetterChases.CopDispatch.OneStar.AirMin;
				break;
			case 2:
				result = Config.Options.BetterChases.CopDispatch.TwoStar.AirMin;
				break;
			case 3:
				result = Config.Options.BetterChases.CopDispatch.ThreeStar.AirMin;
				break;
			case 4:
				result = Config.Options.BetterChases.CopDispatch.FourStar.AirMin;
				break;
			case 5:
				result = Config.Options.BetterChases.CopDispatch.FiveStar.AirMin;
				break;
			}
			return result;
		}
	}

	public static bool IsArmed => Game.Player.Character.Weapons.Current != null && !Enumerable.Contains(NotWeapons, Game.Player.Character.Weapons.Current.Hash) && Function.Call<bool>((Hash)5140692576702410007L, (InputArgument[])(object)new InputArgument[2]
	{
		(Entity)((Entity)(object)Game.Player.Character),
		(InputArgument)7
	});

	public static bool IsValid(Entity entity)
	{
		return entity != (Entity)null && ((PoolObject)entity).Exists();
	}

	public static bool IsThisEntityAheadThatEntity(Entity ent1, Entity ent2)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ent1.Position;
		return Function.Call<Vector3>((Hash)2482816124249826099L, (InputArgument[])(object)new InputArgument[4]
		{
			(Entity)ent2,
			(InputArgument)position.X,
			(InputArgument)position.Y,
			(InputArgument)position.Z
		}).Y > 0f;
	}

	public static bool IsAnyTireBlown(Vehicle vehicle)
	{
		return Function.Call<bool>(unchecked((Hash)(-5032464408400274263L)), (InputArgument[])(object)new InputArgument[3]
		{
			(Entity)((Entity)(object)vehicle),
			(InputArgument)0,
			(InputArgument)false
		}) || Function.Call<bool>(unchecked((Hash)(-5032464408400274263L)), (InputArgument[])(object)new InputArgument[3]
		{
			(Entity)((Entity)(object)vehicle),
			(InputArgument)1,
			(InputArgument)false
		}) || Function.Call<bool>(unchecked((Hash)(-5032464408400274263L)), (InputArgument[])(object)new InputArgument[3]
		{
			(Entity)((Entity)(object)vehicle),
			(InputArgument)2,
			(InputArgument)false
		}) || Function.Call<bool>(unchecked((Hash)(-5032464408400274263L)), (InputArgument[])(object)new InputArgument[3]
		{
			(Entity)((Entity)(object)vehicle),
			(InputArgument)3,
			(InputArgument)false
		}) || Function.Call<bool>(unchecked((Hash)(-5032464408400274263L)), (InputArgument[])(object)new InputArgument[3]
		{
			(Entity)((Entity)(object)vehicle),
			(InputArgument)4,
			(InputArgument)false
		}) || Function.Call<bool>(unchecked((Hash)(-5032464408400274263L)), (InputArgument[])(object)new InputArgument[3]
		{
			(Entity)((Entity)(object)vehicle),
			(InputArgument)5,
			(InputArgument)false
		});
	}

	public static bool IsCopNearby(Vector3 pos, float radius)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		return Function.Call<bool>((Hash)1651774575515508574L, (InputArgument[])(object)new InputArgument[6]
		{
			(InputArgument)(pos.X + radius),
			(InputArgument)(pos.Y + radius),
			(InputArgument)(pos.Z + radius),
			(InputArgument)(pos.X - radius),
			(InputArgument)(pos.Y - radius),
			(InputArgument)(pos.Z - radius)
		});
	}

	public static bool IsPopulatedArea(Vector3 position, float range, int threshold = 8)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		bool result = false;
		Ped[] nearbyPeds = World.GetNearbyPeds(position, range, Array.Empty<Model>());
		Ped[] array = nearbyPeds;
		foreach (Ped val in array)
		{
			if (num < threshold)
			{
				if (!val.IsPlayer && val.IsHuman && ((Entity)val).IsAlive && !Enumerable.Contains(PedCopTypes, Function.Call<int>(unchecked((Hash)(-70476366192974276L)), (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)val) })))
				{
					num++;
				}
				continue;
			}
			result = true;
			break;
		}
		return result;
	}

	public static bool IsSilenced(Weapon weapon)
	{
		return false;
	}

	public static bool IsWearingHelmet(Ped ped)
	{
		if (Function.Call<bool>(unchecked((Hash)(-919895538802844903L)), (InputArgument[])(object)new InputArgument[1] { (Entity)((Entity)(object)ped) }))
		{
			return true;
		}
		return false;
	}

	public static bool IsMasked(Ped ped)
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int> { 8, 9, 10, 11, 12, 13, 14 };
		List<int> list2 = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
		List<int> list3 = new List<int> { 14, 15, 16, 17, 18, 19, 20 };
		Model model = ((Entity)ped).Model;
		if ((model == (Model)"mp_f_freemode_01" || model == (Model)"mp_m_freemode_01") && Function.Call<int>((Hash)7490462606036423932L, (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)Game.Player.Character),
			(InputArgument)1
		}) != 0)
		{
			return true;
		}
		if (model == (Model)(unchecked((PedHash)(-1692214353))) && list.Contains(Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)ped),
			(InputArgument)0
		})))
		{
			return true;
		}
		if (model == (Model)((PedHash)225514697) && list2.Contains(Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)ped),
			(InputArgument)0
		})))
		{
			return true;
		}
		if (model == (Model)(unchecked((PedHash)(-1686040670))) && list3.Contains(Function.Call<int>(unchecked((Hash)(-8535233825580798760L)), (InputArgument[])(object)new InputArgument[2]
		{
			(Entity)((Entity)(object)ped),
			(InputArgument)0
		})))
		{
			return true;
		}
		return false;
	}

	public static Model GetModel(int hash)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Model(hash);
	}

	static Helpers()
	{
		WeaponHash[] array = new WeaponHash[20];
		array = new WeaponHash[] { (WeaponHash)0xA2719263u, (WeaponHash)0xF9E6AA4Bu, (WeaponHash)0x23C9F95Cu, (WeaponHash)0x84BD7BFDu, (WeaponHash)0x60EC506u, (WeaponHash)0x7F7497E5u, (WeaponHash)0x497FACC3u, (WeaponHash)0x47757124u, (WeaponHash)0x8BB05FD7u, (WeaponHash)0x440E4788u, (WeaponHash)0x4E875F73u, (WeaponHash)0xD8DF3C3Cu, (WeaponHash)0x678B81B1u, (WeaponHash)0xA720365Cu, (WeaponHash)0xFBAB5776u, (WeaponHash)0x34A67B97u, (WeaponHash)0x94117305u, (WeaponHash)0xFDBC8A50u, (WeaponHash)0x787F0BBu, (WeaponHash)0x19044EE0u };
		NotWeapons = (WeaponHash[])(object)array;
	}
}
