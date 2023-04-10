using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WORKFLOW.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ms_workflow",
                columns: table => new
                {
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    workflowname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_workflow", x => x.workflowcode);
                });

            migrationBuilder.CreateTable(
                name: "md_workflow",
                columns: table => new
                {
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    paramcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    paramname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    paramsexpression = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("md_workflow_PRIMARY", x => new { x.workflowcode, x.paramcode });
                    table.ForeignKey(
                        name: "FK_md_workflow_ms_workflow_workflowcode",
                        column: x => x.workflowcode,
                        principalTable: "ms_workflow",
                        principalColumn: "workflowcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ms_rule",
                columns: table => new
                {
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rulecode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rulename = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    startdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    enddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ms_rule_PRIMARY", x => new { x.workflowcode, x.rulecode });
                    table.ForeignKey(
                        name: "FK_ms_rule_ms_workflow_workflowcode",
                        column: x => x.workflowcode,
                        principalTable: "ms_workflow",
                        principalColumn: "workflowcode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "md_rule_exps",
                columns: table => new
                {
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rulecode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    linenum = table.Column<int>(type: "integer", nullable: false),
                    groupline = table.Column<int>(type: "integer", nullable: false),
                    linkexp = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    paramcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    paramname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    paramsexpression = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("md_rule_exp_PRIMARY", x => new { x.workflowcode, x.rulecode, x.groupline, x.paramcode });
                    table.ForeignKey(
                        name: "FK_md_rule_exps_ms_rule_workflowcode_rulecode",
                        columns: x => new { x.workflowcode, x.rulecode },
                        principalTable: "ms_rule",
                        principalColumns: new[] { "workflowcode", "rulecode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "md_rule_vars",
                columns: table => new
                {
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rulecode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    linenum = table.Column<int>(type: "integer", nullable: false),
                    paramcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    paramname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    paramsexpression = table.Column<string>(type: "character varying(9999999)", maxLength: 9999999, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("md_rule_var_PRIMARY", x => new { x.workflowcode, x.rulecode, x.paramcode });
                    table.ForeignKey(
                        name: "FK_md_rule_vars_ms_rule_workflowcode_rulecode",
                        columns: x => new { x.workflowcode, x.rulecode },
                        principalTable: "ms_rule",
                        principalColumns: new[] { "workflowcode", "rulecode" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "md_rule_exps");

            migrationBuilder.DropTable(
                name: "md_rule_vars");

            migrationBuilder.DropTable(
                name: "md_workflow");

            migrationBuilder.DropTable(
                name: "ms_rule");

            migrationBuilder.DropTable(
                name: "ms_workflow");
        }
    }
}
