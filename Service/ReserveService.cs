using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_backend_1.Models;
using Microsoft.EntityFrameworkCore;

namespace mvc_backend_1.Service;

public interface ReserveService
{
    public Task<ResponseModel<Reserve>> GenerateQueue();
    public Task<ResponseModel<Reserve>> FindLastQueue();
    public Task<ResponseModel<bool>> CheckExistQueue();
    public ResponseModel<bool> ClearQueue();
}
