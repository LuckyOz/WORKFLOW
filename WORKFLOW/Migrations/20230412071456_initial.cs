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
                name: "ms_groupworkflow",
                columns: table => new
                {
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    groupworkflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    groupworkflowname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ms_groupworkflow_PRIMARY", x => new { x.workflowcode, x.groupworkflowcode });
                });

            migrationBuilder.CreateTable(
                name: "ms_user",
                columns: table => new
                {
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_user", x => x.username);
                });

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
                name: "tr_workflow",
                columns: table => new
                {
                    documentnumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    linegroup = table.Column<int>(type: "integer", nullable: false),
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rulecode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    groupworkflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    actworkflow = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descworkflow = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    closedby = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    closeddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tr_workflow_PRIMARY", x => new { x.documentnumber, x.linegroup, x.workflowcode, x.rulecode, x.groupworkflowcode });
                    table.ForeignKey(
                        name: "FK_tr_workflow_ms_groupworkflow_workflowcode_groupworkflowcode",
                        columns: x => new { x.workflowcode, x.groupworkflowcode },
                        principalTable: "ms_groupworkflow",
                        principalColumns: new[] { "workflowcode", "groupworkflowcode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "md_groupworkflow",
                columns: table => new
                {
                    groupworkflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ms_groupworkflowgroupworkflowcode = table.Column<string>(type: "character varying(100)", nullable: true),
                    ms_groupworkflowworkflowcode = table.Column<string>(type: "character varying(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("md_groupworkflow_PRIMARY", x => new { x.groupworkflowcode, x.username });
                    table.ForeignKey(
                        name: "FK_md_groupworkflow_ms_groupworkflow_ms_groupworkflowworkflowc~",
                        columns: x => new { x.ms_groupworkflowworkflowcode, x.ms_groupworkflowgroupworkflowcode },
                        principalTable: "ms_groupworkflow",
                        principalColumns: new[] { "workflowcode", "groupworkflowcode" });
                    table.ForeignKey(
                        name: "FK_md_groupworkflow_ms_user_username",
                        column: x => x.username,
                        principalTable: "ms_user",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
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
                name: "md_rule_rslt",
                columns: table => new
                {
                    workflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rulecode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    linenum = table.Column<int>(type: "integer", nullable: false),
                    linegroup = table.Column<int>(type: "integer", nullable: false),
                    groupworkflowcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    actworkflow = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descworkflow = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("md_rule_rsl_PRIMARY", x => new { x.workflowcode, x.rulecode, x.linenum });
                    table.ForeignKey(
                        name: "FK_md_rule_rslt_ms_rule_workflowcode_rulecode",
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

            migrationBuilder.CreateIndex(
                name: "IX_md_groupworkflow_ms_groupworkflowworkflowcode_ms_groupworkf~",
                table: "md_groupworkflow",
                columns: new[] { "ms_groupworkflowworkflowcode", "ms_groupworkflowgroupworkflowcode" });

            migrationBuilder.CreateIndex(
                name: "IX_md_groupworkflow_username",
                table: "md_groupworkflow",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_tr_workflow_workflowcode_groupworkflowcode",
                table: "tr_workflow",
                columns: new[] { "workflowcode", "groupworkflowcode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "md_groupworkflow");

            migrationBuilder.DropTable(
                name: "md_rule_exps");

            migrationBuilder.DropTable(
                name: "md_rule_rslt");

            migrationBuilder.DropTable(
                name: "md_rule_vars");

            migrationBuilder.DropTable(
                name: "md_workflow");

            migrationBuilder.DropTable(
                name: "tr_workflow");

            migrationBuilder.DropTable(
                name: "ms_user");

            migrationBuilder.DropTable(
                name: "ms_rule");

            migrationBuilder.DropTable(
                name: "ms_groupworkflow");

            migrationBuilder.DropTable(
                name: "ms_workflow");
        }
    }
}
