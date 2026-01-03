using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace dotnet10.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task Get([FromRoute] int id, CancellationToken ct)
    {
        var studentEntity = await dbContext.Students.FindAsync([id], ct);

        if (studentEntity is null)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "application/json";
        await Response.BodyWriter.WriteAsync(studentEntity.Document, ct);
    }

    [HttpPost]
    public async Task Create([FromBody] Student student, CancellationToken ct)
    {
        var studentBytes = JsonSerializer.SerializeToUtf8Bytes(student);
        var studentEntity = new StudentEntity { Document = studentBytes };
        dbContext.Students.Add(studentEntity);
        await dbContext.SaveChangesAsync(ct);

        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "application/json";
        await Response.BodyWriter.WriteAsync(studentEntity.Document, ct);
    }

    [HttpPut("{id:int}")]
    public async Task Update([FromRoute] int id, [FromBody] Student student, CancellationToken ct)
    {
        var studentEntity = await dbContext.Students.FindAsync([id], ct);
        if (studentEntity is null)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        studentEntity.Document = JsonSerializer.SerializeToUtf8Bytes(student);

        dbContext.Students.Update(studentEntity);
        await dbContext.SaveChangesAsync(ct);

        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "application/json";
        await Response.BodyWriter.WriteAsync(studentEntity.Document, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Update([FromRoute] int id, CancellationToken ct)
    {
        var studentEntity = await dbContext.Students.FindAsync([id], ct);
        if (studentEntity is null)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        dbContext.Students.Remove(studentEntity);
        await dbContext.SaveChangesAsync(ct);

        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "application/json";
        await Response.BodyWriter.WriteAsync(studentEntity.Document, ct);
    }
}