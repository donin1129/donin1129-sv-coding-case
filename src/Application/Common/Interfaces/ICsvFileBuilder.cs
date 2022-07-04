using SvCodingCase.Application.TodoLists.Queries.ExportTodos;

namespace SvCodingCase.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
