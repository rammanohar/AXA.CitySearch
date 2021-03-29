/// <summary>
/// CityResult
/// </summary>

namespace AXA.CitySearch.Service
{
	using AXA.CitySearch.Interface;
	using System;
	using System.Collections.Generic;
	public class CityResult : ICityResult
	{
		public ICollection<string> NextLetters { get; set; }
		public ICollection<string> NextCities { get; set; }

		public CityResult()
		{
			NextLetters = new HashSet<String>();
			NextCities = new HashSet<String>();
		}
	}
   
}
