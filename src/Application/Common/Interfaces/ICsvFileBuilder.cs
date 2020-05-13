using tti_graduation_work.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace tti_graduation_work.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
