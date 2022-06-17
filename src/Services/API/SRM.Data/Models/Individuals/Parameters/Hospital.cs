using Fix.Data;
using SRM.Data.Models.HospitalAppointment;
using System.Collections.Generic;

namespace SRM.Data.Models.Individuals.Parameters
{
    public class Hospital : ParameterEntity<int>
    {
        public string Name { get; set; }
        // public ICollection<AppointmentStudent> AppointmentStudents { get; set; }
        public ICollection<AppointmentStudentHospitalRelation> AppointmentStudentHospitalRelations { get; set; }


    }
}
