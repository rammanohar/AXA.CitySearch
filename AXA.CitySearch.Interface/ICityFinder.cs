/// <summary>
/// ICityFinder
/// </summary>
namespace AXA.CitySearch.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICityFinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        ICityResult Search(string searchString);
    }

}
