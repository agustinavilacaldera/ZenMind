using ZenMindMAUI.Views.Account;

namespace ZenMindMAUI
{
  public partial class AppShell : Shell
  {
    public AppShell()
    {
      InitializeComponent();
      //Routing.RegisterRoute("//Home/Admin", typeof(Admin));
      //Routing.RegisterRoute("//Home/Doctor", typeof(Doctor));
      //Routing.RegisterRoute("//Test/PerformTest", typeof(PerformTest));
      //Routing.RegisterRoute("//Home/Patients", typeof(Patients));
      //Routing.RegisterRoute("//Account/Users", typeof(Users));
      //Routing.RegisterRoute("//Account/AddOrUpdateUser", typeof(AddOrUpdateUser));
      Routing.RegisterRoute("//Account/Login", typeof(Login));
      //Routing.RegisterRoute("//Account/Settings", typeof(Settings));
      //Routing.RegisterRoute("//Account/ProfileSettings", typeof(ProfileSettings));
      //Routing.RegisterRoute("//Test/TestHome", typeof(TestHome));
      //Routing.RegisterRoute("//Test/Answers", typeof(Answers));
      //Routing.RegisterRoute("//Test/Questions", typeof(Questions));
      //Routing.RegisterRoute("//Test/TakeIndividualTest", typeof(TakeIndividualTest));
      //Routing.RegisterRoute("//Test/AddOrUpdateTest", typeof(AddOrUpdateTest));
      //Routing.RegisterRoute("//Test/AddOrUpdateQuestion", typeof(AddOrUpdateQuestion));
      //Routing.RegisterRoute("//Test/AddOrUpdateAnswer", typeof(AddOrUpdateAnswer));
      //Routing.RegisterRoute("//Test/TestResults", typeof(TestResults));
      //Routing.RegisterRoute("//Doctor/DischargePatient", typeof(DischargePatient));
      //Routing.RegisterRoute("//Admin/Logs", typeof(Logs));
    }
    private async void OnMenuItemClicked(object sender, EventArgs e)
    {
      await Shell.Current.GoToAsync("//Account/Login");
    }
  }
}
