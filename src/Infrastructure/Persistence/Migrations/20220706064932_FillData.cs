using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using SvCodingCase.Domain.Entities;

#nullable disable

namespace SvCodingCase.Infrastructure.Persistence.Migrations;

public partial class FillData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        using (StreamReader r = new StreamReader("InitialData/sv_lsm_data.json"))
        {
            string json = r.ReadToEnd();

            Dictionary<string, object> parsed = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            var buildingData = parsed["buildings"].ToString().Replace("\"id\"", "\"Id\"").Replace("\"shortCut\"", "\"ShortCut\"").Replace("\"name\"", "\"Name\"").Replace("\"description\"", "\"Description\"");
            var lockData = parsed["locks"].ToString().Replace("\"id\"", "\"Id\"").Replace("\"buildingId\"", "\"BuildingId\"").Replace("\"type\"", "\"Type\"").Replace("\"name\"", "\"Name\"").Replace("\"description\"", "\"Description\"").Replace("\"serialNumber\"", "\"SerialNumber\"").Replace("\"floor\"", "\"Floor\"").Replace("\"roomNumber\"", "\"RoomNumber\"").Replace("\"Cylinder\"", "\"cylinder\"").Replace("\"SmartHandle\"", "\"smart_handle\"");
            var groupData = parsed["groups"].ToString().Replace("\"id\"", "\"Id\"").Replace("\"name\"", "\"Name\"").Replace("\"description\"", "\"Description\"");
            var mediaData = parsed["media"].ToString().Replace("\"id\"", "\"Id\"").Replace("\"groupId\"", "\"GroupId\"").Replace("\"type\"", "\"Type\"").Replace("\"owner\"", "\"Owner\"").Replace("\"description\"", "\"Description\"").Replace("\"serialNumber\"", "\"SerialNumber\"").Replace("\"Card\"", "\"card\"").Replace("\"TransponderWithCardInlay\"", "\"transponder_with_card_inlay\"").Replace("\"Transponder\"", "\"transponder\"").Replace("'", "''");

            migrationBuilder.Sql($"insert into \"Buildings\" select * from json_populate_recordset (null::\"Buildings\", \'{buildingData}\')");
            migrationBuilder.Sql($"insert into \"Locks\" select * from json_populate_recordset (null::\"Locks\", \'{lockData}\')");
            migrationBuilder.Sql($"insert into \"Groups\" select * from json_populate_recordset (null::\"Groups\", \'{groupData}\')");
            migrationBuilder.Sql($"insert into \"Media\" select * from json_populate_recordset (null::\"Media\", \'{mediaData}\')");
        }
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
