using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WORKFLOW.Migrations
{
    public partial class update_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "md_rule_exp_PRIMARY",
                table: "md_rule_exps");

            migrationBuilder.AddPrimaryKey(
                name: "md_rule_exp_PRIMARY",
                table: "md_rule_exps",
                columns: new[] { "workflowcode", "rulecode", "linenum", "groupline", "paramcode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "md_rule_exp_PRIMARY",
                table: "md_rule_exps");

            migrationBuilder.AddPrimaryKey(
                name: "md_rule_exp_PRIMARY",
                table: "md_rule_exps",
                columns: new[] { "workflowcode", "rulecode", "groupline", "paramcode" });
        }
    }
}
