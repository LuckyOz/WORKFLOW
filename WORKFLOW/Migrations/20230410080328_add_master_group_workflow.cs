using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WORKFLOW.Migrations
{
    public partial class add_master_group_workflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ms_groupworkflow",
                columns: table => new
                {
                    groupworkflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    groupworkflowname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_groupworkflow", x => x.groupworkflowcode);
                });

            migrationBuilder.CreateTable(
                name: "md_groupworkflow",
                columns: table => new
                {
                    groupworkflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("md_groupworkflow_PRIMARY", x => new { x.groupworkflowcode, x.username });
                    table.ForeignKey(
                        name: "FK_md_groupworkflow_ms_groupworkflow_groupworkflowcode",
                        column: x => x.groupworkflowcode,
                        principalTable: "ms_groupworkflow",
                        principalColumn: "groupworkflowcode",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "md_groupworkflow");

            migrationBuilder.DropTable(
                name: "ms_groupworkflow");
        }
    }
}
