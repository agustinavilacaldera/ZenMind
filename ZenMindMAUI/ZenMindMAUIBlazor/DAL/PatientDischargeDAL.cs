using System;
using SQLite;
using ZenMind.DAL.DB;

namespace ZenMind.DAL
{
    public class PatientDischargeDAL
    {
        public ResultModel<Boolean> SavePatientDischarge(TPatientDischarge PatientDischarge)
        {
            ResultModel<Boolean> result = new ResultModel<Boolean>();
            Int64 res = 0;

            try
            {              
                SQLiteConnection con = SqlConnection.GetConnection();

                if (PatientDischarge.PatientDischargeId == 0 )
                {
                    res = con.Insert(PatientDischarge);
                }
                else
                {
                    res = con.InsertOrReplace(PatientDischarge);
                }

                if (res > 0)
                {
                    result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
                    result.ok = true;
                    result.msg = "Patient has been Discharged";
                }
                else
                {
                    result.ok = false;
                    result.msg = "Error while Discharging Patient";
                }
            }
            catch (Exception ex)
            {
                result.ok = false;
                result.msg = "Error while Discharging Patient" + ex.Message;
            }

            return result;
        }
    }
}
