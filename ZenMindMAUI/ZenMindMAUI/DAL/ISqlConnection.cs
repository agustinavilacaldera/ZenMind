
using SQLite;

namespace ZenMind.DAL
{
    public interface ISqlConnection
    {
        SQLiteAsyncConnection Connection();
    }
}
