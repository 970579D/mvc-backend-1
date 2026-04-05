using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_backend_1.Models;
using mvc_backend_1.Context;
using Microsoft.EntityFrameworkCore;

namespace mvc_backend_1.Service;

public class ReserveServiceImpl : ReserveService
{

    private readonly ReserveContext _reserveContext;

    public ReserveServiceImpl(ReserveContext reserveContext)
    {
        _reserveContext = reserveContext;
    }

    public async Task<ResponseModel<Reserve>> GenerateQueue()
    {
        Reserve input;
        Reserve last = await _findLastQueue();
        
        if (last == null)
        {
            input = new Reserve() { QueueNo = 1, QueueLabel = "A1" };
        }
        else
        {
            int newQueueNo = last.QueueNo + 1;
            input = new Reserve()
            {
                QueueNo = newQueueNo,
                QueueLabel = _generateQueueLabel(newQueueNo)
            };

        }
        _save(input);

        IEnumerable<Reserve> resultList = _reserveContext.reserve.Where(e => e.QueueNo == input.QueueNo);
        return _Response(resultList.FirstOrDefault(), Constants.StringConstants.STATUS_SUCCESS);
    }

    public async Task<ResponseModel<Reserve>> FindLastQueue()
    {
        Reserve result = await _findLastQueue();
        return _Response(result, Constants.StringConstants.STATUS_SUCCESS);
    }

    public async Task<ResponseModel<bool>> CheckExistQueue()
    {
        bool result = _reserveContext.reserve.Count() > 0;
        return _Response(result, Constants.StringConstants.STATUS_SUCCESS);
    }

    public ResponseModel<bool> ClearQueue()
    {
        _reserveContext.reserve.RemoveRange(_reserveContext.reserve);
        _reserveContext.SaveChanges();
        return _Response(true, Constants.StringConstants.STATUS_SUCCESS);
    }

    public ResponseModel<T> _Response<T>(T? data, string status, string message = "")
    {
        return new ResponseModel<T>()
        {
            Message = message,
            Status = status,
            Data = data
        };
    }

    public async Task<Reserve> _findLastQueue()
    {
        IEnumerable<Reserve> result = await _reserveContext.reserve.ToListAsync();
        if(result.Count() == 0) return null;

        int maxQueueNo = result.Max(e => e.QueueNo);
        Reserve last = result.Where(e => e.QueueNo == maxQueueNo)?.FirstOrDefault();
        return last;
    }

    public void _save(Reserve input)
    {
        _reserveContext.reserve.Add(input);
        _reserveContext.SaveChanges();
    }

    private static string _generateQueueLabel(int queueNo)
    {
        int queueIndex = queueNo - 1;
        int findCharIndex = (int)Math.Floor(queueIndex / 9.0);
        string findChar = Constants.ArrayConstants.CHAR_ARR[findCharIndex];
        int findNoThisChar = Constants.ArrayConstants.NO_ARR[queueIndex % 9];
        return findChar + findNoThisChar.ToString();
    }
}
