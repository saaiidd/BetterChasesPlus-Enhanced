using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using GTA;
using NativeUI;

namespace BetterChasesPlus;

public class Config : Script
{
	public class GlobalConfig
	{
		public BetterChasesConfig BetterChases = new BetterChasesConfig();

		public ArrestWarrantsConfig ArrestWarrants = new ArrestWarrantsConfig();

		public Keys MenuKey { get; set; } = (Keys)118;

		public Keys SurrenderKey { get; set; } = (Keys)69;

		public GTA.Control SurrenderButton { get; set; } = (GTA.Control)44;

		public bool DisplayHints { get; set; } = true;
	}

	public class BetterChasesConfig
	{
		public ChaseEscalatesConfig ChaseEscalates = new ChaseEscalatesConfig();

		public CopDispatchConfig CopDispatch = new CopDispatchConfig();

		public CrimesConfig Crimes = new CrimesConfig();

		public bool Enabled { get; set; } = true;

		public string WantedLevelControl { get; set; } = "Full";

		public bool CopsManageTraffic { get; set; } = true;

		public bool WreckedCopsStopChasing { get; set; } = true;

		public bool DisallowCopCommandeering { get; set; } = true;

		public bool RequirePITAuthorization { get; set; } = true;

		public bool RequireLethalForceAuthorization { get; set; } = true;

		public bool AllowBustOpportunity { get; set; } = true;

		public bool ShowHUD { get; set; } = true;

		public bool ShowNotifications { get; set; } = true;

		public bool ShowBigMessages { get; set; } = true;

		public int IconOffsetX { get; set; } = 0;

		public int IconOffsetY { get; set; } = 0;
	}

	public class ArrestWarrantsConfig
	{
		public WarrantLengthsConfig WarrantLenghts = new WarrantLengthsConfig();

		public bool Enabled { get; set; } = true;

		public int SpotSpeed { get; set; } = 100;

		public bool RememberChase { get; set; } = true;

		public bool ShowSpottedMeter { get; set; } = true;

		public bool ShowSpottedIndicators { get; set; } = true;

		public bool ShowHUD { get; set; } = true;

		public bool ShowNotifications { get; set; } = true;

		public bool ShowBigMessages { get; set; } = true;

		public int IconOffsetX { get; set; } = 0;

		public int IconOffsetY { get; set; } = 0;

		public int TextOffsetX { get; set; } = 0;

		public int TextOffsetY { get; set; } = 0;

		public int GradientOffsetX { get; set; } = 0;

		public int GradientOffsetY { get; set; } = 0;
	}

	public class ChaseEscalatesConfig
	{
		public ChaseEscalatesPhaseConfig PhaseOne = new ChaseEscalatesPhaseConfig
		{
			Enabled = true,
			Length = 30,
			RequestBackup = true
		};

		public ChaseEscalatesPhaseConfig PhaseTwo = new ChaseEscalatesPhaseConfig
		{
			Enabled = true,
			Length = 30,
			RequestBackup = true
		};

		public ChaseEscalatesPhaseConfig PhaseThree = new ChaseEscalatesPhaseConfig
		{
			Enabled = true,
			Length = 30,
			RequestBackup = true
		};

		public ChaseEscalatesPhaseConfig PhaseFour = new ChaseEscalatesPhaseConfig
		{
			Enabled = true,
			Length = 30,
			WantedLevel = 3,
			PITAuthorized = true
		};

		public bool Enabled { get; set; } = true;
	}

	public class ChaseEscalatesPhaseConfig
	{
		public bool Enabled { get; set; }

		public int Length { get; set; }

		public int WantedLevel { get; set; }

		public bool PITAuthorized { get; set; }

		public bool LethalForceAuthorized { get; set; }

		public bool RequestBackup { get; set; }
	}

	public class WarrantLengthsConfig
	{
		public int OneStar { get; set; } = 6;

		public int TwoStar { get; set; } = 18;

		public int ThreeStar { get; set; } = 24;

		public int FourStar { get; set; } = 48;

		public int FiveStar { get; set; } = 72;
	}

	public class CopDispatchConfig
	{
		public CopDispatchStarConfig OneStar = new CopDispatchStarConfig
		{
			GroundMin = 1,
			GroundMax = 2
		};

		public CopDispatchStarConfig TwoStar = new CopDispatchStarConfig
		{
			GroundMin = 2,
			GroundMax = 3
		};

		public CopDispatchStarConfig ThreeStar = new CopDispatchStarConfig
		{
			GroundMin = 3,
			GroundMax = 4,
			AirMin = 0
		};

		public CopDispatchStarConfig FourStar = new CopDispatchStarConfig
		{
			GroundMin = 4,
			GroundMax = 6,
			AirMin = 1,
			PITAuthorized = true
		};

		public CopDispatchStarConfig FiveStar = new CopDispatchStarConfig
		{
			GroundMin = -1,
			AirMin = -1,
			PITAuthorized = true,
			LethalForceAuthorized = true
		};

		public bool Enabled { get; set; } = true;
	}

	public class CopDispatchStarConfig
	{
		public int GroundMin { get; set; }

		public int AirMin { get; set; }

		public int GroundMax { get; set; }

		public bool PITAuthorized { get; set; }

		public bool LethalForceAuthorized { get; set; }
	}

	public class CrimesConfig
	{
		public CrimeConfig GTA = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 2,
			PITAuthorized = false,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 20,
			RequestBackup = true
		};

