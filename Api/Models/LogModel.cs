using System.Text.Json.Serialization;

namespace Api.Models;

public class LogModel
{
    public DateOnly Date { get; set; } = default!;
    public TimeOnly Time { get; set; } = default!;
    public String Ip { get; set; } = default!;
    public String Action { get; set; } = default!;
    public String Status { get; set; } = default!;

    public override string ToString() => $"{Date:yyyy-MM-dd} | {Time:hh-mm-ss} | {Ip} | {Action} | {Status}";
}