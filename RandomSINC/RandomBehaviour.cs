using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace RandomSINC
{
	internal class RandomBehaviour : ModBehaviour
	{
		bool firstDay = true;

		public override void OnActivate()
		{
			TimeOfDay.OnDayPassed += DayPassed;
			TimeOfDay.OnMonthPassed += MonthPassed;
			GameSettings.IsDoneLoadingGame += GameLoaded;
		}

		private void MonthPassed(object sender, EventArgs e)
		{
			RandomizeMonthly();
			if(TimeOfDay.Instance.Month == 11)
			{
				RandomizeYearly();
			}
		}

		private void DayPassed(object sender, EventArgs e)
		{
			RandomizeDaily();
		}

		private void GameLoaded(object sender, EventArgs e)
		{
			RandomizeStart();
		}

		public override void OnDeactivate()
		{
			TimeOfDay.OnDayPassed -= DayPassed;
			TimeOfDay.OnMonthPassed -= MonthPassed;
			GameSettings.IsDoneLoadingGame -= GameLoaded;
		}

		private void RandomizeDaily()
		{
			DevConsole.Console.Log("Daily Randomizer started...");
			var softwares = GameSettings.Instance.simulation.GetAllProducts(false);
			foreach (var product in softwares)
			{
				//Randomize Software Marketing
				product.Marketing = (MarketSimulation.Active.GetMaxAwareness(product) * (float)new Random().NextDouble()) * new Random().Range(-10,10);
				//Randomize Software Followers
				product.Followers = (uint)new Random().Range(0, 1000000);
				//Randomize Software Bugs
				product.Bugs = (int)new Random().Range(0, 1000);
			}

			//Randomize Shares
			var stockmarkets = GameSettings.Instance.StockMarkets;
			foreach (var stock in stockmarkets)
			{
				stock.Value = (float)((stock.Value * new Random().NextDouble()) * new Random().Range(-2, 2));
			}

			if(firstDay)
			{
				firstDay = false;
				RandomizeMonthly();
				RandomizeYearly();
			}
		}

		private void RandomizeMonthly()
		{
			DevConsole.Console.Log("Monthly Randomizer started...");
		}

		private void RandomizeYearly()
		{
			DevConsole.Console.Log("Yearly Randomizer started...");
			var softwares = GameSettings.Instance.simulation.GetAllProducts(false);
			foreach (var product in softwares)
			{
				//Randomize Software ownership
				product.DevCompany = GameSettings.Instance.simulation.GetAllCompanies().GetRandom();
			}

			//Randomize Founders Stats
			var founders = GameSettings.Instance.sActorManager.Actors.Where(actor => actor.employee.Founder);
			foreach (var founder in founders)
			{
				DevConsole.Console.Log("Randomize Founders Age");
				founder.employee.AgeMonth = (int)new Random().Range(12 * 20, 12 * 60);

				DevConsole.Console.Log("Randomize Founders Spec Experience");
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Lead, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Designer, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Programmer, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Artist, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Service, new Random().Range(0, 100));

				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Lead, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Designer, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Programmer, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Artist, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Service, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);

				DevConsole.Console.Log("Randomize Founders Specializations");
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Lead))
				{
					DevConsole.Console.Log("Randomize Founders Lead Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Lead, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Designer))
				{
					DevConsole.Console.Log("Randomize Founders Designer Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Designer, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Programmer))
				{
					DevConsole.Console.Log("Randomize Founders Programmer Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Programmer, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Artist))
				{
					DevConsole.Console.Log("Randomize Founders Artist Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Artist, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Service))
				{
					DevConsole.Console.Log("Randomize Founders Service Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Service, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				System.Threading.Thread.Sleep(50);
				//Randomize Founders Traits
				//founder.employee.Traits = Employee.Trait.
				//Randomize founder creativity
			}
		}

		private void RandomizeStart()
		{
			DevConsole.Console.Log("Randomize Difficulty");
			//Randomize Difficulty
			GameSettings.Instance.Difficulty = (int)new Random().Range(0, 3);

			DevConsole.Console.Log("Randomize Founders Stats");
			//Randomize Founders Stats
			var founders = GameSettings.Instance.sActorManager.Actors.Where(actor => actor.employee.Founder);
			foreach(var founder in founders)
			{
				DevConsole.Console.Log("Randomize Founders Age");
				founder.employee.AgeMonth = (int)new Random().Range(12, 12 * 60);
				
				DevConsole.Console.Log("Randomize Founders Spec Experience");
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Lead, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Designer, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Programmer, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Artist, new Random().Range(0, 100));
				System.Threading.Thread.Sleep(50);
				founder.employee.SetSpecExperience(Employee.EmployeeRole.Service, new Random().Range(0, 100));

				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Lead, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Designer, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Programmer, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Artist, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);
				founder.employee.ChangeSkillDirect(Employee.EmployeeRole.Service, (float)new Random().NextDouble());
				System.Threading.Thread.Sleep(50);

				DevConsole.Console.Log("Randomize Founders Specializations");
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Lead))
				{
					DevConsole.Console.Log("Randomize Founders Lead Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Lead, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Designer))
				{
					DevConsole.Console.Log("Randomize Founders Designer Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Designer, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Programmer))
				{
					DevConsole.Console.Log("Randomize Founders Programmer Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Programmer, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Artist))
				{
					DevConsole.Console.Log("Randomize Founders Artist Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Artist, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				foreach (string spec in GameSettings.Instance.GetAllSpecializations(Employee.EmployeeRole.Service))
				{
					DevConsole.Console.Log("Randomize Founders Service Spec " + spec);
					founder.employee.SetSpecialization(Employee.EmployeeRole.Service, spec, (int)new Random().Range(0, 3));
					System.Threading.Thread.Sleep(50);
				}
				System.Threading.Thread.Sleep(50);
				//Randomize Founders Traits
				//founder.employee.Traits = Employee.Trait.

				//Randomize founder creativity

			}

			DevConsole.Console.Log("Randomize Starting Money");
			//Randomize Starting Money
			GameSettings.Instance.MyCompany.MakeTransaction(-GameSettings.Instance.MyCompany.Money, Company.TransactionCategory.NA); 
			GameSettings.Instance.MyCompany.MakeTransaction(new Random().Range(-10000, 1000000), Company.TransactionCategory.NA);

			System.Threading.Thread.Sleep(50);

			DevConsole.Console.Log("Randomize Loans");
			//Randomize Starting Loan
			float maxLoan = GameSettings.Instance.MyCompany.Money > 0 ? GameSettings.Instance.MyCompany.Money : new Random().Range(-100,10000);
			if(maxLoan > 0)
			{
				GameSettings.Instance.Loans.Add(new KeyValuePair<int, float>((int)new Random().Range(6, 24), new Random().Range(1, maxLoan)));
			}

			var softwares = GameSettings.Instance.simulation.GetAllProducts(false);
			foreach(var product in softwares)
			{
				//Randomize Software ownership
				product.DevCompany = GameSettings.Instance.simulation.GetAllCompanies().GetRandom();
				//Randomize Software Marketing
				product.Marketing = MarketSimulation.Active.GetMaxAwareness(product) * (float)new Random().NextDouble();
				//Randomize Software Followers
				product.Followers = (uint)new Random().Range(0, 1000000);
				//Randomize Software Bugs
				product.Bugs = (int)new Random().Range(0, 1000);
			}

			//Randomize Shares
			var stockmarkets = GameSettings.Instance.StockMarkets;
			foreach(var stock in stockmarkets)
			{
				stock.Value = (float)(stock.Value * new Random().NextDouble() * new Random().Range(-1, 1));
			}

			//Randomize Furniture Prices

			//Randomize Furniture Stats

			//Randomize AI Companies

			//Randomize Contracts

		}
	}
}
