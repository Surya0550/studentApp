using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        string jsonFile = @"C:\Users\saisu\OneDrive\Documents\studentDb.json";

        // GET: api/<StudentController>
        [EnableCors]
        [HttpGet]
        public string Get()
        {
            var json = System.IO.File.ReadAllText(jsonFile);

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    return jObject["students"].ToString();
                }
                else
                {
                    Console.WriteLine("jObject is null");
                    // return "NULL";
                    return "NULL";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Exception occured";
            }
            // return "value2";
        }

        // GET api/<StudentController>/5
        // [EnableCors]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var json = System.IO.File.ReadAllText(jsonFile);

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray students = (JArray)jObject["students"];
                    if (students.FirstOrDefault(obj => obj["id"].Value<int>() == id) != null)
                    {
                        return students.FirstOrDefault(obj => obj["id"].Value<int>() == id).ToString();
                    }
                    else
                    {
                        return "NULL";
                    }
                    // return students[index].ToString();
                }
                else
                {
                    Console.WriteLine("jObject is null");
                    // return "NULL";
                    return "NULL";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Exception occured";
            }
        }

        // POST api/<StudentController>
        // [EnableCors]
        [HttpPost]
        public JObject Post([FromBody] StudentDataModel value)
        {
            var json = System.IO.File.ReadAllText(jsonFile);

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray students = (JArray)jObject["students"];
                    var newStudentData = "{ 'id': " + (students.Count + 1) + ", 'name': '" + value.name + "', 'age': " + value.age + ", 'subject': '" + value.subject + "', 'percentage': " + value.percentage + "}";
                    var newStudent = JObject.Parse(newStudentData);
                    students.Add(newStudent);
                    jObject["students"] = students;
                    string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(jsonFile, newJsonResult);
                    return newStudent;
                    // return students[index].ToString();
                }
                else
                {
                    Console.WriteLine("jObject is null");
                    // return "NULL";
                    return new JObject();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new JObject();
            }
        }

        // PUT api/<StudentController>/5
        // [EnableCors]
        [HttpPut("{id}")]
        public JObject Put(int id, [FromBody] StudentDataModel value)
        {
            var json = System.IO.File.ReadAllText(jsonFile);
            var flag = 0;

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray students = (JArray)jObject["students"];
                    if (id > 0)
                    {
                        var name = value.name;
                        var age = value.age;
                        var subject = value.subject;
                        var percentage = value.percentage;

                        foreach (var student in students.Where(obj => obj["id"].Value<int>() == id))
                        {
                            flag = 1;
                            student["name"] = !string.IsNullOrEmpty(name) ? name : "";
                            student["age"] = age;
                            student["subject"] = !string.IsNullOrEmpty(subject) ? subject : "";
                            student["percentage"] = percentage;
                        }

                        jObject["students"] = students;
                        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                        System.IO.File.WriteAllText(jsonFile, output);
                        if (flag == 1)
                        {
                            return new JObject();
                        }
                        else
                        {
                            return new JObject();
                        }
                    }
                    else
                    {
                        Console.Write("Invalid Company ID, Try Again!");
                        return new JObject();
                    }
                }
                else
                {
                    Console.WriteLine("jObject is null");
                    // return "NULL";
                    return new JObject();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new JObject();
            }
        }

        // DELETE api/<StudentController>/5
        // [EnableCors]
        [HttpDelete("{id}")]
        public JObject Delete(int id)
        {
            var json = System.IO.File.ReadAllText(jsonFile);

            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray students = (JArray)jObject["students"];
                    var companyName = string.Empty;
                    var studentToBeDeleted = students.FirstOrDefault(obj => obj["id"].Value<int>() == id);

                    if (studentToBeDeleted == null)
                    {
                        return (JObject)studentToBeDeleted;
                    }

                    students.Remove(studentToBeDeleted);

                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(jsonFile, output);
                    return new JObject();
                    // return students[index].ToString();
                }
                else
                {
                    Console.WriteLine("jObject is null");
                    // return "NULL";
                    return new JObject();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new JObject();
            }
        }
    }
}
