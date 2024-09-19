using SQLite;
using System.Data;
using ZenMind.DAL.DB;

namespace ZenMind.DAL
{
  public class UserDAL
  {

    #region Methods

    public ResultModel<Boolean> CheckLogin(MUser user)
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();
      //DataTable dt;

      try
      {
        SQLiteConnection con = SqlConnection.GetConnection();

        var Users = (from x in con.Table<MUser>() where x.Email.Equals(user.Email) && x.Password.Equals(user.Password) select x).ToList();

        if (Users.Count > 0)
        {
          result.ok = true;
          result.msg = "Login Successful";
          result.Users = Users;
        }
        else
        {
          result.ok = false;
          result.msg = "Error while login.";
        }
      }
      catch (Exception ex)
      {
        result.ok = false;
        result.msg = "Error while login." + ex.Message;
      }

      return result;
    }

    public ResultModel<Boolean> SaveUser(MUser User)
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();
      Int64 res = 0;

      try
      {
        //string query = "";

        SQLiteConnection con = SqlConnection.GetConnection();

        if (User.UserId == 0)
        {
          res = con.Insert(User);
        }
        else
        {
          res = con.InsertOrReplace(User);
        }

        if (res > 0)
        {
          result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
          result.ok = true;
          result.msg = "User account has been created";
        }
        else
        {
          result.ok = false;
          result.msg = "Error while creating User account";
        }
      }
      catch (Exception ex)
      {
        result.ok = false;
        result.msg = "Error while creating User account" + ex.Message;
      }

