using System;
using ZenMind.DAL.DB;
using ZenMind.DAL;

namespace ZenMind.BLL
{
    public class UserTestBLL
    {
        public ResultModel<Boolean> SaveUserTest(TUserTest UserTest)
        {
            UserTestDAL UserTestDAL = new UserTestDAL();
            return UserTestDAL.SaveUserTest(UserTest);
        }

        public ResultModel<Boolean> DeleteUserTest(Int64 UserTestId)
        {
            UserTestDAL UserTestDAL = new UserTestDAL();
            return UserTestDAL.DeleteUserTest(UserTestId);
        }

        public ResultModel<Boolean> GetUserTests(TUserTest UserTest)
        {
            UserTestDAL UserTestDAL = new UserTestDAL();
            return UserTestDAL.GetUserTests(UserTest);
        }
    }
}
