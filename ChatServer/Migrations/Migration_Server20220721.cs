using System;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace ChatServer.Migrations
{
    [Migration(2)]
    public class Mirgation_Server20220722 : Migration
    {

        public override void Down()
        {
            Delete.Table("server");
        }

        public override void Up()
        {
            Create.Table("server")
                 .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity(1, 1)
                 .WithColumn("name").AsString().NotNullable()
                 .WithColumn("invite_only").AsBoolean().WithDefaultValue(false).NotNullable();
        }
    }
}
