//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace personal_accounting
{
    using System;
    using System.Collections.Generic;
    
    public partial class Info_employees
    {
        public int id_info_employee { get; set; }
        public Nullable<int> position_id { get; set; }
        public Nullable<int> departament_id { get; set; }
        public int employee_id { get; set; }
        public Nullable<System.DateTime> reception_date { get; set; }
        public Nullable<System.DateTime> dismissal_date { get; set; }
        public Nullable<System.DateTime> start_activity { get; set; }
        public Nullable<bool> disability { get; set; }
        public Nullable<System.DateTime> disability_date { get; set; }
        public string rate_work { get; set; }
        public Nullable<decimal> salary { get; set; }
    
        public virtual Departaments Departaments { get; set; }
        public virtual Employees Employees { get; set; }
        public virtual Positions Positions { get; set; }
    }
}
