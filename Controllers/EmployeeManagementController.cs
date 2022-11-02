using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MvcEmpAdoDemo.Models;

namespace MvcEmpAdoDemo.Controllers;
public class EmployeeManagement : Controller {
    EmployeeDataAccessLayer objEmp = new EmployeeDataAccessLayer();

    public IActionResult Index(){
        List<Employee> lstEmp = new List<Employee>();
        lstEmp = objEmp.GetAllEmp().ToList();

        return View(lstEmp);
    }

    [HttpGet]
    public IActionResult Add(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Employee emp){
        emp.EmpId = Helper.Helper.STD_ID;
        emp.isDeleted = Helper.Helper.initDelStatus();
        ModelState.Remove("EmpId");
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        if (ModelState.IsValid)  
        {  
            objEmp.AddEmp(emp);  
            return RedirectToAction("Index");  
        }  
        return View(emp); 
    }

    [HttpGet]
    public IActionResult Update(String id){
        if(id == null){
            return NotFound();
        }

        Employee emp = objEmp.GetEmp(id);

        if(emp == null){
            return NotFound();
        }

        return View(emp);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Employee emp){
        if (ModelState.IsValid)  
        {  
            objEmp.UpdateEmp(emp);  
            return RedirectToAction("Index");  
        }  
        return View(emp); 
    }

    [HttpGet]
    public IActionResult Details(String id){
        if(id == null){
            return NotFound();
        }

        Employee emp = objEmp.GetEmp(id);

        if(emp == null){
            return NotFound();
        }

        return View(emp);
    }

    [HttpGet]
    public IActionResult Delete(String id){
        if(id == null){
            return NotFound();
        }

        Employee emp = objEmp.GetEmp(id);

        if(emp == null){
            return NotFound();
        }

        return View(emp);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Employee emp){
        if (ModelState.IsValid)  
        {  
            objEmp.DeleteEmp(emp);  
            return RedirectToAction("Index");  
        }  
        return View(emp); 
    }

    [HttpGet]
    public IActionResult TotalSalary(){
        Employee emp = new Employee();
        return View(emp);
    }

    [HttpPost]
    public IActionResult TotalSalary(Employee emp){
        emp.Salary = 0;

        if(!String.IsNullOrEmpty(emp.Jabatan)){
            List<int> salaries = new List<int>();
            salaries = objEmp.GetSalaryEmp(emp.Jabatan);
            emp.Salary = Helper.Helper.sumSalary(salaries);
        }

        return View(emp);
    }

}