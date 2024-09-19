using System;
using ZenMind.DAL.DB;
using ZenMind.DAL;

namespace ZenMind.BLL
{
    public class PatientDischargeBLL
    {
        public ResultModel<Boolean> SavePatientDischarge(TPatientDischarge PatientDischarge)
        {
            PatientDischargeDAL patientDischargeDAL = new PatientDischargeDAL();
            return patientDischargeDAL.SavePatientDischarge(PatientDischarge);
        }
    }
}
