using ShiftLogger.Backend.Entities;
using ShiftLogger.Backend.Interfaces;

namespace ShiftLogger.Backend.Endpoints
{
    public static class ShiftEnpoints
    {
        public static void MapShiftEndpoints(this WebApplication app)
        {
            app.MapGet("/api/shifts", async (IShiftService db, CancellationToken ct) =>
            {
                return await db.GetAll(ct);
            });

           
        }
    }
}
