using System;
using ZenMind.DAL;
using ZenMind.DAL.DB;

namespace ZenMind.BLL
{
  public class UserBLL
  {
    public ResultModel<Boolean> CheckLogin(MUser user)
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.CheckLogin(user);
    }

    public ResultModel<Boolean> SaveUser(MUser User)
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.SaveUser(User);
    }
    public ResultModel<Boolean> CreateDefaultUser()
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.CreateDefaultUser();
    }
    public ResultModel<Boolean> DeleteUser(Int64 UserId)
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.DeleteUser(UserId);
    }

    public ResultModel<Boolean> UpdateProfileSettings(MUser User)
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.UpdateProfileSettings(User);
    }
    public ResultModel<bool> GetUsers()
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.GetUsers();
    }

    public ResultModel<Boolean> GetUsers(MUser User)
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.GetUsers(User);
    }

    public ResultModel<Boolean> GetPatientUsersForDischarge(MUser user)
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.GetPatientUsersForDischarge(user);
    }

    public ResultModel<Boolean> CheckUserExists(MUser usr)
    {
      UserDAL objUserDAL = new UserDAL();
      return objUserDAL.CheckUserExists(usr);
    }
  }
}
