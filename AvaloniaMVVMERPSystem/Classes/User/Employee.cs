﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaMVVMERPSystem.Classes.User
{
    public class Employee : Person
    {
        public int EmployeeId { get; set; }
        public string EmployeePassword { get; set; }
        public string Title { get; set; }
        public string WorkMail { get; set; }
        public string WorkTlf { get; set; }
        public string AdminPassword { get; set; }

        public Employee(int employeeId, string emplyeePassword, string title, string workMail, string workTlf
            ,string adminPassword,int personId, string firstName, string lastName, string cprNumber, PersonaLInfo pInfo) :
            base(personId, firstName, lastName, cprNumber, pInfo) 
        {
            EmployeeId = employeeId;
            EmployeePassword = emplyeePassword;
            Title = title;
            WorkMail = workMail;
            WorkTlf = workTlf;
            AdminPassword = adminPassword;
        }
    }
}
