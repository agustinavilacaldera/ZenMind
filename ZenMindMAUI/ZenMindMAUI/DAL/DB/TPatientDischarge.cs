using SQLite;
using System;

namespace ZenMind.DAL.DB
{
    public  class TPatientDischarge
    {
        [PrimaryKey, AutoIncrement]
        public int PatientDischargeId { get; set; }
        public DateTime DischargeDate { get; set; }
        public int PatientUserId { get; set; }
        public int DoctorUserId { get; set; }//Discharged Doctor's UserId
    }
}
