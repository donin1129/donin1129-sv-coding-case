using System.Globalization;
using SvCodingCase.Application.Common.Interfaces;
using SvCodingCase.Application.TodoLists.Queries.ExportTodos;
using SvCodingCase.Infrastructure.Files.Maps;
using CsvHelper;

namespace SvCodingCase.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
