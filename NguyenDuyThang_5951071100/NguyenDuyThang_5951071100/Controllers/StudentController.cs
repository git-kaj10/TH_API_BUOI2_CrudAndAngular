using NguyenDuyThang_5951071100.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace NguyenDuyThang_5951071100.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        private SqlConnection _conn;
        private SqlDataAdapter _adapter;

        public StudentController()
        {
            _conn = new SqlConnection(@"Data Source=DESKTOP-4A00V1R\SQLEXPRESS;Initial Catalog=TH_API_BUOI2;Integrated Security=True");
        }

        // GET api/<controller>
        public IEnumerable<Student> Get()
        {
            DataTable _dt = new DataTable();
            var query = "select * from Student";
            _adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query, _conn)
            };
            _adapter.Fill(_dt);
            List<Student> students = new List<Student>(_dt.Rows.Count);
            if(_dt.Rows.Count > 0)
            {
                foreach(DataRow studentRecord in _dt.Rows)
                {
                    students.Add(new ReadStudent(studentRecord));
                }
            }

            return students;
        }

        // GET api/<controller>/5
        public IEnumerable<Student> Get(int id)
        {
            DataTable _dt = new DataTable();
            var query = string.Format("select * from Student where id = {0}", id);
            _adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query, _conn)
            };
            _adapter.Fill(_dt);
            List<Student> students = new List<Student>(_dt.Rows.Count);
            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow studentRecord in _dt.Rows)
                {
                    students.Add(new ReadStudent(studentRecord));
                }
            }

            return students;
        }

        // POST api/<controller>
        public string Post([FromBody] CreateStudent value)
        {
            var query = "insert into Student(f_name, m_name, l_name, address, birthDate, score) values(@f_name, @m_name, @l_name, @address, @birthDate, @score)";
            SqlCommand insertCommand = new SqlCommand(query, _conn);
            insertCommand.Parameters.AddWithValue("@f_name", value.f_name);
            insertCommand.Parameters.AddWithValue("@m_name", value.m_name);
            insertCommand.Parameters.AddWithValue("@l_name", value.l_name);
            insertCommand.Parameters.AddWithValue("@address", value.address);
            insertCommand.Parameters.AddWithValue("@birthDate", value.birthDate);
            insertCommand.Parameters.AddWithValue("@score", value.score);

            _conn.Open();
            int result = insertCommand.ExecuteNonQuery();
            if(result > 0)
            {
                return "Them thanh cong";
            }
            else
            {
                return "Them that bai";
            }

        }

        // PUT api/<controller>/5
        public string Put(int id, [FromBody] CreateStudent value)
        {
            var query = "update Student set f_name=@f_name, m_name=@m_name, l_name=@l_name, address=@address, birthDate=@birthDate, score=@score where id = @id";
            SqlCommand updateCommand = new SqlCommand(query, _conn);
            updateCommand.Parameters.AddWithValue("@f_name", value.f_name);
            updateCommand.Parameters.AddWithValue("@m_name", value.m_name);
            updateCommand.Parameters.AddWithValue("@l_name", value.l_name);
            updateCommand.Parameters.AddWithValue("@address", value.address);
            updateCommand.Parameters.AddWithValue("@birthDate", value.birthDate);
            updateCommand.Parameters.AddWithValue("@score", value.score);
            updateCommand.Parameters.AddWithValue("@id", id);
            _conn.Open();
            int result = updateCommand.ExecuteNonQuery();
            if (result > 0)
            {
                return "Cap nhat thanh cong";
            }
            else
            {
                return "Cap nhat that bai";
            }
        }

        // DELETE api/<controller>/5
        public String Delete(int id)
        {
            var query = "delete from Student where id = @id";
            SqlCommand deleteCommand = new SqlCommand(query, _conn);
            deleteCommand.Parameters.AddWithValue("@id", id);
         
            _conn.Open();
            int result = deleteCommand.ExecuteNonQuery();
            if (result > 0)
            {
                return "Xoa thanh cong";
            }
            else
            {
                return "Xoa that bai";
            }
        }
    }
}