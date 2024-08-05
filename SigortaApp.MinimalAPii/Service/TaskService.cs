
namespace SigortaApp.MinimalAPii;

public class TaskService : ITaskService
{
    private readonly DbCoreContext _context;
    public TaskService(DbCoreContext context)
    {
        this._context = context;
    }

    public async Task<List<Task>> GetAllUsers()
    {
        var users = _context.Task.ToList();
        return await System.Threading.Tasks.Task.FromResult(users.ToList());
    }
    public async Task<Task?> GetUserDetail(int id)
    {
        var users = await _context.Task.FindAsync(id);
        return users ?? null;
    }
    public async Task<Task?> CreateUser(Task model)
    {
        var user = _context.Task.Add(model);
        await _context.SaveChangesAsync();
        return await GetUserDetail(model.Id) ?? null;
    }
    public async Task<Task?> UpdateUser(Task model)
    {
        var user = await GetUserDetail(model.Id);
        if (user == null)
            return null;
        _context.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }
    public async Task<Task?> DeleteUser(int id)
    {
        var user = await GetUserDetail(id);
        if (user == null)
            return null;
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
}
