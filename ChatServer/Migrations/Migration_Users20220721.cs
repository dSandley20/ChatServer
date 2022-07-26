using System;
using FluentMigrator;
using FluentMigrator.Postgres;
using FluentMigrator.SqlServer;

namespace ChatServer.Migrations
{
    [Migration(20220721094000)]
    public class Migration_Users20220721 : Migration
    { 

        public override void Down()
        {
            Delete.Table("users");
        }

        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("username").AsString().NotNullable()
                .WithColumn("email").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable();
        }
    }
}
