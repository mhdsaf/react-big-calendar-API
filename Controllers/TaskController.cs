using System;
using System.Collections.Generic;
using System.Linq;
using calendar_api.Data;
using calendar_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace calendar_api.Controllers
{
    [ApiController]
    [Route("")]
    public class TaskController : ControllerBase
    {
        private readonly DataContext data;
        public TaskController(DataContext data)
        {
            this.data = data;
        }

        [HttpGet("tasks")] //YYYY-MM-DD string str = DateTime.Now.ToString("yyyy/MM/dd");
        public IActionResult getTasks(string assignedTo, string dueDate){
            List<Task> tasks = new List<Task>();
            
            // both filters are used
            if(!string.IsNullOrEmpty(assignedTo) && !string.IsNullOrEmpty(dueDate)){
                string[] assignedToList = assignedTo.Split(",");
                if(dueDate=="today")
                    tasks = data.task_data.Where(c => assignedToList.Contains(c.AssignedTo) && c.EndDate==DateTime.Now.Date).ToList();
                else if(dueDate=="tomorrow")
                    tasks = data.task_data.Where(c => assignedToList.Contains(c.AssignedTo) && c.EndDate==DateTime.Now.Date.AddDays(1)).ToList();
                else if(dueDate=="this week"){
                    getFirstAndLastDayOfWeek(DateTime.Now, out DateTime firstDayOfWeek, out DateTime lastDayOfWeek);
                    tasks = data.task_data.Where(c => assignedToList.Contains(c.AssignedTo) && c.EndDate>=firstDayOfWeek && c.EndDate<=lastDayOfWeek).ToList();
                }
                else if(dueDate=="this month"){
                    getFirstAndLastDayOfMonth(DateTime.Now, out DateTime firstDayOfMonth, out DateTime lastDayOfMonth);
                    tasks = data.task_data.Where(c => assignedToList.Contains(c.AssignedTo) && c.EndDate>=firstDayOfMonth && c.EndDate<=lastDayOfMonth).ToList();
                }
                return Ok(tasks);
            }

            // only name filter is used
            else if(!string.IsNullOrEmpty(assignedTo) && string.IsNullOrEmpty(dueDate)){
                string[] assignedToList = assignedTo.Split(",");
                tasks = data.task_data.Where(c => assignedToList.Contains(c.AssignedTo)).ToList();
                return Ok(tasks);
            }
            
            // only date filter is used
            else if(!string.IsNullOrEmpty(dueDate) && string.IsNullOrEmpty(assignedTo)){
                if(dueDate=="today")
                    tasks = data.task_data.Where(c => c.EndDate==DateTime.Now.Date).ToList();
                else if(dueDate=="tomorrow")
                    tasks = data.task_data.Where(c => c.EndDate==DateTime.Now.Date.AddDays(1)).ToList();
                else if(dueDate=="this week"){
                    getFirstAndLastDayOfWeek(DateTime.Now, out DateTime firstDayOfWeek, out DateTime lastDayOfWeek);
                    tasks = data.task_data.Where(c => c.EndDate>=firstDayOfWeek && c.EndDate<=lastDayOfWeek).ToList();
                }
                else if(dueDate=="this month"){
                    getFirstAndLastDayOfMonth(DateTime.Now, out DateTime firstDayOfMonth, out DateTime lastDayOfMonth);
                    tasks = data.task_data.Where(c => c.EndDate>=firstDayOfMonth && c.EndDate<=lastDayOfMonth).ToList();
                }
                return Ok(tasks);
            }

            // no filters
            else{
                tasks = data.task_data.ToList();
                return Ok(tasks);   
            }
        }
    
        [HttpGet("names")]
        public IActionResult getNames(){
            List<string> names = new List<string>();
            names = data.task_data.Select(c => c.AssignedTo).Distinct().ToList();
            return Ok(names);
        }

        [HttpPost("task")]
        public IActionResult updateTasks(List<Task> tasks){
            System.Console.WriteLine("adbkjb");
            System.Console.WriteLine(tasks[0]);
            return Ok("Asd");
        }

        private void getFirstAndLastDayOfWeek(DateTime date, out DateTime firstDayOfWeek, out DateTime lastDayOfWeek){
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = DateTime.Now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;

            firstDayOfWeek = date.AddDays(-diff).Date.AddDays(1);
            lastDayOfWeek = date.AddDays(-diff).Date.AddDays(7);
        }

        private void getFirstAndLastDayOfMonth (DateTime date, out DateTime firstDayOfMonth, out DateTime lastDayOfMonth){
            var month = new DateTime(date.Year, date.Month, 1);
            firstDayOfMonth = month;
            lastDayOfMonth = month.AddMonths(1).AddDays(-1);
        }
    }
    
}

/*
DateTime date = DateTime.Now;
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = DateTime.Now.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;

            var firstDayOfWeek = date.AddDays(-diff).Date;
            var lastDayOfWeek = date.AddDays(-diff).Date.AddDays(6);
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            System.Console.WriteLine(firstDayOfWeek.ToString("yyyy/MM/dd"));
            System.Console.WriteLine(lastDayOfWeek.ToString("yyyy/MM/dd"));
            System.Console.WriteLine(firstDayOfMonth.ToString("yyyy/MM/dd"));
            System.Console.WriteLine(lastDayOfMonth.ToString("yyyy/MM/dd"));


            DateTime date = DateTime.Now;
            getFirstAndLastDayOfWeek(date, out string firstDayOfWeek, out string lastDayOfWeek);
            getFirstAndLastDayOfMonth (date, out string firstDayOfMonth, out string lastDayOfMonth);
            System.Console.WriteLine(firstDayOfWeek);
            System.Console.WriteLine(lastDayOfWeek);
            System.Console.WriteLine(firstDayOfMonth);
            System.Console.WriteLine(lastDayOfMonth);
*/