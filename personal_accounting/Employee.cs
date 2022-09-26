using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personal_accounting
{

    public class Employee
    {
        [Key]
        public int employee_id { get; set; }

        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rights { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string home_address { get; set; }
        public string marital_status { get; set; }
        public string gender { get; set; }
        public Nullable<int> amount_children { get; set; }
        public string data_passport { get; set; }
        public string number_of_telephone { get; set; }
        public Employee() { }
        public Employee(string login, string email, string password, string rights)
        {
            this.login = login;
            this.email = email;
            this.password = password;
            this.rights = rights;
        }

        public Employee(string surname, string name, string patronymic, string login, string email, string password, string rights, DateTime date_of_birth, string home_address, string marital_status, string gender, int amount_children, string data_passport, string number_of_telephone)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.login = login;
            this.email = email;
            this.password = password;
            this.rights = rights;
            this.date_of_birth = date_of_birth;
            this.home_address = home_address;
            this.marital_status = marital_status;
            this.gender = gender;
            this.amount_children = amount_children;
            this.data_passport = data_passport;
            this.number_of_telephone = number_of_telephone;
        }
    }
}
