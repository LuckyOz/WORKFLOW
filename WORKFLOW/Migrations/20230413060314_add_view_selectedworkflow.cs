using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WORKFLOW.Migrations
{
    public partial class add_view_selectedworkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW v_selectedworkflow AS\r\nSELECT \r\ntWf.documentnumber\r\n,tWf.linegroup\r\n,tWf.workflowcode\r\n,tWf.rulecode\r\n,tWf.groupworkflowcode\r\n,msGwf.groupworkflowname\r\n,tWf.actworkflow\r\n,tWf.descworkflow\r\n,mdGwf.username\r\n,tWf.closedby\r\n,tWf.closeddate\r\nFROM tr_workflow AS tWf\r\nINNER JOIN ms_groupworkflow AS msGwf \r\n\tON msGwf.groupworkflowcode = tWf.groupworkflowcode \r\n\tAND msGwf.isactive = true\r\nINNER JOIN md_groupworkflow AS mdGwf \r\n\tON mdGwf.groupworkflowcode = msGwf.groupworkflowcode\r\nORDER BY twf.documentnumber,twf.linegroup;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
