using System;
using Service.Services;
using Service.Services.Interfaces;
using Service.Helpers.Extentions;

namespace ConsoleAppProject.Controllers
{
	public class LocationController
	{
		private readonly ILocationService _locationService;

		public LocationController()
		{
			_locationService = new LocationService();
		}

		public void Create()
		{
			ConsoleColor.Cyan.WriteConsole("test 5");
		}

    }
}