		public CrimeConfig Stolen = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 2,
			PITAuthorized = false,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 40,
			RequestBackup = true
		};

		public CrimeConfig Speeding = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 2,
			PITAuthorized = false,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 40,
			RequestBackup = true,
			Speed = 35
		};

		public CrimeConfig Reckless = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 3,
			PITAuthorized = false,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 20,
			RequestBackup = true,
			Speed = 15
		};

		public CrimeConfig Armed = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 4,
			PITAuthorized = false,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 40,
			WantedLevel = 2,
			RequestBackup = true
		};

		public CrimeConfig Aiming = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 4,
			PITAuthorized = true,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 40,
			WantedLevel = 3,
			RequestBackup = true
		};

		public CrimeConfig Assault = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 4,
			PITAuthorized = true,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 30,
			WantedLevel = 3,
			RequestBackup = true
		};

		public CrimeConfig PoliceAssault = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 0,
			PITAuthorized = true,
			LethalForceAuthorized = false,
			PoliceWitnessThreshold = 10,
			WantedLevel = 3,
			RequestBackup = true
		};

		public CrimeConfig Shooting = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 4,
			PITAuthorized = true,
			LethalForceAuthorized = true,
			PoliceWitnessThreshold = 10,
			WantedLevel = 4
		};

		public CrimeConfig Murder = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 0,
			PITAuthorized = true,
			LethalForceAuthorized = true,
			PoliceWitnessThreshold = 40,
			WantedLevel = 4,
			RequestBackup = true
		};

		public CrimeConfig PoliceMurder = new CrimeConfig
		{
			Enabled = true,
			MaxWantedLevel = 0,
			PITAuthorized = true,
			LethalForceAuthorized = true,
			PoliceWitnessThreshold = 10,
			WantedLevel = 5
		};
	}

	public class CrimeConfig
	{
		public bool Enabled { get; set; }

		public int MaxWantedLevel { get; set; }

		public bool PITAuthorized { get; set; }

		public bool LethalForceAuthorized { get; set; }

		public int PoliceWitnessThreshold { get; set; }

		public int WantedLevel { get; set; }

		public bool RequestBackup { get; set; }

		public int Speed { get; set; }
	}

	private class IngameMenu
	{
		private class Menu
		{
			public delegate void commandDelegate();

			public UIMenu menu;

			public UIMenuItem menuItem;

			public List<Menu> subMenus;

			public object parent;

			public string name;

			public commandDelegate command;

			public bool toggle;

			public dynamic value
			{
				get
				{
					return parent.GetType().GetProperty(name).GetValue(parent, null);
				}
				set
				{
					parent.GetType().GetProperty(name).SetValue(parent, value);
				}
			}
		}

		public Keys MenuKey = Options.MenuKey;

		public MenuPool MainPool = new MenuPool();

		public UIMenu MainMenu = new UIMenu("Better Chases+", "Configure the Global Options.");

		private List<Menu> Menus = new List<Menu>
		{
			new Menu
			{
				menu = new UIMenu("Better Chases", "Configure Better Chases"),
				menuItem = new UIMenuItem("Better Chases", "Configure Better Chases"),
				subMenus = new List<Menu>
				{
					new Menu
					{
						parent = Options.BetterChases,
						name = "Enabled",
						toggle = true,
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Module Enabled", Options.BetterChases.Enabled, "Toggles the functionality of the entire module. If disabled, all features below will be disabled also.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "WantedLevelControl",
						menuItem = (UIMenuItem)new UIMenuListItem("Wanted Level Control", (List<object>)WantedLevelControlOptions, WantedLevelControlOptions.IndexOf(Options.BetterChases.WantedLevelControl), "Full if you want this mod to take full control, passive for mod compatibility.")
					},
					new Menu
					{
						menu = new UIMenu("Chase Escalates", "Chase Escalates Over Time"),
						menuItem = new UIMenuItem("Chase Escalates", "Chase Escalates Over Time"),
						subMenus = new List<Menu>
						{
							new Menu
							{
								parent = Options.BetterChases.ChaseEscalates,
								name = "Enabled",
								toggle = true,
								menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.ChaseEscalates.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
							},
							new Menu
							{
								menu = new UIMenu("Phase One", "Phase One Chase Escalation"),
								menuItem = new UIMenuItem("Phase One", "Phase One Chase Escalation"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseOne,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.ChaseEscalates.PhaseOne.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseOne,
										name = "Length",
										menuItem = (UIMenuItem)new UIMenuListItem("Chase Time", (List<object>)ChaseLengthOptions, ChaseLengthOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseOne.Length), "How long in in-game minutes until below take affect.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseOne,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.ChaseEscalates.PhaseOne.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseOne,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.ChaseEscalates.PhaseOne.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseOne,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.ChaseEscalates.PhaseOne.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseOne,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseOne.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Phase Two", "Phase Two Chase Escalation"),
								menuItem = new UIMenuItem("Phase Two", "Phase Two Chase Escalation"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseTwo,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.ChaseEscalates.PhaseTwo.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseTwo,
										name = "Length",
										menuItem = (UIMenuItem)new UIMenuListItem("Chase Time", (List<object>)ChaseLengthOptions, ChaseLengthOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseTwo.Length), "How long in in-game minutes until below take affect.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseTwo,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.ChaseEscalates.PhaseTwo.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseTwo,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.ChaseEscalates.PhaseTwo.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseTwo,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.ChaseEscalates.PhaseTwo.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseTwo,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseTwo.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Phase Three", "Phase Three Chase Escalation"),
								menuItem = new UIMenuItem("Phase Three", "Phase Three Chase Escalation"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseThree,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.ChaseEscalates.PhaseThree.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseThree,
										name = "Length",
										menuItem = (UIMenuItem)new UIMenuListItem("Chase Time", (List<object>)ChaseLengthOptions, ChaseLengthOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseThree.Length), "How long in in-game minutes until below take affect.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseThree,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.ChaseEscalates.PhaseThree.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseThree,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.ChaseEscalates.PhaseThree.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseThree,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.ChaseEscalates.PhaseThree.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseThree,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseThree.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Phase Four", "Phase Four Chase Escalation"),
								menuItem = new UIMenuItem("Phase Four", "Phase Four Chase Escalation"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseFour,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.ChaseEscalates.PhaseFour.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseFour,
										name = "Length",
										menuItem = (UIMenuItem)new UIMenuListItem("Chase Time", (List<object>)ChaseLengthOptions, ChaseLengthOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseFour.Length), "How long in in-game minutes until below take affect.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseFour,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.ChaseEscalates.PhaseFour.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseFour,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.ChaseEscalates.PhaseFour.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseFour,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.ChaseEscalates.PhaseFour.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.ChaseEscalates.PhaseFour,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.ChaseEscalates.PhaseFour.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							}
						}
					},
					new Menu
					{
						menu = new UIMenu("Dispatch Control", "Control Cop Vehicle Dispatching"),
						menuItem = new UIMenuItem("Dispatch Control", "Control Cop Vehicle Dispatching"),
						subMenus = new List<Menu>
						{
							new Menu
							{
								parent = Options.BetterChases.CopDispatch,
								name = "Enabled",
								toggle = true,
								menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.CopDispatch.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
							},
							new Menu
							{
								menu = new UIMenu("One Star", "One Star Configuration"),
								menuItem = new UIMenuItem("One Star", "One Star Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.OneStar,
										name = "GroundMin",
										menuItem = (UIMenuItem)new UIMenuListItem("One Star Ground Min Limit", new List<object> { 1, 2 }, new List<object> { 1, 2 }.IndexOf(Options.BetterChases.CopDispatch.OneStar.GroundMin), "Minimum ground cop vehicles that will respond to a one star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.OneStar,
										name = "GroundMax",
										menuItem = (UIMenuItem)new UIMenuListItem("One Star Ground Max Limit", new List<object> { 1, 2, 3 }, new List<object> { 1, 2, 3 }.IndexOf(Options.BetterChases.CopDispatch.OneStar.GroundMax), "Maximum ground cop vehicles allowed to respond to a one star wanted level. Additional backup will increase the wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.OneStar,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.CopDispatch.OneStar.PITAuthorized, "Allow police to PIT once this wanted level is reached.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.OneStar,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.CopDispatch.OneStar.LethalForceAuthorized, "Allow police to use lethal force once this wanted level is reached.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Two Star", "TwoStar Configuration"),
								menuItem = new UIMenuItem("Two Star", "Two Star Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.TwoStar,
										name = "GroundMin",
										menuItem = (UIMenuItem)new UIMenuListItem("Two Star Ground Min Limit", new List<object> { 2, 3 }, new List<object> { 2, 3 }.IndexOf(Options.BetterChases.CopDispatch.TwoStar.GroundMin), "Minimum ground cop vehicles that will respond to a two star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.TwoStar,
										name = "GroundMax",
										menuItem = (UIMenuItem)new UIMenuListItem("Two Star Ground Max Limit", new List<object> { 2, 3, 4 }, new List<object> { 2, 3, 4 }.IndexOf(Options.BetterChases.CopDispatch.TwoStar.GroundMax), "Maximum ground cop vehicles allowed to respond to a two star wanted level. Additional backup will increase the wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.TwoStar,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.CopDispatch.TwoStar.PITAuthorized, "Allow police to PIT once this wanted level is reached.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.TwoStar,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.CopDispatch.TwoStar.LethalForceAuthorized, "Allow police to use lethal force once this wanted level is reached.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Three Star", "Three Star Configuration"),
								menuItem = new UIMenuItem("Three Star", "Three Star Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.ThreeStar,
										name = "GroundMin",
										menuItem = (UIMenuItem)new UIMenuListItem("Three Star Ground Min Limit", new List<object> { 3, 4 }, new List<object> { 3, 4 }.IndexOf(Options.BetterChases.CopDispatch.ThreeStar.GroundMin), "Minimum ground cop vehicles that will respond to a three star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.ThreeStar,
										name = "GroundMax",
										menuItem = (UIMenuItem)new UIMenuListItem("Three Star Ground Max Limit", new List<object> { 3, 4, 5 }, new List<object> { 3, 4, 5 }.IndexOf(Options.BetterChases.CopDispatch.ThreeStar.GroundMax), "Maximum ground cop vehicles allowed to respond to a three star wanted level. Additional backup will increase the wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.ThreeStar,
										name = "AirMin",
										menuItem = (UIMenuItem)new UIMenuListItem("Three Star Air Min Limit", new List<object> { 0, 1 }, new List<object> { 0, 1 }.IndexOf(Options.BetterChases.CopDispatch.ThreeStar.AirMin), "Minimum air cop vehicles that will respond to a three star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.ThreeStar,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.CopDispatch.ThreeStar.PITAuthorized, "Allow police to PIT once this wanted level is reached.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.ThreeStar,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.CopDispatch.ThreeStar.LethalForceAuthorized, "Allow police to use lethal force once this wanted level is reached.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Four Star", "Four Star Configuration"),
								menuItem = new UIMenuItem("Four Star", "Four Star Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FourStar,
										name = "GroundMin",
										menuItem = (UIMenuItem)new UIMenuListItem("Four Star Ground Min Limit", new List<object> { 4, 5, 6 }, new List<object> { 4, 5, 6 }.IndexOf(Options.BetterChases.CopDispatch.FourStar.GroundMin), "Minimum ground cop vehicles that will respond to a four star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FourStar,
										name = "GroundMax",
										menuItem = (UIMenuItem)new UIMenuListItem("Four Star Ground Max Limit", new List<object> { 4, 5, 6, 7 }, new List<object> { 4, 5, 6, 7 }.IndexOf(Options.BetterChases.CopDispatch.FourStar.GroundMax), "Maximum ground cop vehicles allowed to respond to a four star wanted level. Additional backup will increase the wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FourStar,
										name = "AirMin",
										menuItem = (UIMenuItem)new UIMenuListItem("Four Star Air Min Limit", new List<object> { 0, 1, 2 }, new List<object> { 0, 1, 2 }.IndexOf(Options.BetterChases.CopDispatch.FourStar.AirMin), "Minimum air cop vehicles that will respond to a four star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FourStar,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.CopDispatch.FourStar.PITAuthorized, "Allow police to PIT once this wanted level is reached.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FourStar,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.CopDispatch.FourStar.LethalForceAuthorized, "Allow police to use lethal force once this wanted level is reached.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Five Star", "Five Star Configuration"),
								menuItem = new UIMenuItem("Five Star", "Five Star Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FiveStar,
										name = "GroundMin",
										menuItem = (UIMenuItem)new UIMenuListItem("Five Star Ground Min Limit", new List<object> { 5, 6, 7, 8 }, new List<object> { 5, 6, 7, 8 }.IndexOf(Options.BetterChases.CopDispatch.FiveStar.GroundMin), "Minimum ground cop vehicles that will respond to a five star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FiveStar,
										name = "AirMin",
										menuItem = (UIMenuItem)new UIMenuListItem("Five Star Air Min Limit", new List<object> { 0, 1, 2, 3 }, new List<object> { 0, 1, 2, 3 }.IndexOf(Options.BetterChases.CopDispatch.FiveStar.AirMin), "Minimum air cop vehicles that will respond to a five star wanted level.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FiveStar,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.CopDispatch.FiveStar.PITAuthorized, "Allow police to PIT once this wanted level is reached.")
									},
									new Menu
									{
										parent = Options.BetterChases.CopDispatch.FiveStar,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.CopDispatch.FiveStar.LethalForceAuthorized, "Allow police to use lethal force once this wanted level is reached.")
									}
								}
							}
						}
					},
					new Menu
					{
						menu = new UIMenu("Crimes Control", "Control Crimes During Chases"),
						menuItem = new UIMenuItem("Crimes Control", "Control Crimes During Chases"),
						subMenus = new List<Menu>
						{
							new Menu
							{
								menu = new UIMenu("Grand Theft Auto", "Grand Theft Auto Configuration"),
								menuItem = new UIMenuItem("Grand Theft Auto", "Grand Theft Auto Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.GTA,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.GTA.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.GTA,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.GTA.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.GTA,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.GTA.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.GTA,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.GTA.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.GTA,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.GTA.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.GTA,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.GTA.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.GTA,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.GTA.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Stolen Vehicle", "Stolen Vehicle Configuration"),
								menuItem = new UIMenuItem("Stolen Vehicle", "Stolen Vehicle Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Stolen,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Stolen.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Stolen,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Stolen.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Stolen,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Stolen.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Stolen,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Stolen.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Stolen,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Stolen.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Stolen,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Stolen.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Stolen,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Stolen.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Excessive Speeding", "Excessive Speeding Configuration"),
								menuItem = new UIMenuItem("Excessive Speeding", "Excessive Speeding Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Speeding.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Speeding.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Speeding.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Speeding.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Speeding.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Speeding.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Speeding.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Speeding,
										name = "Speed",
										menuItem = (UIMenuItem)new UIMenuListItem("Speed Threshold", (List<object>)SpeedingThresholdOptions, SpeedingThresholdOptions.IndexOf(Options.BetterChases.Crimes.Speeding.Speed), "Speed in m/s needed to exceed.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Reckless Driving", "Reckless Driving Configuration"),
								menuItem = new UIMenuItem("Reckless Driving", "Reckless Driving Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Reckless.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Reckless.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Reckless.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Reckless.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Reckless.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Reckless.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Reckless.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Reckless,
										name = "Speed",
										menuItem = (UIMenuItem)new UIMenuListItem("Speed Threshold", (List<object>)SpeedingThresholdOptions, SpeedingThresholdOptions.IndexOf(Options.BetterChases.Crimes.Reckless.Speed), "Speed in m/s needed to exceed.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Armed", "Armed Configuration"),
								menuItem = new UIMenuItem("Armed", "Armed Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Armed,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Armed.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Armed,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Armed.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Armed,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Armed.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Armed,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Armed.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Armed,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Armed.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Armed,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Armed.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Armed,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Armed.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Aiming Weapon", "Aiming Weapon Configuration"),
								menuItem = new UIMenuItem("Aiming Weapon", "Aiming Weapon Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Aiming,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Aiming.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Aiming,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Aiming.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Aiming,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Aiming.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Aiming,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Aiming.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Aiming,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Aiming.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Aiming,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Aiming.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Aiming,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Aiming.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Assaulting Civilian", "Assaulting Civilian Configuration"),
								menuItem = new UIMenuItem("Assaulting Civilian", "Assaulting Civilian Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Assault,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Assault.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Assault,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Assault.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Assault,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Assault.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Assault,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Assault.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Assault,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Assault.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Assault,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Assault.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Assault,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Assault.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Assaulting Police", "Assaulting Police Configuration"),
								menuItem = new UIMenuItem("Assaulting Police", "Assaulting Police Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceAssault,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.PoliceAssault.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceAssault,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.PoliceAssault.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceAssault,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.PoliceAssault.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceAssault,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.PoliceAssault.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceAssault,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.PoliceAssault.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceAssault,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.PoliceAssault.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceAssault,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.PoliceAssault.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Shooting Weapon", "Shooting Weapon Configuration"),
								menuItem = new UIMenuItem("Shooting Weapon", "Shooting Weapon Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Shooting,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Shooting.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Shooting,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Shooting.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Shooting,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Shooting.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Shooting,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Shooting.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Shooting,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Shooting.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Shooting,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Shooting.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Shooting,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Shooting.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Civilian Murder", "Civilian Murder Configuration"),
								menuItem = new UIMenuItem("Civilian Murder", "Civilian Murder Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.Murder,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.Murder.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Murder,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Murder.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Murder,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.Murder.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Murder,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.Murder.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Murder,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.Murder.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Murder,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.Murder.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.Murder,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.Murder.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							},
							new Menu
							{
								menu = new UIMenu("Police Murder", "Police Murder Configuration"),
								menuItem = new UIMenuItem("Police Murder", "Police Murder Configuration"),
								subMenus = new List<Menu>
								{
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceMurder,
										name = "Enabled",
										toggle = true,
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Feature Enabled", Options.BetterChases.Crimes.PoliceMurder.Enabled, "Toggles the functionality of the entire feature. If disabled, all options below will be disabled also.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceMurder,
										name = "MaxWantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Max Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.PoliceMurder.MaxWantedLevel), "When wanted level is above this limit ignore this crime, 0 to disable.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceMurder,
										name = "PoliceWitnessThreshold",
										menuItem = (UIMenuItem)new UIMenuListItem("Police Witness Threshold", (List<object>)PoliceWitnessThresholdOptions, PoliceWitnessThresholdOptions.IndexOf(Options.BetterChases.Crimes.PoliceMurder.PoliceWitnessThreshold), "Lower value is easier to spot, 0 to not need LOS.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceMurder,
										name = "PITAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("PIT Authorization", Options.BetterChases.Crimes.PoliceMurder.PITAuthorized, "Allow police to PIT.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceMurder,
										name = "LethalForceAuthorized",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Lethal Force Authorization", Options.BetterChases.Crimes.PoliceMurder.LethalForceAuthorized, "Allow police to use lethal force.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceMurder,
										name = "RequestBackup",
										menuItem = (UIMenuItem)new UIMenuCheckboxItem("Request Backup", Options.BetterChases.Crimes.PoliceMurder.RequestBackup, "Request additional unit be dispatched.")
									},
									new Menu
									{
										parent = Options.BetterChases.Crimes.PoliceMurder,
										name = "WantedLevel",
										menuItem = (UIMenuItem)new UIMenuListItem("Set Wanted Level", (List<object>)WantedLevelOptions, WantedLevelOptions.IndexOf(Options.BetterChases.Crimes.PoliceMurder.WantedLevel), "Set wanted level to this if currently lower, 0 to disable.")
									}
								}
							}
						}
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "CopsManageTraffic",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Cops Manage Traffic", Options.BetterChases.CopsManageTraffic, "If enabled, cops will try to avoid crashing into vehicles, pedestrians and other cops. They will also refrain from ramming you if there are people nearby.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "WreckedCopsStopChasing",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Wrecked Cops Give Up", Options.BetterChases.WreckedCopsStopChasing, "If enabled, cops driving badly damaged vehicles will give up on pursuit.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "DisallowCopCommandeering",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Cops Won't Commandeer", Options.BetterChases.DisallowCopCommandeering, "If enabled, cops will not commandeer civilian vehicles.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "RequirePITAuthorization",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Require PIT Authorization", Options.BetterChases.RequirePITAuthorization, "If enabled, prevents cops from performing PITs/Ramming until they are allowed.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "RequireLethalForceAuthorization",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Require Lethal-Force Authorization", Options.BetterChases.RequireLethalForceAuthorization, "If enabled, prevents cops from using lethal weapons until they are allowed.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "AllowBustOpportunity",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Allow Extra Bust Opportunity", Options.BetterChases.AllowBustOpportunity, "If enabled, you can optionally give up when above 1 star by pressing ~y~" + ((object)Options.SurrenderKey/*cast due to constrained. prefix*/).ToString() + "~w~ or ~y~" + ((object)Options.SurrenderButton/*cast due to constrained. prefix*/).ToString() + "~w~.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "ShowHUD",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display PIT & Lethal Force HUD", Options.BetterChases.ShowHUD, "If enabled, the icons representing PIT & Lethal Force authorization will display near the Wanted Level stars.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "ShowNotifications",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Notifications", Options.BetterChases.ShowNotifications, "Toggles the notification system that keeps you informed of any changes in the police behavior.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "ShowBigMessages",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Big Messages", Options.BetterChases.ShowBigMessages, "If enabled, the game will display messages similar to the online shards when something important happens.")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "IconOffsetX",
						menuItem = (UIMenuItem)new UIMenuListItem("Icon Offset X", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.BetterChases.IconOffsetX), "Controls the horizontal UI offset of the icons")
					},
					new Menu
					{
						parent = Options.BetterChases,
						name = "IconOffsetY",
						menuItem = (UIMenuItem)new UIMenuListItem("Icon Offset Y", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.BetterChases.IconOffsetY), "Controls the vertical UI offset of the icons")
					}
				}
			},
			new Menu
			{
				menu = new UIMenu("Arrest Warrants", "Configure Arrest Warrants"),
				menuItem = new UIMenuItem("Arrest Warrants", "Configure Arrest Warrants"),
				subMenus = new List<Menu>
				{
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "Enabled",
						toggle = true,
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Module Enabled", Options.ArrestWarrants.Enabled, "Toggles the functionality of the entire module. If disabled, all features below will be disabled also.")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "SpotSpeed",
						menuItem = (UIMenuItem)new UIMenuListItem("Overall Spot Speed", (List<object>)SpotSpeedOptions, SpotSpeedOptions.IndexOf(Options.ArrestWarrants.SpotSpeed), "How fast or slow police will spot you, this can be seen via the Spotted Meter. The default is 100%.")
					},
					new Menu
					{
						menu = new UIMenu("Warrant Length", "Warrant Length Settings"),
						menuItem = new UIMenuItem("Warrant Length", "Warrant Length Settings"),
						subMenus = new List<Menu>
						{
							new Menu
							{
								parent = Options.ArrestWarrants.WarrantLenghts,
								name = "OneStar",
								menuItem = (UIMenuItem)new UIMenuListItem("One Star Warrant Length", (List<object>)WarrantLengthOptions, WarrantLengthOptions.IndexOf(Options.ArrestWarrants.WarrantLenghts.OneStar), "How many in-game hours a one star wanted level warrant expires.")
							},
							new Menu
							{
								parent = Options.ArrestWarrants.WarrantLenghts,
								name = "TwoStar",
								menuItem = (UIMenuItem)new UIMenuListItem("Two Star Warrant Length", (List<object>)WarrantLengthOptions, WarrantLengthOptions.IndexOf(Options.ArrestWarrants.WarrantLenghts.TwoStar), "How many in-game hours a two star wanted level warrant expires. ~y~Note~w~: In addition to previous times.")
							},
							new Menu
							{
								parent = Options.ArrestWarrants.WarrantLenghts,
								name = "ThreeStar",
								menuItem = (UIMenuItem)new UIMenuListItem("Three Star Warrant Length", (List<object>)WarrantLengthOptions, WarrantLengthOptions.IndexOf(Options.ArrestWarrants.WarrantLenghts.ThreeStar), "How many in-game hours a three star wanted level warrant expires. ~y~Note~w~: In addition to previous times.")
							},
							new Menu
							{
								parent = Options.ArrestWarrants.WarrantLenghts,
								name = "FourStar",
								menuItem = (UIMenuItem)new UIMenuListItem("Four Star Warrant Length", (List<object>)WarrantLengthOptions, WarrantLengthOptions.IndexOf(Options.ArrestWarrants.WarrantLenghts.FourStar), "How many in-game hours a four star wanted level warrant expires. ~y~Note~w~: In addition to previous times.")
							},
							new Menu
							{
								parent = Options.ArrestWarrants.WarrantLenghts,
								name = "FiveStar",
								menuItem = (UIMenuItem)new UIMenuListItem("Five Star Warrant Length", (List<object>)WarrantLengthOptions, WarrantLengthOptions.IndexOf(Options.ArrestWarrants.WarrantLenghts.FiveStar), "How many in-game hours a five star wanted level warrant expires. ~y~Note~w~: In addition to previous times.")
							}
						}
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "RememberChase",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Remember Last Chase", Options.ArrestWarrants.RememberChase, "If enabled, you will resume the chase you escaped last time if you were identified. If disabled you will start instead with a 2 star level.")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "ShowSpottedMeter",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Spotted Meter", Options.ArrestWarrants.ShowSpottedMeter, "If enabled, the HUD showing how close nearby police are to spotting you will display near the bottom right corner of the screen.")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "ShowSpottedIndicators",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Spotted Indicators", Options.ArrestWarrants.ShowSpottedIndicators, "If enabled, color coded indicators showing police interest in you will display above the police.")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "ShowHUD",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Arrest Warrants HUD", Options.ArrestWarrants.ShowHUD, "If enabled, the current warrant status HUD will display near the bottom right corner of the screen.")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "ShowNotifications",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Notifications", Options.ArrestWarrants.ShowNotifications, "Toggles the notification system that keeps you informed of any changes in arrest warrants.")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "ShowBigMessages",
						menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Big Messages", Options.ArrestWarrants.ShowBigMessages, "If enabled, the game will display messages similar to the online shards when something important happens.")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "IconOffsetX",
						menuItem = (UIMenuItem)new UIMenuListItem("Icon Offset X", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.ArrestWarrants.IconOffsetX), "Controls the horizontal UI offset of the icons")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "IconOffsetY",
						menuItem = (UIMenuItem)new UIMenuListItem("Icon Offset Y", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.ArrestWarrants.IconOffsetY), "Controls the vertical UI offset of the icons")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "TextOffsetX",
						menuItem = (UIMenuItem)new UIMenuListItem("Text Offset X", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.ArrestWarrants.TextOffsetX), "Controls the horizontal UI offset of the text")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "TextOffsetY",
						menuItem = (UIMenuItem)new UIMenuListItem("Text Offset Y", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.ArrestWarrants.TextOffsetY), "Controls the vertical UI offset of the text")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "GradientOffsetX",
						menuItem = (UIMenuItem)new UIMenuListItem("Gradient Offset X", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.ArrestWarrants.GradientOffsetX), "Controls the horizontal UI offset of the gradient")
					},
					new Menu
					{
						parent = Options.ArrestWarrants,
						name = "GradientOffsetY",
						menuItem = (UIMenuItem)new UIMenuListItem("Gradient Offset Y", (List<object>)OffsetOptions, OffsetOptions.IndexOf(Options.ArrestWarrants.GradientOffsetY), "Controls the vertical UI offset of the gradient")
					}
				}
			},
			new Menu
			{
				parent = Options,
				name = "DisplayHints",
				menuItem = (UIMenuItem)new UIMenuCheckboxItem("Display Hints", Options.DisplayHints, "If enabled, the game will display some hints to help you understand how Better Chases and Arrest Warrants work.")
			},
			new Menu
			{
				menu = new UIMenu("Debug", "Mod commands & debugging"),
				menuItem = new UIMenuItem("Debug", "Mod commands & debugging"),
				subMenus = new List<Menu>
				{
					new Menu
					{
						menuItem = new UIMenuItem("Issue Character Warrant"),
						command = ArrestWarrants.IssuePlayerWarrant
					},
					new Menu
					{
						menuItem = new UIMenuItem("Clear All Warrants"),
						command = ArrestWarrants.ClearWarrants
					},
					new Menu
					{
						menuItem = new UIMenuItem("Save Warrants"),
						command = ArrestWarrants.SaveWarrants
					},
					new Menu
					{
						menuItem = new UIMenuItem("Load Warrants"),
						command = ArrestWarrants.LoadWarrants
					},
					new Menu
					{
						menuItem = new UIMenuItem("Save mod settings to file"),
						command = Save
					},
					new Menu
					{
						menuItem = new UIMenuItem("Load mod settings from file"),
						command = Load
					}
				}
			}
		};

		public IngameMenu()
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Expected O, but got Unknown
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0115: Expected O, but got Unknown
			//IL_012c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0136: Expected O, but got Unknown
			//IL_0141: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Expected O, but got Unknown
			//IL_019d: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a7: Expected O, but got Unknown
			//IL_01be: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c8: Expected O, but got Unknown
			//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_01dd: Expected O, but got Unknown
			//IL_0239: Unknown result type (might be due to invalid IL or missing references)
			//IL_0243: Expected O, but got Unknown
			//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b6: Expected O, but got Unknown
			//IL_030b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0315: Expected O, but got Unknown
			//IL_036a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0374: Expected O, but got Unknown
			//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_03d3: Expected O, but got Unknown
			//IL_043c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0446: Expected O, but got Unknown
			//IL_0468: Unknown result type (might be due to invalid IL or missing references)
			//IL_0472: Expected O, but got Unknown
			//IL_047d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0487: Expected O, but got Unknown
			//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ed: Expected O, but got Unknown
			//IL_0556: Unknown result type (might be due to invalid IL or missing references)
			//IL_0560: Expected O, but got Unknown
			//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05bf: Expected O, but got Unknown
			//IL_0614: Unknown result type (might be due to invalid IL or missing references)
			//IL_061e: Expected O, but got Unknown
			//IL_0673: Unknown result type (might be due to invalid IL or missing references)
			//IL_067d: Expected O, but got Unknown
			//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f0: Expected O, but got Unknown
			//IL_0712: Unknown result type (might be due to invalid IL or missing references)
			//IL_071c: Expected O, but got Unknown
			//IL_0727: Unknown result type (might be due to invalid IL or missing references)
			//IL_0731: Expected O, but got Unknown
			//IL_078d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0797: Expected O, but got Unknown
			//IL_0800: Unknown result type (might be due to invalid IL or missing references)
			//IL_080a: Expected O, but got Unknown
			//IL_085f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0869: Expected O, but got Unknown
			//IL_08be: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c8: Expected O, but got Unknown
			//IL_091d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0927: Expected O, but got Unknown
			//IL_0990: Unknown result type (might be due to invalid IL or missing references)
			//IL_099a: Expected O, but got Unknown
			//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_09c6: Expected O, but got Unknown
			//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_09db: Expected O, but got Unknown
			//IL_0a37: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a41: Expected O, but got Unknown
			//IL_0aaa: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab4: Expected O, but got Unknown
			//IL_0b09: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b13: Expected O, but got Unknown
			//IL_0b68: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b72: Expected O, but got Unknown
			//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd1: Expected O, but got Unknown
			//IL_0c3a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c44: Expected O, but got Unknown
			//IL_0c71: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c7b: Expected O, but got Unknown
			//IL_0c86: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c90: Expected O, but got Unknown
			//IL_0ce2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0cec: Expected O, but got Unknown
			//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d0d: Expected O, but got Unknown
			//IL_0d18: Unknown result type (might be due to invalid IL or missing references)
			//IL_0d22: Expected O, but got Unknown
			//IL_0dbf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0dc9: Expected O, but got Unknown
			//IL_0e80: Unknown result type (might be due to invalid IL or missing references)
			//IL_0e8a: Expected O, but got Unknown
			//IL_0edf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ee9: Expected O, but got Unknown
			//IL_0f3e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f48: Expected O, but got Unknown
			//IL_0f6a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f74: Expected O, but got Unknown
			//IL_0f7f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0f89: Expected O, but got Unknown
			//IL_1026: Unknown result type (might be due to invalid IL or missing references)
			//IL_1030: Expected O, but got Unknown
			//IL_10e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_10f1: Expected O, but got Unknown
			//IL_1146: Unknown result type (might be due to invalid IL or missing references)
			//IL_1150: Expected O, but got Unknown
			//IL_11a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_11af: Expected O, but got Unknown
			//IL_11d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_11db: Expected O, but got Unknown
			//IL_11e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_11f0: Expected O, but got Unknown
			//IL_128d: Unknown result type (might be due to invalid IL or missing references)
			//IL_1297: Expected O, but got Unknown
			//IL_134e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1358: Expected O, but got Unknown
			//IL_13f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_13ff: Expected O, but got Unknown
			//IL_1454: Unknown result type (might be due to invalid IL or missing references)
			//IL_145e: Expected O, but got Unknown
			//IL_14b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_14bd: Expected O, but got Unknown
			//IL_14df: Unknown result type (might be due to invalid IL or missing references)
			//IL_14e9: Expected O, but got Unknown
			//IL_14f4: Unknown result type (might be due to invalid IL or missing references)
			//IL_14fe: Expected O, but got Unknown
			//IL_15b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_15bf: Expected O, but got Unknown
			//IL_1690: Unknown result type (might be due to invalid IL or missing references)
			//IL_169a: Expected O, but got Unknown
			//IL_1751: Unknown result type (might be due to invalid IL or missing references)
			//IL_175b: Expected O, but got Unknown
			//IL_17b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_17ba: Expected O, but got Unknown
			//IL_180f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1819: Expected O, but got Unknown
			//IL_183b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1845: Expected O, but got Unknown
			//IL_1850: Unknown result type (might be due to invalid IL or missing references)
			//IL_185a: Expected O, but got Unknown
			//IL_192b: Unknown result type (might be due to invalid IL or missing references)
			//IL_1935: Expected O, but got Unknown
			//IL_1a06: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a10: Expected O, but got Unknown
			//IL_1a65: Unknown result type (might be due to invalid IL or missing references)
			//IL_1a6f: Expected O, but got Unknown
			//IL_1ac4: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ace: Expected O, but got Unknown
			//IL_1afb: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b05: Expected O, but got Unknown
			//IL_1b10: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b1a: Expected O, but got Unknown
			//IL_1b31: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b3b: Expected O, but got Unknown
			//IL_1b46: Unknown result type (might be due to invalid IL or missing references)
			//IL_1b50: Expected O, but got Unknown
			//IL_1bac: Unknown result type (might be due to invalid IL or missing references)
			//IL_1bb6: Expected O, but got Unknown
			//IL_1c1f: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c29: Expected O, but got Unknown
			//IL_1c92: Unknown result type (might be due to invalid IL or missing references)
			//IL_1c9c: Expected O, but got Unknown
			//IL_1cf1: Unknown result type (might be due to invalid IL or missing references)
			//IL_1cfb: Expected O, but got Unknown
			//IL_1d50: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d5a: Expected O, but got Unknown
			//IL_1daf: Unknown result type (might be due to invalid IL or missing references)
			//IL_1db9: Expected O, but got Unknown
			//IL_1e22: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e2c: Expected O, but got Unknown
			//IL_1e4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e58: Expected O, but got Unknown
			//IL_1e63: Unknown result type (might be due to invalid IL or missing references)
			//IL_1e6d: Expected O, but got Unknown
			//IL_1ec9: Unknown result type (might be due to invalid IL or missing references)
			//IL_1ed3: Expected O, but got Unknown
			//IL_1f3c: Unknown result type (might be due to invalid IL or missing references)
			//IL_1f46: Expected O, but got Unknown
			//IL_1faf: Unknown result type (might be due to invalid IL or missing references)
			//IL_1fb9: Expected O, but got Unknown
			//IL_200e: Unknown result type (might be due to invalid IL or missing references)
			//IL_2018: Expected O, but got Unknown
			//IL_206d: Unknown result type (might be due to invalid IL or missing references)
			//IL_2077: Expected O, but got Unknown
			//IL_20cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_20d6: Expected O, but got Unknown
			//IL_213f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2149: Expected O, but got Unknown
			//IL_216b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2175: Expected O, but got Unknown
			//IL_2180: Unknown result type (might be due to invalid IL or missing references)
			//IL_218a: Expected O, but got Unknown
			//IL_21e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_21f0: Expected O, but got Unknown
			//IL_2259: Unknown result type (might be due to invalid IL or missing references)
			//IL_2263: Expected O, but got Unknown
			//IL_22cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_22d6: Expected O, but got Unknown
			//IL_232b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2335: Expected O, but got Unknown
			//IL_238a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2394: Expected O, but got Unknown
			//IL_23e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_23f3: Expected O, but got Unknown
			//IL_245c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2466: Expected O, but got Unknown
			//IL_24cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_24d9: Expected O, but got Unknown
			//IL_24fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_2505: Expected O, but got Unknown
			//IL_2510: Unknown result type (might be due to invalid IL or missing references)
			//IL_251a: Expected O, but got Unknown
			//IL_2576: Unknown result type (might be due to invalid IL or missing references)
			//IL_2580: Expected O, but got Unknown
			//IL_25e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_25f3: Expected O, but got Unknown
			//IL_265c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2666: Expected O, but got Unknown
			//IL_26bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_26c5: Expected O, but got Unknown
			//IL_271a: Unknown result type (might be due to invalid IL or missing references)
			//IL_2724: Expected O, but got Unknown
			//IL_2779: Unknown result type (might be due to invalid IL or missing references)
			//IL_2783: Expected O, but got Unknown
			//IL_27ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_27f6: Expected O, but got Unknown
			//IL_285f: Unknown result type (might be due to invalid IL or missing references)
			//IL_2869: Expected O, but got Unknown
			//IL_288b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2895: Expected O, but got Unknown
			//IL_28a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_28aa: Expected O, but got Unknown
			//IL_2906: Unknown result type (might be due to invalid IL or missing references)
			//IL_2910: Expected O, but got Unknown
			//IL_2979: Unknown result type (might be due to invalid IL or missing references)
			//IL_2983: Expected O, but got Unknown
			//IL_29ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_29f6: Expected O, but got Unknown
			//IL_2a4b: Unknown result type (might be due to invalid IL or missing references)
			//IL_2a55: Expected O, but got Unknown
			//IL_2aaa: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ab4: Expected O, but got Unknown
			//IL_2b09: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b13: Expected O, but got Unknown
			//IL_2b7c: Unknown result type (might be due to invalid IL or missing references)
			//IL_2b86: Expected O, but got Unknown
			//IL_2ba8: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bb2: Expected O, but got Unknown
			//IL_2bbd: Unknown result type (might be due to invalid IL or missing references)
			//IL_2bc7: Expected O, but got Unknown
			//IL_2c23: Unknown result type (might be due to invalid IL or missing references)
			//IL_2c2d: Expected O, but got Unknown
			//IL_2c96: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ca0: Expected O, but got Unknown
			//IL_2d09: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d13: Expected O, but got Unknown
			//IL_2d68: Unknown result type (might be due to invalid IL or missing references)
			//IL_2d72: Expected O, but got Unknown
			//IL_2dc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_2dd1: Expected O, but got Unknown
			//IL_2e26: Unknown result type (might be due to invalid IL or missing references)
			//IL_2e30: Expected O, but got Unknown
			//IL_2e99: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ea3: Expected O, but got Unknown
			//IL_2ec5: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ecf: Expected O, but got Unknown
			//IL_2eda: Unknown result type (might be due to invalid IL or missing references)
			//IL_2ee4: Expected O, but got Unknown
			//IL_2f40: Unknown result type (might be due to invalid IL or missing references)
			//IL_2f4a: Expected O, but got Unknown
			//IL_2fb3: Unknown result type (might be due to invalid IL or missing references)
			//IL_2fbd: Expected O, but got Unknown
			//IL_3026: Unknown result type (might be due to invalid IL or missing references)
			//IL_3030: Expected O, but got Unknown
			//IL_3085: Unknown result type (might be due to invalid IL or missing references)
			//IL_308f: Expected O, but got Unknown
			//IL_30e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_30ee: Expected O, but got Unknown
			//IL_3143: Unknown result type (might be due to invalid IL or missing references)
			//IL_314d: Expected O, but got Unknown
			//IL_31b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_31c0: Expected O, but got Unknown
			//IL_31e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_31ec: Expected O, but got Unknown
			//IL_31f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3201: Expected O, but got Unknown
			//IL_325d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3267: Expected O, but got Unknown
			//IL_32d0: Unknown result type (might be due to invalid IL or missing references)
			//IL_32da: Expected O, but got Unknown
			//IL_3343: Unknown result type (might be due to invalid IL or missing references)
			//IL_334d: Expected O, but got Unknown
			//IL_33a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_33ac: Expected O, but got Unknown
			//IL_3401: Unknown result type (might be due to invalid IL or missing references)
			//IL_340b: Expected O, but got Unknown
			//IL_3460: Unknown result type (might be due to invalid IL or missing references)
			//IL_346a: Expected O, but got Unknown
			//IL_34d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_34dd: Expected O, but got Unknown
			//IL_34ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_3509: Expected O, but got Unknown
			//IL_3514: Unknown result type (might be due to invalid IL or missing references)
			//IL_351e: Expected O, but got Unknown
			//IL_357a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3584: Expected O, but got Unknown
			//IL_35ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_35f7: Expected O, but got Unknown
			//IL_3660: Unknown result type (might be due to invalid IL or missing references)
			//IL_366a: Expected O, but got Unknown
			//IL_36bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_36c9: Expected O, but got Unknown
			//IL_371e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3728: Expected O, but got Unknown
			//IL_377d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3787: Expected O, but got Unknown
			//IL_37f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_37fa: Expected O, but got Unknown
			//IL_381c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3826: Expected O, but got Unknown
			//IL_3831: Unknown result type (might be due to invalid IL or missing references)
			//IL_383b: Expected O, but got Unknown
			//IL_3897: Unknown result type (might be due to invalid IL or missing references)
			//IL_38a1: Expected O, but got Unknown
			//IL_390a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3914: Expected O, but got Unknown
			//IL_397d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3987: Expected O, but got Unknown
			//IL_39dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_39e6: Expected O, but got Unknown
			//IL_3a3b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3a45: Expected O, but got Unknown
			//IL_3a9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3aa4: Expected O, but got Unknown
			//IL_3b0d: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b17: Expected O, but got Unknown
			//IL_3b39: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b43: Expected O, but got Unknown
			//IL_3b4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_3b58: Expected O, but got Unknown
			//IL_3bb4: Unknown result type (might be due to invalid IL or missing references)
			//IL_3bbe: Expected O, but got Unknown
			//IL_3c27: Unknown result type (might be due to invalid IL or missing references)
			//IL_3c31: Expected O, but got Unknown
			//IL_3c9a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ca4: Expected O, but got Unknown
			//IL_3cf9: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d03: Expected O, but got Unknown
			//IL_3d58: Unknown result type (might be due to invalid IL or missing references)
			//IL_3d62: Expected O, but got Unknown
			//IL_3db7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3dc1: Expected O, but got Unknown
			//IL_3e2a: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e34: Expected O, but got Unknown
			//IL_3e8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_3e95: Expected O, but got Unknown
			//IL_3ed6: Unknown result type (might be due to invalid IL or missing references)
			//IL_3ee0: Expected O, but got Unknown
			//IL_3f21: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f2b: Expected O, but got Unknown
			//IL_3f6c: Unknown result type (might be due to invalid IL or missing references)
			//IL_3f76: Expected O, but got Unknown
			//IL_3fb7: Unknown result type (might be due to invalid IL or missing references)
			//IL_3fc1: Expected O, but got Unknown
			//IL_4013: Unknown result type (might be due to invalid IL or missing references)
			//IL_4018: Unknown result type (might be due to invalid IL or missing references)
			//IL_4037: Unknown result type (might be due to invalid IL or missing references)
			//IL_403c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4059: Unknown result type (might be due to invalid IL or missing references)
			//IL_4063: Expected O, but got Unknown
			//IL_40a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_40af: Expected O, but got Unknown
			//IL_40f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_40fa: Expected O, but got Unknown
			//IL_413b: Unknown result type (might be due to invalid IL or missing references)
			//IL_4145: Expected O, but got Unknown
			//IL_419a: Unknown result type (might be due to invalid IL or missing references)
			//IL_41a4: Expected O, but got Unknown
			//IL_41f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_4203: Expected O, but got Unknown
			//IL_4227: Unknown result type (might be due to invalid IL or missing references)
			//IL_4231: Expected O, but got Unknown
			//IL_423c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4246: Expected O, but got Unknown
			//IL_428e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4298: Expected O, but got Unknown
			//IL_42ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_42f7: Expected O, but got Unknown
			//IL_430e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4318: Expected O, but got Unknown
			//IL_4323: Unknown result type (might be due to invalid IL or missing references)
			//IL_432d: Expected O, but got Unknown
			//IL_438c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4396: Expected O, but got Unknown
			//IL_43f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_43ff: Expected O, but got Unknown
			//IL_445e: Unknown result type (might be due to invalid IL or missing references)
			//IL_4468: Expected O, but got Unknown
			//IL_44c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_44d1: Expected O, but got Unknown
			//IL_4530: Unknown result type (might be due to invalid IL or missing references)
			//IL_453a: Expected O, but got Unknown
			//IL_4586: Unknown result type (might be due to invalid IL or missing references)
			//IL_4590: Expected O, but got Unknown
			//IL_45d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_45db: Expected O, but got Unknown
			//IL_461c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4626: Expected O, but got Unknown
			//IL_4667: Unknown result type (might be due to invalid IL or missing references)
			//IL_4671: Expected O, but got Unknown
			//IL_46b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_46bc: Expected O, but got Unknown
			//IL_46fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_4707: Expected O, but got Unknown
			//IL_475c: Unknown result type (might be due to invalid IL or missing references)
			//IL_4766: Expected O, but got Unknown
			//IL_47bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_47c5: Expected O, but got Unknown
			//IL_481a: Unknown result type (might be due to invalid IL or missing references)
			//IL_4824: Expected O, but got Unknown
			//IL_4879: Unknown result type (might be due to invalid IL or missing references)
			//IL_4883: Expected O, but got Unknown
			//IL_48d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_48e2: Expected O, but got Unknown
			//IL_4937: Unknown result type (might be due to invalid IL or missing references)
			//IL_4941: Expected O, but got Unknown
			//IL_4983: Unknown result type (might be due to invalid IL or missing references)
			//IL_498d: Expected O, but got Unknown
			//IL_49a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_49af: Expected O, but got Unknown
			//IL_49ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_49c4: Expected O, but got Unknown
			//IL_49d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_49e2: Expected O, but got Unknown
			//IL_4a08: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a12: Expected O, but got Unknown
			//IL_4a38: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a42: Expected O, but got Unknown
			//IL_4a68: Unknown result type (might be due to invalid IL or missing references)
			//IL_4a72: Expected O, but got Unknown
			//IL_4a98: Unknown result type (might be due to invalid IL or missing references)
			//IL_4aa2: Expected O, but got Unknown
			//IL_4ac8: Unknown result type (might be due to invalid IL or missing references)
			//IL_4ad2: Expected O, but got Unknown
			//IL_4b49: Unknown result type (might be due to invalid IL or missing references)
			//IL_4b53: Expected O, but got Unknown
			//IL_4ba5: Unknown result type (might be due to invalid IL or missing references)
			//IL_4baf: Expected O, but got Unknown
			//IL_4bb9: Unknown result type (might be due to invalid IL or missing references)
			//IL_4bc3: Expected O, but got Unknown
			//IL_4bcd: Unknown result type (might be due to invalid IL or missing references)
			//IL_4bd7: Expected O, but got Unknown
			//IL_4c00: Unknown result type (might be due to invalid IL or missing references)
			//IL_4c0a: Expected O, but got Unknown
			foreach (UIMenu item in MainPool.ToList())
			{
				item.Clear();
			}
			MainPool = new MenuPool();
			MainPool.Add(MainMenu);
			AddMenus(MainPool, Menus, MainMenu);
			foreach (UIMenu item2 in MainPool.ToList())
			{
				item2.OnItemSelect += new ItemSelectEvent(OnItemSelect);
				item2.OnListChange += new ListChangedEvent(OnListChange);
				item2.OnCheckboxChange += new CheckboxChangeEvent(OnCheckboxChange);
			}
			MainMenu.OnMenuClose += new MenuCloseEvent(OnMenuClose);
			MainPool.RefreshIndex();
		}

		private static void AddMenus(MenuPool mainPool, List<Menu> menus, UIMenu parent)
		{
			foreach (Menu menu in menus)
			{
				if (menu.menu != null)
				{
					mainPool.Add(menu.menu);
					parent.AddItem(menu.menuItem);
					parent.BindMenuToItem(menu.menu, menu.menuItem);
					if (menu.subMenus != null)
					{
						AddMenus(mainPool, menu.subMenus, menu.menu);
					}
				}
				else
				{
					parent.AddItem(menu.menuItem);
				}
				if (menu.toggle)
				{
					IngameMenu.ToggleMenus(menus, menu.value);
				}
			}
		}

		private static void ApplySetting(List<Menu> menus, UIMenuItem menuItem, dynamic value)
		{
			foreach (Menu menu in menus)
			{
				if (((object)menu.menuItem).GetHashCode() == ((object)menuItem).GetHashCode())
				{
					if (value != null)
					{
						menu.value = (object)value;
					}
					else if (menu.command != null)
					{
						menu.command();
					}
					if (menu.toggle)
					{
						IngameMenu.ToggleMenus(menus, menu.value);
					}
				}
				else if (menu.subMenus != null)
				{
					IngameMenu.ApplySetting(menu.subMenus, menuItem, value);
				}
			}
		}

		private static void ToggleMenus(List<Menu> menus, bool enabled)
		{
			foreach (Menu menu in menus)
			{
				if (!menu.toggle)
				{
					menu.menuItem.Enabled = enabled;
				}
			}
		}

		private static void OnItemSelect(UIMenu sender, UIMenuItem menuItem, int index)
		{
			ApplySetting(Config.Menu.Menus, menuItem, null);
		}

		private static void OnCheckboxChange(UIMenu sender, UIMenuCheckboxItem checkboxItem, bool enabled)
		{
			ApplySetting(Config.Menu.Menus, (UIMenuItem)(object)checkboxItem, enabled);
		}

		private static void OnListChange(UIMenu sender, UIMenuListItem listItem, int index)
		{
			IngameMenu.ApplySetting(Config.Menu.Menus, listItem, (dynamic)listItem.IndexToItem(index));
		}

		private static void OnMenuClose(UIMenu sender)
		{
			if (((object)sender).GetHashCode() == ((object)Config.Menu.MainMenu).GetHashCode())
			{
				Save();
			}
		}
	}

	public static GlobalConfig Options = new GlobalConfig();

	public static List<dynamic> WantedLevelOptions = new List<object> { 0, 1, 2, 3, 4, 5 };

	public static List<dynamic> WantedLevelControlOptions = new List<object> { "Full", "Passive" };

	public static List<dynamic> SpeedingThresholdOptions = (dynamic)Enumerable.Range(1, 100).Cast<object>().ToList();

	public static List<dynamic> PoliceWitnessThresholdOptions = (dynamic)Enumerable.Range(0, 100).Cast<object>().ToList();

	public static List<dynamic> WarrantLengthOptions = (dynamic)Enumerable.Range(1, 672).Cast<object>().ToList();

	public static List<dynamic> ChaseLengthOptions = (dynamic)Enumerable.Range(1, 359).Cast<object>().ToList();

	public static List<dynamic> CopDispatchOptions = (dynamic)Enumerable.Range(-1, 21).Cast<object>().ToList();

	public static List<dynamic> SpotSpeedOptions = (dynamic)Enumerable.Range(10, 300).Cast<object>().ToList();

	public static List<dynamic> OffsetOptions = (dynamic)Enumerable.Range(-2000, 4000).Cast<object>().ToList();

	private static IngameMenu Menu;

	public static bool IsMenuOpen => Menu != null && Menu.MainPool.IsAnyMenuOpen();

	public Config()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		base.Tick += OnTick;
		base.KeyUp += new KeyEventHandler(OnKeyUp);
		base.Aborted += Shutdown;
		base.Interval = 4;
		StoragePaths.MigrateLegacyFiles();
		Load();
		ReloadMenu();
		Diagnostics.Info("Configuration script initialized.");
	}

	private void OnTick(object sender, EventArgs e)
	{
		Menu?.MainPool.ProcessMenus();
	}

	private void OnKeyUp(object sender, KeyEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (e.KeyCode == Menu.MenuKey && !Menu.MainPool.IsAnyMenuOpen())
		{
			Menu.MainMenu.Visible = !Menu.MainMenu.Visible;
		}
	}

	public static void Save()
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
			XmlPersistence.Save(StoragePaths.ConfigFile, Options, "BetterChasesPlus");
		}
		catch (Exception exception)
		{
			Diagnostics.Error("Saving configuration", exception);
		}
	}

	public static void Load()
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
			if (XmlPersistence.TryLoad(StoragePaths.ConfigFile, "BetterChasesPlus", out GlobalConfig loaded))
			{
				Options = loaded;
			}
		}
		catch (Exception exception)
		{
			Diagnostics.Error("Loading configuration", exception);
		}
	}

	private void Shutdown(object sender, EventArgs e)
	{
		Save();
		if (Menu != null && Menu.MainPool.IsAnyMenuOpen())
		{
			Menu.MainPool.CloseAllMenus();
		}
		Diagnostics.Info("Configuration script stopped.");
	}

	public static void ReloadMenu()
	{
		if (Menu != null)
		{
			if (Menu.MainPool.IsAnyMenuOpen())
			{
				Menu.MainPool.CloseAllMenus();
			}
		}
		Menu = new IngameMenu();
	}
}
