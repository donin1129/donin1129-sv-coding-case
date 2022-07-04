using SvCodingCase.Application.Common.Mappings;
using SvCodingCase.Domain.Entities;

namespace SvCodingCase.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
