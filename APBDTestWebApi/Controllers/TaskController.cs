using APBDTestWebApi.Contracts.Responses;
using APBDTestWebApi.Entities;
using APBDTestWebApi.Mappers;
using APBDTestWebApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBDTestWebApi.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController(ITeamMemberRepository teamMemberRepository, ITaskRepository taskRepository)
    : ControllerBase
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly ITeamMemberRepository _teamMemberRepository = teamMemberRepository;

    [HttpGet("{id:int}")]
    [ProducesResponseType<GetTaskResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTasksOfMember([FromRoute] int id, CancellationToken ct)
    {
        if (id < 0)
        {
            return BadRequest("Id should be bigger than 0");
        }
        
        TeamMember? teamMember = await _teamMemberRepository.GetTeamMember(id, ct);

        if (teamMember == null)
        {
            return NotFound("Team member with this id doesn't exist");
        }
        
        var tasksAssigned = await _taskRepository.GetTasksAssignedTo(id, ct);
        var tasksCreatedBy = await _taskRepository.GetTasksCreatedBy(id, ct);

        foreach (var taskEntity in tasksCreatedBy!)
        {
            if (!tasksAssigned!.Contains(taskEntity))
            {
                tasksAssigned.Add(taskEntity);
            }
        }

        if (tasksAssigned != null) return Ok(tasksAssigned.MapToSample());
        return NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProject([FromRoute] int id, CancellationToken ct)
    {
        if (id < 0)
        {
            return BadRequest("Id should be bigger than 0");
        }
        
        var result = await _taskRepository.DeleteDataAboutProject(id, ct);

        if (result.success == false)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result.message);
        }

        return Ok();
    }
}