using Microsoft.AspNetCore.Mvc;
using Mvc2.Models;
using System.Diagnostics;
using Mvc2.Services;

namespace Mvc2.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmployeeService _employeeService;
    const string SelectedEmployeeIds = "SelectedEmployeeIds";



    public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult EmployeeList(int? selectedId)
    {
        ViewModels.Home.EmployeeList list = new ViewModels.Home.EmployeeList();

        list.Employees = _employeeService.GetAllEmployees();
        if (selectedId.HasValue == false)
        {
            list.SelectedEmployees = new List<Employee>();
            HttpContext.Session.SetString(SelectedEmployeeIds, "");
        }
        else
        {
            var oldList = HttpContext.Session.GetString(SelectedEmployeeIds);

            if (string.IsNullOrWhiteSpace(oldList))
            {
                HttpContext.Session.SetString(SelectedEmployeeIds, selectedId.GetValueOrDefault().ToString());
                var emp = _employeeService.GetEmployeesById(selectedId.GetValueOrDefault());
                if (emp != null)
                {
                    list.SelectedEmployees.Add(emp);
                }
            }
            else
            {
                string newList = "";

                if (oldList.Split(",").Contains(selectedId.GetValueOrDefault().ToString()))
                {
                    newList = oldList;
                }
                else
                {
                    newList = oldList + "," + selectedId.GetValueOrDefault().ToString();
                    HttpContext.Session.SetString(SelectedEmployeeIds, newList);
                }

                var ids = newList.Split(",").ToList().Select(o => Convert.ToInt32(o)).ToList();
                list.SelectedEmployees.AddRange(_employeeService.GetEmployeesByIds(ids));

            }


            //if (HttpContext.Session.Keys.Any(o=> o== SelectedEmployeeIds))
            //{

            //}

        }


        return View(list);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
