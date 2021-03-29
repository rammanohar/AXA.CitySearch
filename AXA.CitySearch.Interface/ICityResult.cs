/// <summary>
/// ICityResult
/// </summary>
namespace AXA.CitySearch.Interface
{

    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface ICityResult
    {
        /// <summary>
        /// 
        /// </summary>
        ICollection<string> NextLetters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ICollection<string> NextCities { get; set; }
    }

}