      return result;
    }

    public ResultModel<Boolean> CreateDefaultUser()
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();
      Int64 res = 0;

      try
      {
        //string query = "";

        SQLiteConnection con = SqlConnection.GetConnection();

        con.CreateTable<MUserType>();
        con.CreateTable<MUser>();
        con.CreateTable<MAnswer>();
        con.CreateTable<MQuestion>();
        con.CreateTable<MTest>();
        con.CreateTable<TUserTest>();
        con.CreateTable<TTestAnswers>();
        con.CreateTable<TPatientDischarge>();
        con.CreateTable<TLogs>();
        con.CreateTable<TNotificationsSent>();

        var defaultUserExist = (from x in con.Table<MUser>()
                                where
                               (x.Email == "admin@gmail.com" && x.Password == "admin")
                                select x).ToList();
        if (defaultUserExist == null)
        {
          SaveDefaultUser(out result, out res, out con);
        }
        else
        {
          if (defaultUserExist.Count <= 0)
          {
            SaveDefaultUser(out result, out res, out con);
          }
          else
          {
            result.AlreadyExists = true;
          }
        }

      }
      catch (Exception ex)
      {
        result.ok = false;
        result.msg = "Error while creating Default User account" + ex.Message;
      }

      return result;
    }

    private void SaveDefaultUser(out ResultModel<Boolean> result, out Int64 res, out SQLiteConnection con)
    {
      result = new ResultModel<bool>();
      con = SqlConnection.GetConnection();

      MUser usr = new MUser(); //User Data object class object creation
      usr.UserId = 1;
      usr.UserType = 1;
      usr.Email = "admin@gmail.com";//Sets values for pass to the API
      usr.Password = "admin";
      usr.Name = "admin";

      res = con.Insert(usr);
      con = null;

      if (res > 0)
      {
        result.IdentityId = SQLite3.LastInsertRowid(con.Handle);
        result.ok = true;
        result.msg = "User account has been created";
      }
      else
      {
        result.ok = false;
        result.msg = "Error while creating Default User account";
      }
    }

    public ResultModel<Boolean> UpdateProfileSettings(MUser User)
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();
      Int64 res = 0;

      try
      {
        //string query = "";

        SQLiteConnection con = SqlConnection.GetConnection();

        //  var usr = con.Query<MUser>($"UPDATE MUser SET Name='" + User.Name + "' WHERE UserId = '" + User.UserId + "'");

        var usr = (from x in con.Table<MUser>()
                   where
                  (x.UserId == User.UserId)
                   select x).ToList();

        usr[0].Name = User.Name;
        usr[0].AllowEmailNotifications = User.AllowEmailNotifications;
        usr[0].PlayBackgroundMusic = User.PlayBackgroundMusic;

        res = con.Update(usr[0]);

        if (res > 0)
        {
          result.IdentityId = res;
          result.ok = true;
          result.msg = "Name has been updated";
        }
        else
        {
          result.ok = false;
          result.msg = "Error while updating Name";
        }
      }
      catch (Exception ex)
      {
        result.ok = false;
        result.msg = "Error while updating Name" + ex.Message;
      }

      return result;
    }

    public ResultModel<Boolean> DeleteUser(Int64 UserId)
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();

      Int64 res = 0;

      try
      {
        SQLiteConnection con = SqlConnection.GetConnection();
        res = con.Delete(UserId);

        if (res > 0)
        {
          result.ok = true;

          result.msg = "User has been deleted";
        }
        else
        {
          result.ok = false;
          result.msg = "Error while deleting User";
        }
      }
      catch (Exception ex)
      {
        result.ok = false;
        result.msg = "Error while deleting User " + ex.Message;
      }

      return result;
    }

    public ResultModel<Boolean> GetUsers(MUser user)
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();

      try
      {
        SQLiteConnection con = SqlConnection.GetConnection();
        var Users = (from x in con.Table<MUser>()
                     where (x.UserType == user.UserType || user.UserType == 0 )
                      && (x.UserId == user.UserId || user.UserId == 0 )
                     select x).ToList();

        if (Users.Count > 0)
        {
          result.ok = true;
          result.Users = Users;
          result.msg = "";
        }
        else
        {
          result.ok = false;
          result.msg = "";
        }
      }
      catch (Exception ex)
      {
        result.ok = false;
        result.msg = "Error while getting User. " + ex.Message;
      }

      return result;
    }

    public ResultModel<Boolean> GetPatientUsersForDischarge(MUser user)
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();

      try
      {
        List<MUser> users = new List<MUser>();

        SQLiteConnection con = SqlConnection.GetConnection();
        var Users = (from x in con.Table<MUser>()
                     join pd in con.Table<TPatientDischarge>()
                     on x.UserId equals pd.PatientUserId into pdDetails
                     from patientDischargeDet in pdDetails.DefaultIfEmpty()
                     where (x.UserType == user.UserType || user.UserType == 0)
                      && (x.UserId == user.UserId || user.UserId == 0 )
                     //&&(x.UserId!=patientDischargeDet.PatientUserId)
                     select x).ToList();


        if (Users.Count > 0)
        {
          foreach (MUser usr in Users)
          {
            var PatientDischarged = (from pd in con.Table<TPatientDischarge>()
                                     where (pd.PatientUserId == usr.UserId)
                                     select pd).ToList();

            if (PatientDischarged.Count <= 0)
            {
              users.Add(usr);
            }
          }

          result.ok = true;
          result.Users = users;
          result.msg = "";
        }
        else
        {
          result.ok = false;
          result.msg = "";
        }
      }
      catch (Exception ex)
      {
        result.ok = false;
        result.msg = "Error while getting User. " + ex.Message;
      }

      return result;
    }

    public ResultModel<Boolean> CheckUserExists(MUser user)
    {
      ResultModel<Boolean> result = new ResultModel<Boolean>();
      //DataTable dt;

      try
      {
        SQLiteConnection con = SqlConnection.GetConnection();

        var Users = (from x in con.Table<MUser>() where x.Email.Equals(user.Email) select x).ToList();

        if (Users.Count > 0)
        {
          result.ok = true;
          result.msg = "User already exists!";
          result.AlreadyExists = true;
        }
        else
        {
          result.AlreadyExists = false;
          result.ok = false;
          result.msg = "";
        }
      }
      catch (Exception ex)
      {
        result.AlreadyExists = false;
        result.ok = false;
        result.msg = "Error while getting User. " + ex.Message;
      }

      return result;
    }

    #endregion
  }
}
