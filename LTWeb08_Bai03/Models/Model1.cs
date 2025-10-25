using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace LTWeb08_Bai03.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tbl_Deparment> Deparment { get; set; }
        public virtual DbSet<tbl_Employee> Employee { get; set; }
        public void InsertOnSubmit(tbl_Deparment deparment)
        {
            Deparment.Add(deparment);
        }
        public void UpdateOnSubmit(tbl_Deparment deparment)
        {
            Deparment.AddOrUpdate(deparment);
        }
        public void DeleteOnSubmit(tbl_Deparment deparment)
        {
            Deparment.Remove(deparment);
        }
        public void SubmitChanges()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
