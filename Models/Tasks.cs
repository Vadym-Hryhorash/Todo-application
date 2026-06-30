using System;
namespace Models;
public class Tasks
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; }
    public int CategoryId { get; set; }
    public Categories Category { get; set; }
}