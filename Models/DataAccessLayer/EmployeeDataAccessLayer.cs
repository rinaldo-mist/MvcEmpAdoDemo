using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using MvcEmpAdoDemo.Controllers.Helper;

namespace MvcEmpAdoDemo.Models {
    public class EmployeeDataAccessLayer {
        public SqlDataReader getData(String sp, String empId){
            SqlConnection conn = new SqlConnection(Helper.CONN_STRING_EMP);
            SqlCommand cmd = new SqlCommand(sp, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if(!String.IsNullOrEmpty(empId) && sp == Helper.SP_GET_EMP_BY_ID){
                cmd.Parameters.AddWithValue("@EmpId", empId);
            }
            conn.Open();

            SqlDataReader rdr = cmd.ExecuteReader();

            conn.Close();
            return rdr;
        }

        public void postData(String sp, Employee emp){
            SqlConnection conn = new SqlConnection(Helper.CONN_STRING_EMP);
            SqlCommand cmd = new SqlCommand(sp, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if(sp == Helper.SP_DELETE_EMP_BY_ID){
                cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
            } else {
                if(sp == Helper.SP_ADD_EMP){
                    cmd.Parameters.AddWithValue("@EmpId", Helper.generateEmpId(emp.Jabatan,emp.Nama));
                } else if (sp == Helper.SP_UPDATE_EMP_BY_ID){
                    cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
                }
                cmd.Parameters.AddWithValue("@Nama", emp.Nama);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@Jabatan", emp.Jabatan);
                cmd.Parameters.AddWithValue("@isDeleted", emp.isDeleted);
            }
            
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Get All Employee
        public List<Employee> GetAllEmp(){
            List<Employee> lstEmp = new List<Employee>();
            try
            {
                SqlConnection conn = new SqlConnection(Helper.CONN_STRING_EMP);
                SqlCommand cmd = new SqlCommand(Helper.SP_GET_ALL_EMP, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                //SqlDataReader rdr = getData(Helper.SP_GET_ALL_EMP,"");

                while(rdr.Read()){
                    Employee emp = new Employee();

                    emp.EmpId = rdr["EmpId"].ToString();
                    emp.Nama = rdr["Nama"].ToString();
                    emp.Salary = Convert.ToInt32(rdr["Salary"]);
                    emp.Jabatan = rdr["Jabatan"].ToString();
                    emp.isDeleted = Convert.ToInt32(rdr["isDeleted"]);

                    lstEmp.Add(emp);
                }
                conn.Close();
            }
            catch (System.Exception)
            {
                throw;
            }

            return lstEmp;
        }

        public Employee GetEmp(String empId){
            Employee emp = new Employee();
            
            try
            {
                SqlConnection conn = new SqlConnection(Helper.CONN_STRING_EMP);
                SqlCommand cmd = new SqlCommand(Helper.SP_GET_EMP_BY_ID, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", empId);
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read()){

                    emp.EmpId = rdr["EmpId"].ToString();
                    emp.Nama = rdr["Nama"].ToString();
                    emp.Salary = Convert.ToInt32(rdr["Salary"]);
                    emp.Jabatan = rdr["Jabatan"].ToString();
                    emp.isDeleted = Convert.ToInt32(rdr["isDeleted"]);
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return emp;
        }

        public void AddEmp(Employee emp){
            try
            {
                postData(Helper.SP_ADD_EMP, emp);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void UpdateEmp(Employee emp){
            try
            {
                postData(Helper.SP_UPDATE_EMP_BY_ID, emp);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void DeleteEmp(Employee emp){
            try
            {
                postData(Helper.SP_DELETE_EMP_BY_ID, emp);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public List<int> GetSalaryEmp(String jabatan){
            List<int> salaries = new List<int>();
            
            try
            {
                SqlConnection conn = new SqlConnection(Helper.CONN_STRING_EMP);
                SqlCommand cmd = new SqlCommand(Helper.SP_GET_SALARY_EMP, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Jabatan", jabatan);
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read()){
                    salaries.Add(Convert.ToInt32(rdr["Salary"]));
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return salaries;
        }

    }
}