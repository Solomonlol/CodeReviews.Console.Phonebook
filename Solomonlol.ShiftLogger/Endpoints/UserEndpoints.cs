using ShiftLogger.Backend.Entities;
using ShiftLogger.Backend.Interfaces;

namespace ShiftLogger.Backend.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/api/users", async (IDbService<User> db, CancellationToken ct) =>
            {
                return await db.GetAll(ct);
            });

            app.MapGet("/api/users/{id}", async (int id, IDbService<User> db, CancellationToken ct) =>
            {
                return await db.GetById(id, ct)
                    is User user ? Results.Ok(user) : Results.NotFound();
            });

            app.MapPost("/api/users", async(User user, IDbService<User> db, CancellationToken ct) =>
            {
                await db.Create(user, ct);
            });
        }
    }
}
