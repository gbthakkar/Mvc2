using Mvc2.Models;

namespace Mvc2.Services;


public interface IEmployeeService
{
    List<Employee> GetAllEmployees();
    Employee? GetEmployeesById(int id);

    List<Employee> GetEmployeesByIds(List<int> ids);
}



public class EmployeeService : IEmployeeService
{
    public List<Employee> GetAllEmployees()
    {
        List<Employee> list = new List<Employee>();
        list.Add(new Employee(){Id = 1,Name = "Emp 01",City = "City 01"});
        list.Add(new Employee(){Id = 2,Name = "Emp 02",City = "City 02"});
        list.Add(new Employee(){Id = 3,Name = "Emp 03",City = "City 03"});
        list.Add(new Employee(){Id = 4,Name = "Emp 04",City = "City 04"});
        list.Add(new Employee(){Id = 5,Name = "Emp 05",City = "City 05"});

        return list;
    }

    public Employee? GetEmployeesById(int id)
    {
        return GetAllEmployees().FirstOrDefault(o => o.Id == id);
        
    }
    public List<Employee> GetEmployeesByIds(List<int> ids)
    {
        var allEmp = GetAllEmployees();

        var query = (from emp in allEmp
                     where ids.Contains(emp.Id)
                select emp
                );

        //foreach (var id in ids)
        //{
        //    query = query.Where(o => o.Id == id);
        //}
        
        return query.ToList();

    }

}
