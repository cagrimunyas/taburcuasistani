using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
namespace Zirve.Models
{
    [Table("hastalar")]
    public class hasta
    {
        [Key]
        public int id { get; set; }

        public int age { get; set; }
        public string gender { get; set; }
        public string race { get; set; }
        public int admisson_source_id { get; set; }
        public int admission_type_id { get; set; }
        public int number_emergency { get; set; }
        public int number_inpatient { get; set; }
        public int number_outpatient { get; set; }
        public int num_lab_procedures { get; set; }
        public int num_medications { get; set; }
        public int num_procedures { get; set; }
        public int number_diagnoses { get; set; }
        public string diag_1 { get; set; }
        public int time_in_hospital { get; set; }
        public int discharge_disposition_id { get; set; }
        public string readmitted_dttm { get; set; }
        public string Scored_Labels { get; set; }
        public float Scored_Probabilities { get; set; }
    }
    public class hastaDBContext : DbContext
    {
        public DbSet<hasta> Hasta { get; set; }
    }
}