using Riode.Models;

namespace Riode.ViewModels;

public class HeaderViewModel
{
    public Dictionary<string, string> Settings { get; set; }
    public List<BasketModel> BasketModels { get; set; }
    public double TotalPrice { get; set; }
}