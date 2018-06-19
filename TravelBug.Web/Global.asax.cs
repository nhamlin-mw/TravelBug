using System.Web.Mvc;
using EPiServer;
using TravelBug.Application.Configuration;

namespace TravelBug
{
	public class EPiServerApplication : Global
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			ViewEngines.Engines.Add(new TravelViewEngine());
			//Tip: Want to call the EPiServer API on startup? Add an initialization module instead (Add -> New Item.. -> EPiServer -> Initialization Module)
		}
	}
}
