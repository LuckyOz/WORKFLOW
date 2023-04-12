
namespace WORKFLOW.Model.Context
{
    public partial class WorkflowContext : DbContext
    {
        public WorkflowContext(DbContextOptions<WorkflowContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //for set timestamp without timezone
        }

        public DbSet<ms_workflow>? ms_workflows { get; set; }
        public DbSet<md_workflow>? md_workflows { get; set; }
        public DbSet<ms_rule>? ms_rules { get; set; }
        public DbSet<md_rule_var>? md_rule_vars { get; set; }
        public DbSet<md_rule_exp>? md_rule_exps { get; set; }
        public DbSet<md_rule_rslt>? md_rule_rslts { get; set; }
        public DbSet<ms_groupworkflow>? ms_groupworkflows { get; set; }
        public DbSet<md_groupworkflow>? md_groupworkflows { get; set; }
        public DbSet<ms_user>? ms_users { get; set; }
        public DbSet<tr_workflow>? tr_workflows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<md_groupworkflow>(entity =>
            {
                entity.HasKey(e => new { e.groupworkflowcode, e.username })
                    .HasName("md_groupworkflow_PRIMARY");
            });

            modelBuilder.Entity<md_workflow>(entity =>
            {
                entity.HasKey(e => new { e.workflowcode, e.paramcode })
                    .HasName("md_workflow_PRIMARY");
            });

            modelBuilder.Entity<ms_rule>(entity =>
            {
                entity.HasKey(e => new { e.workflowcode, e.rulecode })
                    .HasName("ms_rule_PRIMARY");
            });

            modelBuilder.Entity<md_rule_var>(entity =>
            {
                entity.HasKey(e => new { e.workflowcode, e.rulecode, e.paramcode })
                    .HasName("md_rule_var_PRIMARY");
            });

            modelBuilder.Entity<md_rule_exp>(entity =>
            {
                entity.HasKey(e => new { e.workflowcode, e.rulecode, e.linenum, e.groupline, e.paramcode })
                    .HasName("md_rule_exp_PRIMARY");
            });

            modelBuilder.Entity<md_rule_rslt>(entity =>
            {
                entity.HasKey(e => new { e.workflowcode, e.rulecode, e.linenum })
                    .HasName("md_rule_rsl_PRIMARY");
            });

            modelBuilder.Entity<ms_groupworkflow>(entity =>
            {
                entity.HasKey(e => new { e.workflowcode, e.groupworkflowcode })
                    .HasName("ms_groupworkflow_PRIMARY");
            });

            modelBuilder.Entity<tr_workflow>(entity =>
            {
                entity.HasKey(e => new { e.documentnumber, e.linegroup, e.workflowcode, e.rulecode, e.groupworkflowcode })
                    .HasName("tr_workflow_PRIMARY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
