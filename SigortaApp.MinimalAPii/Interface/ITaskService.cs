namespace SigortaApp.MinimalAPii;

public interface ITaskService
{
    Task<List<Task>> GetAllUsers();
    Task<Task?> GetUserDetail(int id);
    Task<Task?> CreateUser(Task model);
    Task<Task?> UpdateUser(Task model);
    Task<Task?> DeleteUser(int id);

}
