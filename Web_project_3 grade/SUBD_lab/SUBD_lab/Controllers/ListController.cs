using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using SUBD_lab.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MySql.Data.MySqlClient;

namespace SUBD_lab.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    
        
    public class ListController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        
        
       
        public ListController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select ProductId,ProductName,ProductCount,ProductCost
                            
                            from dbo.List";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand command = new SqlCommand(query, myCon))
                {
                    myReader = command.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]

        public JsonResult Post(List list)
        {
            
            string query = @"insert into dbo.List (ProductName,ProductCount,ProductCost)
            values (@ProductName, @ProductCount, @ProductCost)";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand command = new SqlCommand(query, myCon))
                {
                    command.Parameters.AddWithValue("@ProductName", list.ProductName);
                    command.Parameters.AddWithValue("@ProductCount", list.ProductCount);
                    command.Parameters.AddWithValue("@ProductCost", list.ProductCost);
                   
                    myReader = command.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]

        public JsonResult Update(List list)
        {
            string query = @"
            update dbo.List set ProductName=@ProductName, ProductCount=@ProductCount, ProductCost=@ProductCost where ProductId=@ProductId";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand command = new SqlCommand(query, myCon))
                {
                    command.Parameters.AddWithValue("@ProductId", list.ProductId);
                    command.Parameters.AddWithValue("@ProductName", list.ProductName);
                    command.Parameters.AddWithValue("@ProductCount", list.ProductCount);
                    command.Parameters.AddWithValue("@ProductCost", list.ProductCost);
                    
                    myReader = command.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            string query = @"
            delete from dbo.List
            where ProductId = " + id + @"
            ";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand command = new SqlCommand(query, myCon))
                {
                    myReader = command.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        [Route("{id}/increment")]
        [HttpPut]
        public void Increment(int id)
        {
            
            string query = @"update dbo.List set ProductCount=ProductCount+1 where ProductId=@ProductId";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand command = new SqlCommand(query, myCon))
                {
                    command.Parameters.AddWithValue("@ProductId", id);
                    
                    myReader = command.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
        }
        [Route("{id}/{count}/decrease")]
        [HttpPut]
        public void Decrease(int id, int count)
        {
           
            if (count == 1)
            {
                Delete(id);
                return;
            }
            string query = @"update dbo.List set ProductCount=ProductCount-1 where ProductId=@ProductId";
            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand command = new SqlCommand(query, myCon))
                {
                    command.Parameters.AddWithValue("@ProductId", id);
                    myReader = command.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
            }
        }
    }
    
}

