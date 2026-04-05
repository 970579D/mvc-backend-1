using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mvc_backend_1.Models;

public class Reserve
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    public int QueueNo { get; set; }
    public string? QueueLabel { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Date { get; set; }
}
