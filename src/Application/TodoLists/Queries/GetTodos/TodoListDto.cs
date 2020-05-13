using tti_graduation_work.Application.Common.Mappings;
using tti_graduation_work.Domain.Entities;
using System.Collections.Generic;

namespace tti_graduation_work.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public IList<TodoItemDto> Items { get; set; }
}
}
