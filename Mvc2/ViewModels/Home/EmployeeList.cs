using Mvc2.Models;

namespace Mvc2.ViewModels.Home;

public class EmployeeList
{
    public List<Models.Employee> Employees { get; set; } = new List<Models.Employee>();
    public List<Models.Employee> SelectedEmployees { get; set; } = new List<Models.Employee>();

}
