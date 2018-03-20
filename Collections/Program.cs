using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Plexware.Assessment.Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            Employees list1 = new Employees();
            list1.Add(new Employee() { FirstName = "Sarah", LastName = "Scott" });
            list1.Add(new Employee() { FirstName = "John", LastName = "Smith" });
            list1.Add(new Employee() { FirstName = "David", LastName = "Brown" });
            list1.Add(new Employee() { FirstName = "Joanne", LastName = "Snyder" });

            Employees list2 = new Employees();
            list2.Add(new Employee() { FirstName = "Susan", LastName = "Smith" });
            list2.Add(new Employee() { FirstName = "Joanne", LastName = "Snyder" });
            list2.Add(new Employee() { FirstName = "John", LastName = "Holmes" });
            list2.Add(new Employee() { FirstName = "David", LastName = "Brown" });

            List<Employee> intersection = list1.Intersection(list2);



            Debug.Assert(intersection.Count == 2, "There are supposed to be two items in the list.");
            bool foundItems = 
                ((intersection[0].FirstName == "David" && intersection[0].LastName == "Brown" && 
                intersection[1].FirstName == "Joanne" && intersection[1].LastName == "Snyder") ||
                (intersection[1].FirstName == "David" && intersection[1].LastName == "Brown" && 
                intersection[0].FirstName == "Joanne" && intersection[0].LastName == "Snyder"));
            Debug.Assert(foundItems, "The expected items were not found in the intersection!");
            
        }
    }
}

public class ProductComparer : IEqualityComparer<Employee>
{
    public bool Equals(Employee x, Employee y)
    {
        //Check whether the objects are the same object. 
        if (Object.ReferenceEquals(x, y)) return true;

        //Check whether the products' properties are equal. 
        return x != null && y != null && x.FirstName.Equals(y.FirstName) && x.LastName.Equals(y.LastName);
    }

    public int GetHashCode(Employee obj)
    {
        //Get hash code for the Name field if it is not null. 
        int hashProductName = obj.FirstName == null ? 0 : obj.FirstName.GetHashCode();

        //Get hash code for the Code field. 
        int hashProductCode = obj.LastName.GetHashCode();

        //Calculate the hash code for the product. 
        return hashProductName ^ hashProductCode;
    }
}

public class Employees 
{
    private Collection<Employee> employees;

    public Employees()
    {
        employees = new Collection<Employee>();
    }

    public void Add(Employee employee)
    {
        employees.Add(employee);
    }

    public List<Employee> Intersection(Employees otherEmployees)
    {
        return this.employees.Intersect(otherEmployees.employees, new ProductComparer()).ToList();       
    }
}

public class Employee
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
}